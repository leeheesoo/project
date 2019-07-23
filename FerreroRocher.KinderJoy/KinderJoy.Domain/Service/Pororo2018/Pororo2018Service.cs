using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.Pororo2018;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Repository.Pororo2018;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KinderJoy.Domain.Service.Pororo2018 {
    public class Pororo2018Service : IPororo2018Service {
        private IPororo2018InstantLotteryRepository repo;
        private IPororo2018InstantLotteryPrizeSettingRepository settingRepo;
        private ITimeProvider time;

        private EventNaverPrizeSelector<Pororo2018InstantLotteryPrizeSetting, Pororo2018InstantLotteryPrizeType> prizeSelector;

        public Pororo2018Service(IPororo2018InstantLotteryRepository repo, IPororo2018InstantLotteryPrizeSettingRepository settingRepo,ITimeProvider time) {
            this.repo = repo;
            this.settingRepo = settingRepo;
            this.time = time;

            this.prizeSelector = new EventNaverPrizeSelector<Pororo2018InstantLotteryPrizeSetting, Pororo2018InstantLotteryPrizeType>();
        }
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        public Pororo2018InstantLottery CreateInstantLottery(Pororo2018InstantLottery entry,bool isChecked = false) {
            using (var tx = new TransactionScope()) {
                var now = time.Now;

                var isWin = repo.Any(x => x.PrizeType != Pororo2018InstantLotteryPrizeType.Loser && x.IpAddress == entry.IpAddress);

                entry.PrizeType = Pororo2018InstantLotteryPrizeType.Loser;
                if(isChecked && !isWin) {
                    var prizes = settingRepo.Filter(x => x.Date == now.Date && x.StartTime <= now.Hour && x.TotalCount > x.WinnerCount && x.Rate > 0.0f).ToList();
                    entry.PrizeType = prizeSelector.SelectPrize(prizes, Pororo2018InstantLotteryPrizeType.Loser, now);

                    if(entry.PrizeType != Pororo2018InstantLotteryPrizeType.Loser) {
                        var prize = settingRepo.SingleOrDefault(x => x.Date == now.Date && x.PrizeType == entry.PrizeType);
                        if(prize == null) {
                            entry.PrizeType = Pororo2018InstantLotteryPrizeType.Loser;
                        }else {
                            prize.WinnerCount = repo.Filter(x => x.CreateDate >= now.Date && x.PrizeType == entry.PrizeType).Count() + 1;
                            settingRepo.Update(prize);
                        }
                    }
                }

                entry.CreateDate = now;
                var user = repo.Add(entry);
                repo.Save();

                tx.Complete();

                return user;
            }
        }
        /// <summary>
        /// 경품 당첨 후 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        public Pororo2018InstantLottery UpdateInstantLotteryWinner(Pororo2018InstantLottery entry) {
            var isWin = repo.Any(x => x.PrizeType != Pororo2018InstantLotteryPrizeType.Loser && x.Mobile == entry.Mobile);
            if (isWin) {
                throw new Pororo2018ServiceException("400", "이미 당첨된 내역이 존재합니다.당첨된 분에게는 추가 경품이 지급되지 않습니다.", entry.Mobile);
            }

            var result = repo.Update(entry);
            repo.Save();

            return result;
        }
        /// <summary>
        /// 경품 당첨 유저 탐색
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pororo2018InstantLottery GetInstantLotteryWinner(long id) {
            var entry = repo.SingleOrDefault(x => x.Id == id && x.PrizeType != Pororo2018InstantLotteryPrizeType.Loser && x.Mobile == null && x.Name == null);
            if (entry == null) {
                throw new Pororo2018ServiceException("400", "이미 개인정보가 등록되었거나 당첨 내역을 찾을 수 없습니다.", null);
            }
            return entry;
        }
        /// <summary>
        /// 관리자 경품 세팅
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public Pororo2018InstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(Pororo2018InstantLotteryPrizeSetting entry) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var isOverlapPrize = settingRepo.Filter(x => x.Date == entry.Date && x.PrizeType == entry.PrizeType).SingleOrDefault();
            if (isOverlapPrize != null) {
                throw new Pororo2018ServiceException("400", "이미 등록된 데이터가 있습니다.", null);
            }
            var result = settingRepo.Add(entry);
            settingRepo.Save();

            return result;
        }
        /// <summary>
        /// 관리자 경품 수정
        /// </summary>
        /// <param name="date"></param>
        /// <param name="prizeType"></param>
        /// <param name="totalCount"></param>
        /// <param name="startTime"></param>
        /// <param name="rate"></param>
        public Pororo2018InstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, Pororo2018InstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var updateEntry = settingRepo.SingleOrDefault(x => x.Date == date && x.PrizeType == prizeType);
            if (updateEntry == null) {
                throw new Pororo2018ServiceException("400", "수정할 데이터가 없습니다.", null);
            }
            if (updateEntry.WinnerCount > totalCount) {
                throw new Pororo2018ServiceException("400", "경품수량은 당첨수량보다 적게 세팅할 수 없습니다.", totalCount);
            }
            if (totalCount <= 0) {
                throw new Pororo2018ServiceException("400", "경품수량은 0이하로 세팅할 수 없습니다.", totalCount);
            }
            if (startTime < 0 || startTime >= 24) {
                throw new Pororo2018ServiceException("400", "당첨 시작시간은 0~23 까지 가능합니다.", startTime);
            }
            if (rate <= 0) {
                throw new Pororo2018ServiceException("400", "당첨확률은 0이하로 세팅할 수 없습니다.", rate);
            }
            updateEntry.TotalCount = totalCount;
            updateEntry.StartTime = startTime;
            updateEntry.Rate = rate;

            var result = settingRepo.Update(updateEntry);
            settingRepo.Save();

            return result;
        }

        public IQueryable<Pororo2018InstantLottery> GetAdminPororo2018InstatLotteryList(Pororo2018QueryOptions option) {
            return repo.GetAll().AsExpandable().Where(option.BuildPredicate()).OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<Pororo2018InstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList() {
            return settingRepo.GetAll().OrderBy(x => x.Date);
        }
    }

    public class Pororo2018ServiceException : EventServiceException {
        public Pororo2018ServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
