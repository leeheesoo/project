using AutoMapper;
using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.KittyJusticeLeague;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.KittyJusticeleage;
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
    [RoutePrefix("api/2017-kitty-justiceleague")]
    public class KittyJusticeLeagueApiController : ApiController
    {
        private IKittyJusticeLeagueService service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;

        public KittyJusticeLeagueApiController(IKittyJusticeLeagueService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<KittyJusticeLeaguePrizeSettingModel, KittyJusticeLeagueInstantLotteryPrizeSetting>();
            });
        }

        public void KittyJusticeLeagueUpdateWinner(KittyJusticeLeagueModel model) {
            // 당첨자 세션 check
            long? entryId = HttpContext.Current.Session["KittyJusticeLeagueEntryId"] as long?;
            KittyJusticeLeagueInstantLotteryPrizeType? prize = HttpContext.Current.Session["KittyJusticeLeaguePrizeType"] as KittyJusticeLeagueInstantLotteryPrizeType?;

            if(!entryId.HasValue || entryId.Value != model.Id) {
                throw new KittyJusticeLeagueServiceException("400", "당첨자가 아닙니다.", model.Id);
            }
            if (!prize.HasValue || prize.Value != model.PrizeType) {
                throw new KittyJusticeLeagueServiceException("400", "해당 경품 당첨자가 아닙니다.", model.PrizeType);
            }

            var user = service.GetInstantLotteryWinner(model.Id);
            var entity = mapperConfig.CreateMapper().Map<KittyJusticeLeagueModel,KittyJusticeLeagueInstantLottery>(model,user);
            entity.UpdateDate = common.Now;
            service.UpdateInstantLotteryWinner(entity);
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("create-prize", Name = "CreatePrizeSetting")]
        public void CreatePrizeSetting(KittyJusticeLeaguePrizeSettingModel model) {
            if (model.PrizeType == KittyJusticeLeagueInstantLotteryPrizeType.Loser) {
                throw new KittyJusticeLeagueServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<KittyJusticeLeagueInstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpPost]
        [Route("update-prize", Name = "UpdatePrizeSetting")]
        public void UpdatePrizeSetting(KittyJusticeLeagueInstantLotteryPrizeSetting entity) {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminInstatLotteryPrizeSettingList")]
        public IList<KittyJusticeLeagueInstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList() {
            var result = service.GetAdminInstatLotteryPrizeSettingList();
            return result.ToList();
        }

        [Authorize(Roles = "Administrators,Pentacles")]
        [HttpGet]
        [Route("admin-entry-list", Name = "GetAdminKittyJusticeLeagueEntryList")]
        public IPagedList<KittyJusticeLeagueInstantLottery> GetAdminKittyJusticeLeagueEntryList([FromUri] KittyJusticeLeagueQueryOptions option) {
            var result = service.GetAdminKittyJusticeLeagueEntryList(option);
            return new SerializablePagedList<KittyJusticeLeagueInstantLottery>(result, option.Page, option.PageSize);
        }
    }
}
