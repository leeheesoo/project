using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Repository.BackToSchool2016;
using KinderJoy.Domain.Service;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.BackToSchool2016;
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
    [RoutePrefix("api/2016-backtoschool")]
    public class BackToSchool2016ApiController : ApiController
    {
        private IBackToSchool2016Repository repository;
        private ICommonProvider common;

        public BackToSchool2016ApiController(IBackToSchool2016Repository repository,ICommonProvider common)
        {
            this.repository = repository;
            this.common = common;
        }

        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [Route("create", Name = "CreateBackToSchool2016BingoQuizEntry")]
        [HttpPost]
        public BackToSchool2016BingoQuizModel CreateBackToSchool2016BingoQuizEntry(BackToSchool2016BingoQuizModel entry)
        {
            if(common.Now >= new DateTime(2016, 3, 14))
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.\n감사합니다.", entry);
            }
            var result = repository.CreateBackToSchool2016BingoQuiz(new Domain.Entities.BackToSchool2016.BackToSchool2016BingoQuiz
            {
                Name = entry.Name,
                Age = entry.Age,
                Mobile = entry.Mobile,
                ZipCode = entry.ZipCode,
                Address1 = entry.Address1,
                Address2 = entry.Address2,
                Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web" ,
                IpAddress = common.ClientIP,
                CreateDate = common.Now
            });
            entry.Id = result.Id;
            return entry;
        }

        [Route("sns-share-check",Name = "SnsShareCheckCount")]
        [HttpPost]
        public void SnsShareCheckCount(BackToSchool2016BingoQuizCheckSNSShareModel entry)
        {
            var bingoQuiz = repository.BackToSchool2016BingoQuiz.Where(e => e.Id.Equals(entry.BingoQuizId)).SingleOrDefault();

            int cnt = repository.GetBackToSchool2016BingoQuizSnsShareCount(entry.SNSType, bingoQuiz.Mobile, common.Now.ToString("yyyy-MM-dd"));

            if (cnt >= 3)
            {
                throw new KinderJoy.Domain.Exceptions.EventServiceException("400", (entry.SNSType == "kakaostory" ? "카카오스토리" : (entry.SNSType == "kakaotalk" ? "카카오톡" : "페이스북")) + "은(는) 1일 3회 공유가 가능합니다.", cnt);
            }
        }

        [Route("create-sns", Name = "CreateBackToSchool2016BingoQuizSnsEntry")]
        [HttpPost]
        public void CreateBackToSchool2016BingoQuizSnsEntry (BackToSchool2016BingoQuizSnsModel entry) {
            if (common.Now >= new DateTime(2016, 3, 14)) {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.\n감사합니다.", entry);
            }
            repository.CreateBackToSchool2016BingoQuizSns(new Domain.Entities.BackToSchool2016.BackToSchool2016BingoQuizSns {
                BackToSchool2016BingoQuizId = entry.BackToSchool2016BingoQuizId,
                SnsType = entry.SnsType,
                SnsId = entry.SnsId,
                SnsName = entry.SnsName,
                SnsNickName = entry.SnsNickName,
                ScrapUrl = entry.ScrapUrl,
                RegisterDate = common.Now
            });
        }

        /// <summary>
        /// 관리자페이지 - 새학기이벤트 참여자 리스트
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("list",Name = "GetBackToSchool2016BingoQuizList")]
        [HttpPost]
        [Authorize]
        public IPagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuiz> GetBackToSchool2016BingoQuizList(BackToSchool2016QueryOptions options)
        {
            var query = repository.BackToSchool2016BingoQuiz.AsQueryable();
            if (options.FromDate.HasValue)
            {
                var fromDate = options.FromDate.Value.Date;
                query = query.Where(e => e.CreateDate >= fromDate);
            }
            if (options.ToDate.HasValue)
            {
                var toDate = options.ToDate.Value.AddDays(1).Date;
                query = query.Where(e => e.CreateDate < toDate);
            }

            if (!string.IsNullOrEmpty(options.Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(options.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(options.Mobile))
            {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            if (!string.IsNullOrEmpty(options.Channel)) {
                query = query.Where(e => e.Channel.Equals(options.Channel));
            }
            var result = query.Select(e => new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuiz
            {
                CreateDate = e.CreateDate,
                Channel = e.Channel,
                IpAddress = e.IpAddress,
                Name= e.Name,
                Age = e.Age,
                Mobile = e.Mobile,
                ZipCode = e.ZipCode,
                Address1 = e.Address1,
                Address2 = e.Address2
            }).OrderByDescending(e=>e.CreateDate);

            return new Domain.Service.SerializablePagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuiz>(result, options.Page, options.PageSize);
        }

        /// <summary>
        /// 관리자페이지 - 새학기이벤트 이벤트 공유
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("sns-list", Name = "GetBackToSchool2016BingoQuizSNSList")]
        [HttpPost]
        [Authorize]
        public PagedList.IPagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNS> GetBackToSchool2016BingoQuizSNSList(BackToSchool2016QueryOptionsByBingoQuizSns options)
        {
            var query = repository.BackToSchool2016BingoQuizSns.Select(e => new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNS
            {
                IpAddress = e.BackToSchool2016BingoQuiz.IpAddress,
                Channel = e.BackToSchool2016BingoQuiz.Channel,
                Name = e.BackToSchool2016BingoQuiz.Name,
                Mobile = e.BackToSchool2016BingoQuiz.Mobile,
                ZipCode = e.BackToSchool2016BingoQuiz.ZipCode,
                Address1 = e.BackToSchool2016BingoQuiz.Address1,
                Address2 = e.BackToSchool2016BingoQuiz.Address2,
                Age = e.BackToSchool2016BingoQuiz.Age,
                SnsType = e.SnsType,
                SnsId = e.SnsId,
                SnsName = e.SnsName,
                SnsNickName = e.SnsNickName,
                ScrapUrl = e.ScrapUrl,
                RegisterDate = e.RegisterDate
            });

            if (options.FromDate.HasValue)
            {
                var sDate = options.FromDate.Value.Date;
                query = query.Where(e => e.RegisterDate >= sDate);
            }
            if (options.ToDate.HasValue)
            {
                var eDate = options.ToDate.Value.AddDays(1);
                query = query.Where(e => e.RegisterDate < eDate);
            }
            if (!string.IsNullOrEmpty(options.Channel))
            {
                query = query.Where(e => e.Channel == options.Channel);
            }
            if (!string.IsNullOrEmpty(options.Name))
            {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile))
            {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            if (!string.IsNullOrEmpty(options.SnsType))
            {
                query = query.Where(e => e.SnsType.ToLower().Equals(options.SnsType));
            }
            if (!string.IsNullOrEmpty(options.Channel))
            {
                query = query.Where(e => e.Channel == options.Channel);
            }
            query = query.OrderByDescending(e => e.RegisterDate);
            return new SerializablePagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNS>(query, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("sns-stats")]
        [HttpPost]
        public PagedList.IPagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNSStats> GetChristmas2015MakeTreeSNSStats(BackToSchool2016QueryOptions options)
        {
            var sns = repository.BackToSchool2016BingoQuiz.AsQueryable()
                .Join(repository.BackToSchool2016BingoQuizSns, e => e.Id, p => p.BackToSchool2016BingoQuizId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name });

            var query = from s in sns
                        group s by s.Mobile into BingoQuizSns
                        select new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNSStats
                        {
                            Mobile = BingoQuizSns.Key,
                            Name = BingoQuizSns.Max(e => e.Name),
                            FacebookCount = BingoQuizSns.Count(e => e.SnsType == "facebook"),
                            KakaostoryCount = BingoQuizSns.Count(e => e.SnsType == "kakaostory"),
                            KakaotalkCount = BingoQuizSns.Count(e => e.SnsType == "kakaotalk"),
                            TotalCount = BingoQuizSns.Count()
                        };
            if (!string.IsNullOrEmpty(options.Name))
            {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if (!string.IsNullOrEmpty(options.Mobile))
            {
                query = query.Where(e => e.Mobile.Contains(options.Mobile));
            }
            query = query.OrderByDescending(e => e.TotalCount);
            return new SerializablePagedList<Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNSStats>(query, options.Page, options.PageSize);
        }
    }
}
