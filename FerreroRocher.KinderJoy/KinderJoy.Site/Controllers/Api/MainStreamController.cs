using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using KinderJoy.Domain.Entities.MainStream;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Models.MainStream;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.MainStream;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Admin.MainStream;
using KinderJoy.Site.Models.MainStream;
using LinqKit;
using PagedList;

namespace KinderJoy.Site.Controllers.Api {
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/main-stream")]
    public class MainStreamController : ApiController
    {
        private IMainStreamService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;
        public MainStreamController(IMainStreamService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<MainStreamSnsModel, MainStreamSurpriseSNS>();
                cfg.CreateMap<MainStreamSurprise, AdminMainStreamViewModel>();
                cfg.CreateMap<MainStreamSurpriseSNS, AdminMainStreamSnsViewModel>();
            });
        }

        [Route("check", Name = "CheckPersonalEntry")]
        [HttpPost]
        public MainStreamModel CheckPersonalEntry(MainStreamModel entry) {
            if (common.Now >= new DateTime(2016, 10, 15)) {
                throw new EventServiceException("400", "이벤트가 종료되었습니다!\\n다음 이벤트를 기다려주세요^^'", entry);
            }
            return entry;
        }

        [Route("create", Name = "CreateEntry")]
        [HttpPost]
        public long CreateEntry(MainStreamEntryModel entry) {
            if (common.Now >= new DateTime(2016, 10, 15)) {
                throw new EventServiceException("400", "이벤트가 종료되었습니다!\\n다음 이벤트를 기다려주세요^^'", entry);
            }
            var result = service.CreateEntry(new Domain.Entities.MainStream.MainStreamSurprise {
                Name = entry.Name,
                Mobile = entry.Mobile,
                Age = entry.Age,
                Gender = entry.Gender,
                Quiz = entry.Quiz,
                Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web",
                IpAddress = common.ClientIP,
                CreateDate = common.Now
            });
            
            return result;
        }
        //main-stream/create-sns
        [Route("create-sns", Name ="CreateSns")]
        [HttpPost]
        public void CreateSns(MainStreamSnsModel entry) {
            var createEntrySns = mapperConfig.CreateMapper().Map<MainStreamSurpriseSNS>(entry);
            service.CreateEntrySns(createEntrySns);
        }

        [Authorize]
        [Route("list/entry", Name = "GetMainStreamModel")]
        [HttpGet]
        public IPagedList<AdminMainStreamViewModel> GetMainStreamModel([FromUri] MainStreamQueryOptions options) {
            var entry = service.MainStreamSurprises.AsExpandable().Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate).ToList();
            var list = mapperConfig.CreateMapper().Map<List<AdminMainStreamViewModel>>(entry);
            return new SerializablePagedList<AdminMainStreamViewModel>(list, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("list/sns", Name = "GetMainStreamSnsModel")]
        [HttpGet]
        public IPagedList<AdminMainStreamSnsViewModel> GetMainStreamSnsModel([FromUri] MainStreamSnsQueryOptions options) {
            var entry = service.MainStreamSurpriseSNSs.AsExpandable().Where(options.BuildPredicate()).OrderByDescending(x => x.RegisterDate).ToList();
            var list = mapperConfig.CreateMapper().Map<List<AdminMainStreamSnsViewModel>>(entry);
            return new SerializablePagedList<AdminMainStreamSnsViewModel>(list, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("list/sns-stats", Name = "GetMainStreamSnsStatsModel")]
        [HttpGet]
        public IPagedList<AdminMainStreamSnsStatsViewModel> GetMainStreamSnsStatsModel([FromUri] PageQueryOptions options) {
            var list = service.GetMainStreamSurpriseSnsStats().OrderByDescending(x=>x.TotalCount).ToList();
            return new SerializablePagedList<AdminMainStreamSnsStatsViewModel>(list, options.Page, options.PageSize);
        }
    }
}
