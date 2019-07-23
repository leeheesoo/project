using KinderJoy.Domain.Entities.Christmas2015;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Repository.Christmas2015;
using KinderJoy.Domain.Service;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Admin.Christmas2015;
using KinderJoy.Site.Models.Christmas2015;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KinderJoy.Site.Controllers.Api {
    /// <summary>
    /// 크리스마스 이벤트 Api
    /// </summary>
    [RoutePrefix("api/2015-christmas")]
    [OnApiException]
    [ValidationActionFilter]
    public class Christmas2015ApiController : ApiController {
        private IChristmas2015Repository repository;
        private ICommonProvider common;
        public Christmas2015ApiController(IChristmas2015Repository repository, ICommonProvider common){
            this.repository = repository;
            this.common = common;
        }

        #region 즉석당첨 이벤트
        /// <summary>
        /// [즉석당첨 이벤트] 이벤트 당첨 시, 개인정보 Update
        /// </summary>
        /// <param name="entry"></param>
        [Route("updateEvent1", Name="UpdateChristmas2015Event1")]
        [HttpPost]
        public void UpdateChristmas2015Event1(Christmas2015EventModel entry) {
            repository.UpdateChristmas2015WinById(new Domain.Entities.Christmas2015.Christmas2015Win {
                Id = entry.WinEvent.WinId,
                Name = entry.WinEvent.Name,
                Mobile = entry.WinEvent.Mobile,
                Zipcode = entry.WinEvent.ZipCode,
                Address1 = entry.WinEvent.Address1,
                Address2 = entry.WinEvent.Address2,
            });
        }
        #endregion

        #region 트리만들기 이벤트
        /// <summary>
        /// [트리만들기] 01. 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [Route("save-event2", Name = "SaveChristmas2015Event2")]
        [HttpPost]
        public long SaveChristmas2015Event2(Christmas2015EventModel entry) {
            return -1;

            //var saveData = repository.CreateChristmas2015MakeTree(new Domain.Entities.Christmas2015.Christmas2015MakeTree {
            //    Name = entry.TreeEvent.Name,
            //    Mobile = entry.TreeEvent.Mobile,
            //    Zipcode = entry.TreeEvent.ZipCode,
            //    Address1 = entry.TreeEvent.Address1,
            //    Address2 = entry.TreeEvent.Address2,
            //    Age = entry.TreeEvent.Age,
            //    IpAddress = common.ClientIP,
            //    Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web",
            //    RegisterDate = common.Now
            //});
            //return saveData.Id;
        }
        /// <summary>
        /// [트리만들기] 02. 선택한 장난감 & 합성이미지 & 합성텍스트 정보 저장
        /// 텍스트정보는 아직 미구현...
        /// </summary>
        /// <param name="entry"></param>
        [Route("update-event2", Name = "UpdateChristmas2015Event2")]
        [HttpPost]
        public  Domain.Entities.Christmas2015.Christmas2015MakeTree MakeTree(Christmas2015MakeTreeModel entry) {
            byte[] byteArray = Convert.FromBase64String(entry.SynthesisImage);
            MemoryStream stream = new MemoryStream(byteArray);
            string saveImage = common.S3FileUpload(stream, "Christmas2015", "synthesisImage.jpg");

            var result = repository.UpdateChristmas2015MakeTreeById(new Domain.Entities.Christmas2015.Christmas2015MakeTree {
                Id = entry.MakeTreeId,
                Toy1 = entry.Toy1,
                Toy2 = entry.Toy2,
                Toy3 = entry.Toy3,
                Toy4 = entry.Toy4,
                Toy5 = entry.Toy5,
                Toy6 = entry.Toy6,
                Toy7 = entry.Toy7,
                SynthesisImage = saveImage,
                Content = entry.Content
            });
            return result;
        }
        /// <summary>
        /// [트리만들기] 03. sns공유 - 1일 공유 가능 횟수 check
        /// </summary>
        /// <param name="entry"></param>
        [Route("sns-share-count", Name = "CheckShareCountChristmas2015Event2")]
        [HttpPost]
        public void CheckShareCount(Christmas2015MakeTreeCheckSNSModel entry) {
            var today = common.Now.ToString("yyyy-MM-dd");
            int shareCntPerDay = repository.GetSNSShareCountByMobile(entry.SnsType, entry.Mobile, today);
            if (shareCntPerDay >= 3) {
                throw new KinderJoy.Domain.Exceptions.EventServiceException("400", (entry.SnsType == "kakaostory" ? "카카오스토리" : (entry.SnsType == "kakaotalk" ? "카카오톡" : "페이스북")) + "은(는) 1일 3회 공유가 가능합니다.", shareCntPerDay);
            }
        }
        /// <summary>
        /// [트리만들기] 03. sns공유 - 저장
        /// </summary>
        /// <param name="entry"></param>
        [Route("save-sns", Name="SaveSnsChristmas2015Event2")]
        [HttpPost]
        public void SaveSnsShare(Christmas2015MakeTreeSNSModel entry) {
            repository.CreateChristmas2015MakeTreeSNSShare(new Domain.Entities.Christmas2015.Christmas2015MakeTreeSNSShare {
                Christmas2015MakeTreeId = entry.Christmas2015MakeTreeId,
                SnsType = entry.SnsType,
                SnsName = entry.SnsName,
                SnsNickName = entry.SnsNickName,
                SnsId = entry.SnsId,
                ScrapUrl = entry.ScrapUrl,
                RegisterDate = common.Now
            });
        }
        #endregion

        #region 관리자
        [Authorize]
        [Route("win-list")]
        [HttpPost]
        public PagedList.IPagedList<WinListEntry> GetChristmas2015WinList(Christmas2015QueryOptionsByWin options) {
            var query = repository.Christmas2015Wins.Select(e => new WinListEntry {
                Name = e.Name,
                Mobile = e.Mobile,
                Zipcode = e.Zipcode,
                Address1 = e.Address1,
                Address2 = e.Address2,
                PrizeType = e.PrizeType,
                IpAddress = e.IpAddress,
                Channel = e.Channel,
                RegisterDate = e.RegisterDate
            });

            if (options.StartDate.HasValue) {
                var sDate = options.StartDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }

            if (options.EndDate.HasValue) {
                var eDate = options.EndDate.Value.AddDays(1);
                query = query.Where(e => e.RegisterDate < eDate);
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel == options.Channel);
            }
            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }

            if (options.PrizeType.HasValue) {
                var prizeType = options.PrizeType.Value;
                query = query.Where(e => e.PrizeType == prizeType);
            }
            query = query.OrderByDescending(e => e.RegisterDate);
            return new SerializablePagedList<WinListEntry>(query, options.Page, options.PageSize);
        }
        [Authorize]
        [Route("win-by-ip")]
        [HttpPost]
        public PagedList.IPagedList<WinListByIpAddress> GetChristmas2015WinByIpAddress(Christmas2015QueryOptionsByWin options) {
            var query = repository.Christmas2015Wins;
            if (options.StartDate.HasValue) {
                var sDate = options.StartDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }
            if (options.EndDate.HasValue) {
                var eDate = options.EndDate.Value.AddDays(1);
                query = query.Where(e => e.RegisterDate < eDate);
            }
            if (!string.IsNullOrEmpty(options.IpAddress)) {
                query = query.Where(e => e.IpAddress.Contains(options.IpAddress));
            }

            var sql = query.GroupBy(e => e.IpAddress).Select(e => new WinListByIpAddress {
                IpAddress = e.Key,
                TotalCount = e.Count(),
                LoserCount = e.Count(x=>x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Loser),
                KinderCount = e.Count(x=>x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Kinder),
                CloakCount = e.Count(x=>x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Cloak)
            });
            sql = sql.OrderByDescending(e => e.TotalCount);
            return new SerializablePagedList<WinListByIpAddress>(sql, options.Page, options.PageSize);
        }
        [Authorize]
        [Route("win-settings")]
        [HttpPost]
        public IList<KinderJoy.Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrizeSetting> GetChristmas2015WinSettings() {
            var query = repository.Christmas2015WinPrizeSettings.Select(e => new KinderJoy.Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrizeSetting {
                Date = e.Date,
                PrizeType = e.PrizeType,
                TotalCount = e.TotalCount,
                WinnerCount = e.WinnerCount,
                StartTime = e.StartTime,
                Rate = e.Rate
            }).OrderBy(e=>e.Date);
            return query.ToList();
       }
        [Authorize]
        [Route("update-win-settings")]
        [HttpPost]
        public void UpdateChristmas2015WinSettings(WinSettings entry) {
            var updateEntry = new Christmas2015WinPrizeSetting {
                Date = entry.Date,
                PrizeType = entry.PrizeType
            };
            switch (entry.Key.ToUpper()) {
                case "TC"://총 수량
                    updateEntry.TotalCount = Convert.ToInt32(entry.Value);
                    repository.UpdateChristmas2015WinTotalCount(updateEntry);
                    break;
                case "ST"://시작시간
                    updateEntry.StartTime = Convert.ToInt32(entry.Value);
                    repository.UpdateChristmas2015WinStartTime(updateEntry);
                    break;
                case "RT"://확률
                    updateEntry.Rate = Convert.ToSingle(entry.Value);
                    repository.UpdateChristmas2015WinRate(updateEntry);
                    break;
            }
        }
        [Authorize]
        [Route("make-tree")]
        [HttpPost]
        public PagedList.IPagedList<MakeTreeEntry> GetChristmas2015MakeTree(Christmas2015QueryOptions options) {
            var query = repository.Christmas2015MakeTree.Select(e => new MakeTreeEntry {
                Name = e.Name,
                Mobile = e.Mobile,
                Zipcode = e.Zipcode,
                Address1 = e.Address1,
                Address2 = e.Address2,
                Age = e.Age,
                IpAddress = e.IpAddress,
                Channel = e.Channel,
                RegisterDate = e.RegisterDate,
                Toy1 = e.Toy1,
                Toy2 = e.Toy2,
                Toy3 = e.Toy3,
                Toy4 = e.Toy4,
                Toy5 = e.Toy5,
                Toy6 = e.Toy6,
                Toy7 = e.Toy7,
                SynthesisImage = e.SynthesisImage,
                Content = e.Content
            });
            if (options.StartDate.HasValue) {
                var sDate = options.StartDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }
            if (options.EndDate.HasValue) {
                var eDate = options.EndDate.Value.AddDays(1);
                query = query.Where(e => e.RegisterDate < eDate);
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel == options.Channel);
            }
            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            query = query.OrderByDescending(e => e.RegisterDate);
            return new SerializablePagedList<MakeTreeEntry>(query, options.Page, options.PageSize);
        }
        [Authorize]
        [Route("make-tree-sns")]
        [HttpPost]
        public PagedList.IPagedList<MakeTreeSNSEntry> GetChristmas2015MakeTreeSNS(Christmas2015QueryOptionsByMakeTreeSns options) {
            var query = repository.Christmas2015MakeTreeSNSShares.Select(e => new MakeTreeSNSEntry {
                IpAddress = e.Christmas2015MakeTree.IpAddress,
                Channel = e.Christmas2015MakeTree.Channel,
                Name = e.Christmas2015MakeTree.Name,
                Mobile = e.Christmas2015MakeTree.Mobile,
                Zipcode = e.Christmas2015MakeTree.Zipcode,
                Address1 = e.Christmas2015MakeTree.Address1,
                Address2 = e.Christmas2015MakeTree.Address2,
                Age = e.Christmas2015MakeTree.Age,
                SnsType = e.SnsType,
                SnsId = e.SnsId,
                SnsName = e.SnsName,
                SnsNickName = e.SnsNickName,
                ScrapUrl = e.ScrapUrl,
                RegisterDate = e.RegisterDate
            });

            if (options.StartDate.HasValue) {
                var sDate = options.StartDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }
            if (options.EndDate.HasValue) {
                var eDate = options.EndDate.Value.AddDays(1);
                query = query.Where(e => e.RegisterDate < eDate);
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel == options.Channel);
            }
            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            if (!string.IsNullOrEmpty(options.SnsType)) {
                query = query.Where(e => e.SnsType.ToLower().Equals(options.SnsType));
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel == options.Channel);
            }
            query = query.OrderByDescending(e => e.RegisterDate);
            return new SerializablePagedList<MakeTreeSNSEntry>(query, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("make-tree-sns-stats")]
        [HttpPost]
        public PagedList.IPagedList<MakeTreeSNSStats> GetChristmas2015MakeTreeSNSStats(Christmas2015QueryOptions options) {
            var sns = repository.Christmas2015MakeTree.AsQueryable()
                .Join(repository.Christmas2015MakeTreeSNSShares, e => e.Id, p => p.Christmas2015MakeTreeId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name });

            var query = from s in sns
                       group s by s.Mobile into makeTreeSns
                       select new MakeTreeSNSStats {
                           Mobile = makeTreeSns.Key,
                           Name = makeTreeSns.Max(e => e.Name),
                           FacebookCount = makeTreeSns.Count(e => e.SnsType =="facebook"),
                           KakaostoryCount = makeTreeSns.Count(e => e.SnsType == "kakaostory"),
                           KakaotalkCount = makeTreeSns.Count(e => e.SnsType == "kakaotalk"),
                           TotalCount = makeTreeSns.Count()
                       };
            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            query = query.OrderByDescending(e => e.TotalCount);
            return new SerializablePagedList<MakeTreeSNSStats>(query, options.Page, options.PageSize);
        }
        #endregion
    }
}
