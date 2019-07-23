using AutoMapper;
using LinqKit;
using Samsonite.Mall.Domain.Entities.Christmas2017;
using Samsonite.Mall.Domain.Exceptions;
using Samsonite.Mall.Domain.Services.Christmas2017;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models;
using Samsonite.Mall.Models.Christmas2017Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using X.PagedList;

namespace Samsonite.Mall.Controllers.Api
{
    [RoutePrefix("api/2017-christmas")]
    [ValidationActionFilter]
    [OnApiException]
    public class Christmas2017Controller : ApiController
    {
        private IChristmas2017Service service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public Christmas2017Controller(IChristmas2017Service service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<Christmas2017EntryModel, Christmas2017Entry>();
            });
        }

        [HttpPost]
        [Route("create", Name = "CreateChristmas2017")]
        public void CreateChristmas2017(Christmas2017EntryModel model) {
            if (common.Now >= new DateTime(2017, 12, 18)) {
                throw new EventServiceException("400","이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<Christmas2017Entry>(model);
            entry.IpAddress = common.IpAddress;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web";
            entry.CreateDate = common.Now;
            var result = service.CreateChristmas2017Entry(entry);
        }

        [HttpGet]
        [Route("get-stats-list", Name = "GetChristmasVoteStats")]
        public Christmas2017VoteStatsModel GetChristmasVoteStats() {
            var result = new Christmas2017VoteStatsModel {
                TielonnCount = service.GetChristmasTielonnCount,
                MarlonCount = service.GetChristmasMarlonCount,
                PlumeBasicCount = service.GetChristmasPlumeBasicCount,
                SupremeLiteCount = service.GetChristmasSupremeLiteCount
            };

            return result;
        }

        [HttpPost]
        [Route("admin-create", Name = "CreateAdminChristmas2017")]
        [Authorize]
        public void CreateAdminChristmas2017(Christmas2017AdminModel model) {
            var entry = new Christmas2017Entry {
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteMallId = "test",
                ChristmasBagType = model.ChristmasBagType,
                CreateDate = common.Now,
                Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web",
                IpAddress = common.IpAddress
            };

            var result = service.CreateChristmas2017Entry(entry);
        }

        [HttpGet]
        [Route("get-admin-list", Name = "GetAdminChristmasEntryList")]
        [Authorize]
        public IPagedList<Christmas2017Entry> GetAdminChristmasEntryList([FromUri] Christmas2017PageQueryOptions options) {
            var result = service.GetAdminChristmasEntryList().AsExpandable().Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate);
            return new SerializablePagedList<Christmas2017Entry>(result, options.Page, options.PageSize);
        }
    }
}
