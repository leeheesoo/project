using AutoMapper;
using KinderJoy.Domain.Entities.DisneyStarWars2018;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.DisneyStarWars2018;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.DisneyStarWars2018;
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
    [RoutePrefix("api/disney-starwars")]
    public class DisneyStarWars2018Controller : ApiController
    {
        private IDisneyStarWars2018Service service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;

        public DisneyStarWars2018Controller(IDisneyStarWars2018Service service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<DisneyStarWars2018PrizeSettingModel, DisneyStarWars2018InstantLotteryPrizeSetting>();
            });
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("create-prize", Name = "CreateDisneyStarWars2018PrizeSetting")]
        public void CreateDisneyStarWars2018PrizeSetting(DisneyStarWars2018PrizeSettingModel model)
        {
            if (model.PrizeType == DisneyStarWars2018InstantLotteryPrizeType.Loser)
            {
                throw new DisneyStarWars2018ServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<DisneyStarWars2018InstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);
        }


        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("update-prize", Name = "UpdateDisneyStarWars2018PrizeSetting")]
        public void UpdateDisneyStarWars2018PrizeSetting(DisneyStarWars2018InstantLotteryPrizeSetting entity)
        {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
        }


        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminDisneyStarWars2018InstatLotteryPrizeSettingList")]
        public IList<DisneyStarWars2018InstantLotteryPrizeSetting> GetAdminDisneyStarWars2018InstatLotteryPrizeSettingList()
        {
            var result = service.GetAdminInstatLotteryPrizeSettingList();
            return result.ToList();
        }


        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-DisneyStarWars-entry", Name = "GetAdminDisneyStarWars2018InstatLotteryList")]
        public IPagedList<DisneyStarWars2018InstantLottery> GetAdminDisneyStarWars2018InstatLotteryList([FromUri] DisneyStarWars2018QueryOptions options)
        {
            var result = service.GetAdminDisneyStarWars2018InstatLotteryList(options);
            return new SerializablePagedList<DisneyStarWars2018InstantLottery>(result, options.Page, options.PageSize);
        }
    }
}
