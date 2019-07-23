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
    [RoutePrefix("api/roulette")]
    public class RouletteEventController : ApiController
    {
        private IRouletteEventService service;
        private MapperConfiguration mapperConfig;
        private ICommonProvider common;

        public RouletteEventController(IRouletteEventService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<RouletteEventPrizeSettingModel, RouletteEventInstantLotteryPrizeSetting>();
                cfg.CreateMap<RouletteEventEntryModel, RouletteEventEntry>().ForMember(dest => dest.Mobile,opt => opt.MapFrom(e => string.Format("{0}{1}{2}",e.PhoneA,e.PhoneB,e.PhoneC)));
            });
        }

        [HttpPost]
        [Route("create",Name = "CreateRouletteEventEntry")]
        public long CreateRouletteEventEntry(RouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2017, 12, 1)) {
                throw new RouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<RouletteEventEntry>(model);
            entry.CreateDate = common.Now;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC;
            entry.IpAddress = common.IpAddress;

            var result = service.CreateRouletteEventEntry(entry);
            return result.Id;
        }
        
        [HttpPost]
        [Route("update",Name = "UpdateRouletteEventEntry")]
        public RouletteEventInstantLotteryViewModel UpdateRouletteEventEntry(UpdateRouletteEventEntryModel model) {
            if (common.Now >= new DateTime(2017, 12, 1)) {
                throw new RouletteServiceException("400", "이벤트가 종료되었습니다.", model);
            }
            var entry = service.UpdateRouletteEventEntry(model.id);
            var result = new RouletteEventInstantLotteryViewModel {
                id = entry.Id,
                PrizeName = "waqpwpelre",
                PrizeType = entry.PrizeType.Value
            };
            switch (entry.PrizeType) {
                case RouletteEventInstantLotteryPrizeType.Cash5000:
                    result.PrizeName = "wlaisodn";
                    break;
                case RouletteEventInstantLotteryPrizeType.Cash10000:
                    result.PrizeName = "wtzixgcevr";
                    break;
                case RouletteEventInstantLotteryPrizeType.Cash50000:
                    result.PrizeName = "wmqiwlek";
                    break;
                case RouletteEventInstantLotteryPrizeType.Cash100000:
                    result.PrizeName = "whaesedsfogo";
                    break;
                case RouletteEventInstantLotteryPrizeType.Cash500000:
                    result.PrizeName = "wyzexcchvabn";
                    break;
                case RouletteEventInstantLotteryPrizeType.Cash1000000:
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
        [Route("create-prize", Name = "CreatePrizeSetting")]
        public RouletteEventInstantLotteryPrizeSetting CreatePrizeSetting(RouletteEventPrizeSettingModel model) {
            if(model.PrizeType == RouletteEventInstantLotteryPrizeType.Loser) {
                throw new RouletteServiceException("400", "해당경품은 선택이 불가합니다.", model.PrizeType);
            }
            var entity = mapperConfig.CreateMapper().Map<RouletteEventInstantLotteryPrizeSetting>(model);
            entity.WinnerCount = 0;
            var result = service.CreateInstantLotteryPrizeSetting(entity);

            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpPost]
        [Route("update-prize", Name = "UpdatePrizeSetting")]
        public RouletteEventInstantLotteryPrizeSetting UpdatePrizeSetting(RouletteEventInstantLotteryPrizeSetting entity) {
            var result = service.UpdateInstantLotteryPrizeSetting(entity.Date, entity.PrizeType, entity.TotalCount, entity.StartTime, entity.Rate);
            return result;
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-prize-list", Name = "GetAdminRouletteEventInstantLotteryPrizeSettingList")]
        public IList<RouletteEventInstantLotteryPrizeSetting> GetAdminRouletteEventInstantLotteryPrizeSettingList() {
            var result = service.GetAdminRouletteEventInstantLotteryPrizeSettingList();
            return result.ToList();
        }

        [Authorize(Roles = "Administrators,Pentacle")]
        [HttpGet]
        [Route("admin-entry-list", Name = "GetAdminRouletteEventEntryList")]
        public IPagedList<RouletteEventEntry> GetAdminRouletteEventEntryList([FromUri] RouletteEventQueryOptions option) {
            var result = service.GetAdminRouletteEventEntryList(option);
            return new SerializablePagedList<RouletteEventEntry>(result, option.Page, option.PageSize);
        }
    }
}
