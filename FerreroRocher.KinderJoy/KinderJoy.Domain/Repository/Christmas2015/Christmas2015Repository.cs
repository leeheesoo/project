using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Abstract.Christmas2015;
using KinderJoy.Domain.Entities.Christmas2015;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;

namespace KinderJoy.Domain.Repository.Christmas2015 {
    public class Christmas2015Repository : IChristmas2015Repository {
        #region variables
        private AppDbContext context;
        private EventNaverPrizeSelector<Christmas2015EnumConst.Christmas2015WinPrizeSetting, Christmas2015EnumConst.Christmas2015WinPrize> prizeSelector;
        #endregion

        #region constructor
        public Christmas2015Repository(AppDbContext context) {
            this.context = context;
            prizeSelector = new EventNaverPrizeSelector<Christmas2015EnumConst.Christmas2015WinPrizeSetting, Christmas2015EnumConst.Christmas2015WinPrize>();
        }
        public Christmas2015Repository() {
            context = AppDbContext.Create();
            prizeSelector = new EventNaverPrizeSelector<Christmas2015EnumConst.Christmas2015WinPrizeSetting, Christmas2015EnumConst.Christmas2015WinPrize>();
        }
        #endregion

        #region [C]reate
        /// <summary>
        /// [즉석당첨 이벤트] 생성
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isOK"></param>
        /// <returns></returns>
        public Christmas2015Win CreateChristmas2015Win(Entities.Christmas2015.Christmas2015Win entry, bool isOK) {
            if (isOK) {
                // 기당첨 여부 확인 (IP)
                var winCount = context.Christmas2015Wins.Where(e => e.IpAddress.Equals(entry.IpAddress) && e.PrizeType != Christmas2015EnumConst.Christmas2015WinPrize.Loser).Count();
                
                // 중복당첨이 아닐시에만 즉석당첨 경품추첨
                if (winCount <= 0) {
                    var prizes = context.Christmas2015WinPrizeSettings.Where(e => e.Date == Now.Date && e.StartTime <= Now.Hour && e.TotalCount > e.WinnerCount && e.Rate > 0.0f).Select(e => new Christmas2015EnumConst.Christmas2015WinPrizeSetting {
                        Date = e.Date,
                        StartTime = e.StartTime,
                        PrizeType = e.PrizeType,
                        Rate = e.Rate,
                        TotalCount = e.TotalCount,
                        WinnerCount = e.WinnerCount
                    }).ToList();

                    entry.PrizeType = prizeSelector.SelectPrize(prizes, Christmas2015EnumConst.Christmas2015WinPrize.Loser, Now);
                    if (entry.PrizeType != Christmas2015EnumConst.Christmas2015WinPrize.Loser) {
                        //즉석당첨 setting 해당 경품 갯수 +1
                        var updateModel = context.Christmas2015WinPrizeSettings.Where(e => e.Date == Now.Date && e.PrizeType == entry.PrizeType).SingleOrDefault();
                        if (updateModel != null) {
                            updateModel.WinnerCount = updateModel.WinnerCount + 1;
                            var updateEntry = context.Entry(updateModel);
                            updateEntry.Property(e => e.WinnerCount).IsModified = true;
                        }
                    }
                }
            }
            var result = context.Christmas2015Wins.Add(entry);
            context.SaveChanges();

            return result;
        }
        /// <summary>
        /// [트리만들기 이벤트] 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public Christmas2015MakeTree CreateChristmas2015MakeTree(Christmas2015MakeTree entry) {
            var result = context.Christmas2015MakeTrees.Add(entry);
            context.SaveChanges();
            return result;
        }
        /// <summary>
        /// [트리만들기 이벤트] 장난감 정보&합성이미지 Update
        /// </summary>
        /// <param name="entry"></param>
        public void CreateChristmas2015MakeTreeSNSShare(Christmas2015MakeTreeSNSShare entry) {
            var result = context.Christmas2015MakeTreeSNSShares.Add(entry);
            context.SaveChanges();
        }
        /// <summary>
        /// [즉석당첨 이벤트] 경품 셋팅
        /// </summary>
        /// <param name="saveDatas"></param>
        public void CreateChristmas2015WinSettings(List<Dictionary<string, object>> saveDatas) {
            //이전 데이터(삭제할 데이터)
            var removeData = context.Christmas2015WinPrizeSettings;
            //엑셀 업로드 데이터
            foreach (Dictionary<string, object> data in saveDatas) {
                context.Christmas2015WinPrizeSettings.Add(new Christmas2015WinPrizeSetting {
                    Date = (data["Date"] == null ? new DateTime(1900, 1, 1) : Convert.ToDateTime(data["Date"].ToString())),
                    PrizeType = (Christmas2015EnumConst.Christmas2015WinPrize)Enum.Parse(typeof(Christmas2015EnumConst.Christmas2015WinPrize), data["PrizeType"].ToString()),
                    TotalCount = (data["TotalCount"] == null ? 0 : Convert.ToInt32(data["TotalCount"].ToString())),
                    StartTime = (data["StartTime"] == null ? 10 : Convert.ToInt32(data["StartTime"].ToString())),
                    Rate = (data["Rate"] == null ? 0 : Convert.ToSingle(data["Rate"].ToString())),

                });
            }
            if (removeData != null) {
                context.Christmas2015WinPrizeSettings.RemoveRange(removeData);
            }
            context.SaveChanges();
        }
        #endregion

        #region [R]ead
        /// <summary>
        /// [즉석당첨 이벤트] 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public IList<Christmas2015Win> GetChristmas2015Win() {
            return context.Christmas2015Wins.ToList();
        }
        /// <summary>
        /// [즉석당첨 이벤트] 경품,확률 셋팅정보 가져오기
        /// </summary>
        /// <returns></returns>
        public IList<Christmas2015WinPrizeSetting> GetChristmas2015WinPrizeSettings() {
            return context.Christmas2015WinPrizeSettings.ToList();
        }
        /// <summary>
        /// [트리만들기 이벤트] SNS공유 가능 횟수 가져오기
        /// </summary>
        /// <param name="snsType"></param>
        /// <param name="mobile"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public int GetSNSShareCountByMobile(string snsType, string mobile, string today) {    
    
            var query = context.Christmas2015MakeTreeSNSShares.AsQueryable()
                .Join(context.Christmas2015MakeTrees, e => e.Christmas2015MakeTreeId, p => p.Id, (e, p) => new {SnsType = e.SnsType, Mobile = p.Mobile,  RegistDate = SqlFunctions.DatePart("year", p.RegisterDate) + "-" + SqlFunctions.DatePart("month", p.RegisterDate) + "-" + (SqlFunctions.DatePart("day", p.RegisterDate).ToString().Length == 1 ? "0" + SqlFunctions.DatePart("day", p.RegisterDate).ToString() : SqlFunctions.DatePart("day", p.RegisterDate).ToString()) });
            query = query.Where(e => e.RegistDate.Equals(today) && e.SnsType.Equals(snsType.ToLower()) && e.Mobile.Equals(mobile));
            return query.Count();
        }
        /// <summary>
        /// 현재날짜
        /// </summary>
        public DateTime Now {
            get {
                return context.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
            }
        }
        public IQueryable<Christmas2015WinPrizeSetting> Christmas2015WinPrizeSettings {
            get { return context.Christmas2015WinPrizeSettings; }
        }
        public IQueryable<Christmas2015Win> Christmas2015Wins {
            get { return context.Christmas2015Wins; }
        }
        public IQueryable<Christmas2015MakeTree> Christmas2015MakeTree {
            get { return context.Christmas2015MakeTrees; }
        }
        public IQueryable<Christmas2015MakeTreeSNSShare> Christmas2015MakeTreeSNSShares {
            get { return context.Christmas2015MakeTreeSNSShares; }
        }
        #endregion

        #region [U]date
        /// <summary>
        /// [즉석당첨 이벤트] 경품 당첨인 경우 개인정보 Update
        /// </summary>
        /// <param name="entry"></param>
        public void UpdateChristmas2015WinById(Christmas2015Win entry) {
            int winCount = context.Christmas2015Wins.Where(e => e.PrizeType != Christmas2015EnumConst.Christmas2015WinPrize.Loser && e.Mobile == entry.Mobile).Count();
            if (winCount > 0) {
                throw new EventServiceException("500", "이미 당첨된 내역이 존재합니다.\r\n한번 당첨된 분에게는 추가 경품이 지급되지 않습니다.", winCount);
            } else {
                var updateModel = context.Christmas2015Wins.Where(e => e.Id.Equals(entry.Id)).SingleOrDefault();
                if (updateModel != null) {
                    updateModel.Name = entry.Name;
                    updateModel.Mobile = entry.Mobile;
                    updateModel.Zipcode = entry.Zipcode;
                    updateModel.Address1 = entry.Address1;
                    updateModel.Address2 = entry.Address2;

                    var updateEntry = context.Entry(updateModel);
                    updateEntry.Property(e => e.Name).IsModified = true;
                    updateEntry.Property(e => e.Mobile).IsModified = true;
                    updateEntry.Property(e => e.Zipcode).IsModified = true;
                    updateEntry.Property(e => e.Address1).IsModified = true;
                    updateEntry.Property(e => e.Address2).IsModified = true;

                    context.SaveChanges();
                }
            }
        }
        /// <summary>
        /// [즉석당첨 이벤트] 총수량 Update
        /// </summary>
        /// <param name="entry"></param>
        public void UpdateChristmas2015WinTotalCount(Christmas2015WinPrizeSetting entry) {
            var updateModel = context.Christmas2015WinPrizeSettings.Where(e => e.Date == entry.Date && e.PrizeType == entry.PrizeType).SingleOrDefault();
            if (updateModel != null) {
                updateModel.TotalCount = entry.TotalCount;

                var updateEntry = context.Entry(updateModel);
                updateEntry.Property(e => e.TotalCount).IsModified = true;

                context.SaveChanges();
            }
        }
        /// <summary>
        /// [즉석당첨 이벤트] 시작시간 Update
        /// </summary>
        /// <param name="entry"></param>
        public void UpdateChristmas2015WinStartTime(Christmas2015WinPrizeSetting entry) {
            var updateModel = context.Christmas2015WinPrizeSettings.Where(e => e.Date == entry.Date && e.PrizeType == entry.PrizeType).SingleOrDefault();
            if (updateModel != null) {
                updateModel.StartTime = entry.StartTime;

                var updateEntry = context.Entry(updateModel);
                updateEntry.Property(e => e.StartTime).IsModified = true;

                context.SaveChanges();
            }
        }
        /// <summary>
        /// [즉석당첨 이벤트] 확률 Update
        /// </summary>
        /// <param name="entry"></param>
        public void UpdateChristmas2015WinRate(Christmas2015WinPrizeSetting entry) {
            var updateModel = context.Christmas2015WinPrizeSettings.Where(e => e.Date == entry.Date && e.PrizeType == entry.PrizeType).SingleOrDefault();
            if (updateModel != null) {
                updateModel.Rate = entry.Rate;

                var updateEntry = context.Entry(updateModel);
                updateEntry.Property(e => e.Rate).IsModified = true;

                context.SaveChanges();
            }
        }
        /// <summary>
        /// [트리만들기 이벤트] 장난감 정보,합성이미지 Update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public Christmas2015MakeTree UpdateChristmas2015MakeTreeById(Christmas2015MakeTree entry) {
            var updateModel = context.Christmas2015MakeTrees.Where(e => e.Id.Equals(entry.Id)).SingleOrDefault();
            if (updateModel != null) {
                updateModel.SynthesisImage = entry.SynthesisImage;
                updateModel.Content = entry.Content;
                updateModel.Toy1 = entry.Toy1;
                updateModel.Toy2 = entry.Toy2;
                updateModel.Toy3 = entry.Toy3;
                updateModel.Toy4 = entry.Toy4;
                updateModel.Toy5 = entry.Toy5;
                updateModel.Toy6 = entry.Toy6;
                updateModel.Toy7 = entry.Toy7;

                var updateEntry = context.Entry(updateModel);
                updateEntry.Property(e => e.SynthesisImage).IsModified = true;
                updateEntry.Property(e => e.Content).IsModified = true;
                updateEntry.Property(e => e.Toy1).IsModified = true;
                updateEntry.Property(e => e.Toy2).IsModified = true;
                updateEntry.Property(e => e.Toy3).IsModified = true;
                updateEntry.Property(e => e.Toy4).IsModified = true;
                updateEntry.Property(e => e.Toy5).IsModified = true;
                updateEntry.Property(e => e.Toy6).IsModified = true;
                updateEntry.Property(e => e.Toy7).IsModified = true;

                context.SaveChanges();
            }
            return updateModel;
        }
        #endregion

        #region [D]elete
        #endregion
    }
}
