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

    [RoutePrefix("api/bag-to-the-future/entry")]
    [ValidationActionFilter]
    [OnApiException]
    public class BagtotheFutureEntryController : ApiController {
        private IBagtotheFutureService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public BagtotheFutureEntryController(IBagtotheFutureService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<BagtotheFutureEntryModel, BagtotheFutureEntry>();
            });
        }

        [HttpGet]
        [Route("get/list")]
        public IPagedList<BagtotheFutureEntry> Get([FromUri]BagtotheFutureEntryQueryOptions options) {
            var data = service.GetBagtotheFutureEntries().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate);
            return new SerializablePagedList<BagtotheFutureEntry>(data, options.Page, options.PageSize);
        }

        [HttpGet]
        [Route("get")]
        public BagtotheFutureEntry Get(long id) {
            return service.GetBagtotheFutureEntryById(id);
        }

        [HttpPost]
        [Route("post", Name = "CreateBagtotheFutureEntry")]
        public void Post([FromBody] BagtotheFutureEntryModel model) {
            if (common.Now >= new DateTime(2017, 7, 20)) {
                throw new BagtotheFutureServiceException("400", "접수가 마감되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<BagtotheFutureEntry>(model);
            entry.IpAddress = common.IpAddress;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web";
            entry.CreateDate = common.Now;
            service.CreateBagtotheFutureEntry(entry);
        }        
    }
}
