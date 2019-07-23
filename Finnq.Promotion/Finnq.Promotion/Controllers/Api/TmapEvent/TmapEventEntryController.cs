using AutoMapper;
using Finnq.Promotion.Domain.Entities.TmapEvent;
using Finnq.Promotion.Domain.Services.TmapEvent;
using Finnq.Promotion.Infrastructures;
using Finnq.Promotion.Models.TmapEventModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using X.PagedList;

namespace Finnq.Promotion.Controllers.Api.TmapEvent {
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/tmap-event")]
    public class TmapEventEntryController : ApiController {
        private ITmapEventEntryService service;
        private MapperConfiguration mapperConfig;
        private ICommonProvider common;

        public TmapEventEntryController(ITmapEventEntryService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(c => {
                c.CreateMap<TmapEventEntryModel, TmapEventEntry>();
            });
        }
        
        /// <summary>
        /// t map 이벤트 응모자 리스트 가져오기
        /// </summary>
        /// <param name="options">필터 조건. optional</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IPagedList<TmapEventEntry> Get([FromUri] TmapEventEntryQueryOptions options) {
            var data = service.FilterTmapEventEntry(options);
            return new SerializablePagedList<TmapEventEntry>(data, options.Page, options.PageSize);
        }

        /// <summary>
        /// t map 이벤트 응모하기
        /// </summary>
        /// <param name="model">응모 개인정보 입력</param>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateTmapEventEntry")]
        public TmapEventEntry Post([FromBody]TmapEventEntryModel model) {
            var entry = mapperConfig.CreateMapper().Map<TmapEventEntry>(model);
            entry.IsMobileDevice = HttpContext.Current.Request.Browser.IsMobileDevice;
            entry.IpAddress = common.IpAddress;
            entry.CreateDate = DateTime.Now;

            return service.CreateTmapEventEntry(entry);
        }        
    }
}
