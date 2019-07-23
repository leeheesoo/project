using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Repository.ChildrensDay;
using KinderJoy.Domain.Service;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.ChildrensDay;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KinderJoy.Site.Controllers.Api
{
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/childrens-day")]
    public class ChildrensDayApiController : ApiController
    {
        private IChildrensDayRepository repository;
        private ICommonProvider common;

        public ChildrensDayApiController(IChildrensDayRepository repository, ICommonProvider common) {
            this.repository = repository;
            this.common = common;
        }

        [Route("create", Name = "CreateChildrensDayHiddenPictureEntry")]
        [HttpPost]
        public ChildrensDayHiddenPictureModel CreateChildrensDayHiddenPictureEntry(ChildrensDayHiddenPictureModel entry) {
            if (common.Now >= new DateTime(2016, 5, 23)) {
                throw new EventServiceException("400", "이벤트가 종료되었습니다!\\n다음 이벤트를 기다려주세요^^'", entry);
            }
            var result = repository.CreateChildrensDayHiddenPicture(new Domain.Entities.ChildrensDay.ChildrensDayHiddenPicture {
                Name = entry.Name,
                Mobile = entry.Mobile,
                Age = entry.Age,
                Gender = entry.Gender,
                Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web",
                IpAddress = common.ClientIP,
                CreateDate = common.Now
            });
            entry.Id = result.Id;
            return entry;
        }

        [Route("create-sns", Name = "CreateChildrensDayHiddenPictureSnsEntry")]
        [HttpPost]
        public void CreateChildrensDayHiddenPictureSnsEntry(ChildrensDayHiddenPictureSnsModel entry) {
            if (common.Now >= new DateTime(2016, 5, 23)) {
                throw new EventServiceException("400", "이벤트가 종료되었습니다!\\n다음 이벤트를 기다려주세요^^'", entry);
            }
            repository.CreateChildrensDayHiddenPictureSns(new Domain.Entities.ChildrensDay.ChildrensDayHiddenPictureSns {
               ChildrensDayHiddenPictureId = entry.ChildrensDayHiddenPictureId,
               SnsType = entry.SnsType,
               SnsId = entry.SnsId,
               SnsName = entry.SnsName,
               SnsNickName = entry.SnsNickName,
               ScrapUrl = entry.ScrapUrl,
               RegisterDate = common.Now
            });
        }

        /// <summary>
        /// 관리자페이지 - 어린이날 이벤트 참여자 리스트
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("list", Name = "GetChildrensDayHiddenPictureList")]
        [HttpPost]
        [Authorize]
        public IPagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPicture> GetChildrensDayHiddenPictureList(ChildrensDayOption options) {
            var query = repository.ChildrensDayHiddenPicture.AsQueryable();
            if (options.FromDate.HasValue) {
                var fromDate = options.FromDate.Value.Date;
                query = query.Where(e => e.CreateDate >= fromDate);
            }
            if (options.ToDate.HasValue) {
                var toDate = options.ToDate.Value.AddDays(1).Date;
                query = query.Where(e => e.CreateDate < toDate);
            }

            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.ToLower().Contains(options.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel.Equals(options.Channel));
            }
            var result = query.Select(e => new Models.Admin.ChildrensDay.ChildrensDayHiddenPicture {
                CreateDate = e.CreateDate,
                Channel = e.Channel,
                IpAddress = e.IpAddress,
                Name = e.Name,
                Gender = e.Gender,
                Mobile = e.Mobile,
                Age = e.Age
            }).OrderByDescending(e => e.CreateDate);

            return new Domain.Service.SerializablePagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPicture>(result, options.Page, options.PageSize);
        }

        /// <summary>
        /// 관리자페이지 - 어린이날 이벤트 공유
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("sns-list", Name = "GetChildrensDayHiddenPictureSnsList")]
        [HttpPost]
        [Authorize]
        public PagedList.IPagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNS> GetChildrensDayHiddenPictureSnsList(ChildrensDayOptionByBingoQuizSns options) {
            var query = repository.ChildrensDayHiddenPictureSns.Select(e => new Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNS {
                IpAddress = e.ChildrensDayHiddenPicture.IpAddress,
                Channel = e.ChildrensDayHiddenPicture.Channel,
                Name = e.ChildrensDayHiddenPicture.Name,
                Mobile = e.ChildrensDayHiddenPicture.Mobile,
                Age = e.ChildrensDayHiddenPicture.Age,
                SnsType = e.SnsType,
                SnsId = e.SnsId,
                SnsName = e.SnsName,
                SnsNickName = e.SnsNickName,
                ScrapUrl = e.ScrapUrl,
                RegisterDate = e.RegisterDate
            });

            if (options.FromDate.HasValue) {
                var sDate = options.FromDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }
            if (options.ToDate.HasValue) {
                var eDate = options.ToDate.Value.AddDays(1);
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
            return new SerializablePagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNS>(query, options.Page, options.PageSize);
        }

        /// <summary>
        /// 관리자페이지 - 어린이날 이벤트 공유 통계
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Authorize]
        [Route("sns-stats", Name = "GetChildrensDayHiddenPictureSnsStats")]
        [HttpPost]
        public PagedList.IPagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNSState> GetChildrensDayHiddenPictureSnsStats(ChildrensDayOption options) {
            var sns = repository.ChildrensDayHiddenPicture.AsQueryable()
                .Join(repository.ChildrensDayHiddenPictureSns, e => e.Id, p => p.ChildrensDayHiddenPictureId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name, Age = e.Age });
 
            var query = from s in sns
                        group s by s.Mobile into HiddenPicureSns

                        select new Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNSState {
                            Mobile = HiddenPicureSns.Key,
                            Name = HiddenPicureSns.Max(e => e.Name),
                            Age = HiddenPicureSns.Max(e => e.Age),
                            FacebookCount = HiddenPicureSns.Count(e => e.SnsType == "facebook"),
                            KakaostoryCount = HiddenPicureSns.Count(e => e.SnsType == "kakaostory"),
                            KakaotalkCount = HiddenPicureSns.Count(e => e.SnsType == "kakaotalk"),
                            TotalCount = HiddenPicureSns.Count()
                        };
            if (!string.IsNullOrEmpty(options.Name)) {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile)) {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            query = query.OrderByDescending(e => e.TotalCount);
            return new SerializablePagedList<Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNSState>(query, options.Page, options.PageSize);
        }
    }
}
