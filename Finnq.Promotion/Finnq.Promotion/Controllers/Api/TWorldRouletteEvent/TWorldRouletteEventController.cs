using AutoMapper;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Services.TWorldRouletteEvent;
using Finnq.Promotion.Infrastructures;
using Finnq.Promotion.Models.TWorldRouletteEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using X.PagedList;

namespace Finnq.Promotion.Controllers.Api.TWorldRouletteEvent {
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/tworld-roulette")]
    public class TWorldRouletteEventController : ApiController
    {
        private ITWorldRouletteEventService service;
        private MapperConfiguration mapperConfig;
        private ICommonProvider common;

        public TWorldRouletteEventController(ITWorldRouletteEventService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<TWorldRouletteEventPrizeSettingModel, TWorldRouletteEventInstantLotteryPrizeSetting>();
                cfg.CreateMap<TWorldRouletteEventEntryModel, TWorldRouletteEventEntry>().ForMember(dest => dest.Mobile,opt => opt.MapFrom(e => string.Format("{0}{1}{2}",e.PhoneA,e.PhoneB,e.PhoneC)));
            });
        }

        [HttpPost]
        [Route("create",Name = "CreateTWorldRouletteEventEntry")]
        public long CreateTWorldRouletteEventEntry(TWorldRouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2018, 1, 1)) {
                throw new TWorldRouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<TWorldRouletteEventEntry>(model);
            entry.CreateDate = common.Now;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC;
            entry.IpAddress = common.IpAddress;

            var result = service.CreateTWorldRouletteEventEntry(entry);
            return result.Id;
        }
        
        [HttpPost]
        [Route("prize",Name = "UpdateTWorldRouletteEventEntry")]
        public TWorldRouletteEventInstantLotteryViewModel UpdateTWorldRouletteEventEntry(UpdateTWorldRouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2018, 1, 1)) {
                throw new TWorldRouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = service.UpdateTWorldRouletteEventEntry(model.id);
            var result = new TWorldRouletteEventInstantLotteryViewModel {
                id = entry.Id,
                PrizeName = "waqpwpelre",
                PrizeType = entry.PrizeType.Value
            };
            switch (entry.PrizeType) {
                case TWorldRouletteEventInstantLotteryPrizeType.Cash5000:
                    result.PrizeName = "wlaisodn";
                    break;
                case TWorldRouletteEventInstantLotteryPrizeType.Cash10000:
                    result.PrizeName = "wtzixgcevr";
                    break;
                case TWorldRouletteEventInstantLotteryPrizeType.Cash50000:
                    result.PrizeName = "wmqiwlek";
                    break;
                case TWorldRouletteEventInstantLotteryPrizeType.Cash100000:
                    result.PrizeName = "whaesedsfogo";
                    break;
                case TWorldRouletteEventInstantLotteryPrizeType.Cash500000:
                    result.PrizeName = "wyzexcchvabn";
                    break;
                case TWorldRouletteEventInstantLotteryPrizeType.Cash1000000:
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
        [Route("create-prize", Name = "CreateTWorldRoulettePrizeSetting")]
        public TWorldRouletteEventInstantLotteryPrizeSetting CreateTWorldRoulettePrizeSetting(TWorldRouletteEventPrizeSettingModel model) {
            if(model.PrizeType == TWorldRouletteEventInstantLotteryPrizeType.Loser) {
                throw new TWorldRouletteServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<TWorldRouletteEventInstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);

            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpPost]
        [Route("update-prize", Name = "UpdateTWorldRoulettePrizeSetting")]
        public TWorldRouletteEventInstantLotteryPrizeSetting UpdateTWorldRoulettePrizeSetting(TWorldRouletteEventInstantLotteryPrizeSetting entity) {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminTWorldRouletteEventInstantLotteryPrizeSettingList")]
        public IList<TWorldRouletteEventInstantLotteryPrizeSetting> GetAdminTWorldRouletteEventInstantLotteryPrizeSettingList() {
            var result = service.GetAdminTWorldRouletteEventInstantLotteryPrizeSettingList();
            return result.ToList();
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-entry-list", Name = "GetAdminTWorldRouletteEventEntryList")]
        public IPagedList<TWorldRouletteEventEntry> GetAdminTWorldRouletteEventEntryList([FromUri] TWorldRouletteEventQueryOptions option) {
            var result = service.GetAdminTWorldRouletteEventEntryList(option);
            return new SerializablePagedList<TWorldRouletteEventEntry>(result, option.Page, option.PageSize);
        }
    }
}
