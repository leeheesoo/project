using AutoMapper;
using KinderJoy.Domain.Entities.MavelFrozen;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Models.MarvelFrozen;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.MavelFrozen;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.MavelFrozen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KinderJoy.Site.Controllers.Api
{
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/2017-marvel-frozen")]
    public class MavelFrozenApiController : ApiController
    {
        private IMavelFrozenService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public MavelFrozenApiController(IMavelFrozenService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<MarvelFrozenUserModel, MavelFrozenUser>();
                cfg.CreateMap<MarvelFrozenSnsModel, MavelFrozenSNS>();
            });
        }

        [Route("create-user", Name = "CreateMarvelFrozenUser")]
        [HttpPost]
        public MavelFrozenUser CreateMarvelFrozenUser(MarvelFrozenUserModel model) {
            if (common.Now < new DateTime(2017, 4, 24,9,0,0)) {
                throw new EventServiceException("400", "4월 24일 월요일 오전 9시부터 참여가능합니다 :)", null);
            }
            if (common.Now >= new DateTime(2017, 5, 20)) {
                throw new EventServiceException("400", "5월 19일 이벤트가 종료되었습니다.", null);
            }
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<MavelFrozenUser>(model);

            entry.IpAddress = common.ClientIP;
            entry.Channel = common.IsMobileDevice ? "mobile" : "pc";
            entry.CreateDate = common.Now;

            var result = service.CreateUser(entry);
            return result;
        }

        [Route("create-sns", Name = "CreateMarvelFrozenSNS")]
        [HttpPost]
        public MavelFrozenSNS CreateMarvelFrozenSNS(MarvelFrozenSnsModel model) {
            if (common.Now < new DateTime(2017, 4, 24, 9, 0, 0)) {
                throw new EventServiceException("400", "4월 24일 월요일 오전 9시부터 참여가능합니다 :)", null);
            }
            if (common.Now >= new DateTime(2017, 5, 20)) {
                throw new EventServiceException("400", "5월 19일 이벤트가 종료되었습니다.", null);
            }
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<MavelFrozenSNS>(model);

            entry.CreateDate = common.Now;

            var result = service.CreateSNSShare(entry);
            return result;
        }

        [Authorize]
        [Route("get-users", Name = "MarvelFrozenUser")]
        [HttpGet]
        public PagedList.IPagedList<MavelFrozenUser> GetMarvelFrozenUserList([FromUri]AdminMarvelFrozenQueryOptions options) {
            var users = service.GetUser(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<MavelFrozenUser>(users, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("get-sns", Name = "GetMarvelFrozenSNSList")]
        [HttpGet]
        public PagedList.IPagedList<MavelFrozenSNS> GetMarvelFrozenSNSList([FromUri]AdminMarvelFrozenSNSQueryOptions options) {

            var users = service.GetSns(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<MavelFrozenSNS>(users, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("get-stats", Name = "GetMarvelFrozenSNSStats")]
        public PagedList.IPagedList<AdminMarvelFrozenStatsViewModel> GetMarvelFrozenSNSStats([FromUri]PageQueryOptions options) {
            var list = service.GetStatistics().OrderByDescending(x => x.TotalCount);
            return new SerializablePagedList<AdminMarvelFrozenStatsViewModel>(list, options.Page, options.PageSize);
        }
    }
}
