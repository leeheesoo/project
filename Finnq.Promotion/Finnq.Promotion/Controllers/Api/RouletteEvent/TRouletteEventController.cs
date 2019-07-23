using AutoMapper;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Services.RouletteEvent;
using Finnq.Promotion.Infrastructures;
using Finnq.Promotion.Models.RouletteEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using X.PagedList;

namespace Finnq.Promotion.Controllers.Api.RouletteEvent
{
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/t-roulette")]
    public class TRouletteEventController : ApiController
    {
        private ITRouletteEventService service;
        private MapperConfiguration mapperConfig;
        private ICommonProvider common;

        public TRouletteEventController(ITRouletteEventService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<TRouletteEventPrizeSettingModel, TRouletteEventInstantLotteryPrizeSetting>();
                cfg.CreateMap<TRouletteEventEntryModel, TRouletteEventEntry>().ForMember(dest => dest.Mobile,opt => opt.MapFrom(e => string.Format("{0}{1}{2}",e.PhoneA,e.PhoneB,e.PhoneC)));
            });
        }

        [HttpPost]
        [Route("create",Name = "CreateTRouletteEventEntry")]
        public long CreateTRouletteEventEntry(TRouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2018, 1, 1)) {
                throw new TRouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<TRouletteEventEntry>(model);
            entry.CreateDate = common.Now;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC;
            entry.IpAddress = common.IpAddress;

            var result = service.CreateTRouletteEventEntry(entry);
            return result.Id;
        }
        
        [HttpPost]
        [Route("prize",Name = "UpdateTRouletteEventEntry")]
        public TRouletteEventInstantLotteryViewModel UpdateTRouletteEventEntry(UpdateTRouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2018, 1, 1)) {
                throw new TRouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = service.UpdateTRouletteEventEntry(model.id);
            var result = new TRouletteEventInstantLotteryViewModel {
                id = entry.Id,
                PrizeName = "waqpwpelre",
                PrizeType = entry.PrizeType.Value
            };
            switch (entry.PrizeType) {
                case TRouletteEventInstantLotteryPrizeType.Cash5000:
                    result.PrizeName = "wlaisodn";
                    break;
                case TRouletteEventInstantLotteryPrizeType.Cash10000:
                    result.PrizeName = "wtzixgcevr";
                    break;
                case TRouletteEventInstantLotteryPrizeType.Cash50000:
                    result.PrizeName = "wmqiwlek";
                    break;
                case TRouletteEventInstantLotteryPrizeType.Cash100000:
                    result.PrizeName = "whaesedsfogo";
                    break;
                case TRouletteEventInstantLotteryPrizeType.Cash500000:
                    result.PrizeName = "wyzexcchvabn";
                    break;
                case TRouletteEventInstantLotteryPrizeType.Cash1000000:
                    result.PrizeName = "wyloouvnea";
                    break;
                default:
                    result.PrizeName = "waqpwpelre";
                    break;
            }

            return result;
        }



        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpPost]
        [Route("create-prize", Name = "CreateTRoulettePrizeSetting")]
        public TRouletteEventInstantLotteryPrizeSetting CreateTRoulettePrizeSetting(TRouletteEventPrizeSettingModel model) {
            if(model.PrizeType == TRouletteEventInstantLotteryPrizeType.Loser) {
                throw new TRouletteServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<TRouletteEventInstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);

            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpPost]
        [Route("update-prize", Name = "UpdateTRoulettePrizeSetting")]
        public TRouletteEventInstantLotteryPrizeSetting UpdateTRoulettePrizeSetting(TRouletteEventInstantLotteryPrizeSetting entity) {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminTRouletteEventInstantLotteryPrizeSettingList")]
        public IList<TRouletteEventInstantLotteryPrizeSetting> GetAdminTRouletteEventInstantLotteryPrizeSettingList() {
            var result = service.GetAdminTRouletteEventInstantLotteryPrizeSettingList();
            return result.ToList();
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-entry-list", Name = "GetAdminTRouletteEventEntryList")]
        public IPagedList<TRouletteEventEntry> GetAdminTRouletteEventEntryList([FromUri] TRouletteEventQueryOptions option) {
            var result = service.GetAdminTRouletteEventEntryList(option);
            return new SerializablePagedList<TRouletteEventEntry>(result, option.Page, option.PageSize);
        }
    }
}
