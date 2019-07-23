using AutoMapper;
using KinderJoy.Domain.Entities.Pororo2018;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.Pororo2018;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Pororo2018;
using PagedList;
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
    [RoutePrefix("api/2018-pororo")]
    public class Pororo2018ApiController : ApiController
    {
        private IPororo2018Service service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;

        public Pororo2018ApiController(IPororo2018Service service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Pororo2018PrizeSettingModel, Pororo2018InstantLotteryPrizeSetting>();
            });
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("create-prize", Name = "CreatePororo2018PrizeSetting")]
        public void CreatePororo2018PrizeSetting(Pororo2018PrizeSettingModel model) {
            if (model.PrizeType == Pororo2018InstantLotteryPrizeType.Loser) {
                throw new Pororo2018ServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<Pororo2018InstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("update-prize", Name = "UpdatePororo2018PrizeSetting")]
        public void UpdatePororo2018PrizeSetting(Pororo2018InstantLotteryPrizeSetting entity) {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminPororo2018InstatLotteryPrizeSettingList")]
        public IList<Pororo2018InstantLotteryPrizeSetting> GetAdminPororo2018InstatLotteryPrizeSettingList() {
            var result = service.GetAdminInstatLotteryPrizeSettingList();
            return result.ToList();
        }
        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-pororo-entry", Name = "GetAdminPororo2018InstatLotteryList")]
        public IPagedList<Pororo2018InstantLottery> GetAdminPororo2018InstatLotteryList([FromUri] Pororo2018QueryOptions options) {
            var result = service.GetAdminPororo2018InstatLotteryList(options);
            return new SerializablePagedList<Pororo2018InstantLottery>(result, options.Page, options.PageSize);
        }
    }
}
