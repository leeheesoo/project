using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.OverFortyFiveDb;
using INGLife.Event.Domain.Services.Rebranding;
using INGLife.Event.Domain.Services.TumblerEvent;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using X.PagedList;

namespace INGLife.Event.Controllers.Api
{
    [OnApiException]
    [ValidationActionFilter]
    public class TumblerEventApiController : ApiController
    {
        private ITumblerEventService service;

        public TumblerEventApiController(ITumblerEventService service) {
            this.service = service;
        }

        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]

        public IPagedList<TumblerEventEntry> GetAdminTumblerEventEntryList([FromUri] TumblerEventQueryOptions options)
        {
            var result = service.GetAdminTumblerEventEntryList(options.eventType);
            return new SerializablePagedList<TumblerEventEntry>(result, options.Page, options.PageSize);
        }
    }
}

