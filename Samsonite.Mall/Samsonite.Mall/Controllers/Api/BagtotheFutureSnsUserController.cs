using AutoMapper;
using LinqKit;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Services.BagtotheFuture;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models;
using Samsonite.Mall.Models.BagtotheFutureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using X.PagedList;

namespace Samsonite.Mall.Controllers.Api {
    [RoutePrefix("api/bag-to-the-future/sns-user")]
    [ValidationActionFilter]
    [OnApiException]
    public class BagtotheFutureSnsUserController : ApiController {

        private IBagtotheFutureService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;
        public BagtotheFutureSnsUserController(IBagtotheFutureService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<BagtotheFutureSnsUserModel, BagtotheFutureSnsUser>();
            });
        }

        [HttpGet]
        [Route("get/list")]
        public IPagedList<BagtotheFutureSnsUser> Get([FromUri]BagtotheFutureSnsUserQueryOptions options) {
            var data = service.GetBagtotheFutureSnsUsers().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate);
            return new SerializablePagedList<BagtotheFutureSnsUser>(data, options.Page, options.PageSize);
        }

        [HttpPost]
        [Route("post", Name = "CreateSnsPersonalInfo")]
        public BagtotheFutureSnsUser Post([FromBody]BagtotheFutureSnsUserModel model) {
            if (common.Now >= new DateTime(2017, 7, 20)) {
                throw new BagtotheFutureServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<BagtotheFutureSnsUser>(model);
            entry.IpAddress = common.IpAddress;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web";
            entry.CreateDate = common.Now;
            var result = service.CreateBagtotheFutureSnsUser(entry);
            return result;
        }
    }
}
