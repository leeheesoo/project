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
using System.Web.Http;
using X.PagedList;

namespace Samsonite.Mall.Controllers.Api {
    [RoutePrefix("api/bag-to-the-future/sns")]
    [ValidationActionFilter]
    [OnApiException]
    public class BagtotheFutureSnsController : ApiController {

        private IBagtotheFutureService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;
        public BagtotheFutureSnsController(IBagtotheFutureService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<BagtotheFutureSnsModel, BagtotheFutureSns>();
            });
        }

        [HttpGet]
        [Route("get/list")]
        public IPagedList<BagtotheFutureSns> Get([FromUri]BagtotheFutureSnsQueryOptions options) {
            var data = service.GetBagtotheFutureSns().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate);
            return new SerializablePagedList<BagtotheFutureSns>(data, options.Page, options.PageSize);
        }

        [HttpPost]
        [Route("post")]
        public void Post(BagtotheFutureSnsModel model) {
            var entry = mapperConfig.CreateMapper().Map<BagtotheFutureSns>(model);            
            entry.CreateDate = common.Now;
            var result = service.CreateBagtotheFutureSns(entry);
        }
    }
}
