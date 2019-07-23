using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.Rebranding;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using X.PagedList;

namespace INGLife.Event.Controllers.Api
{
    [OnApiException]
    [ValidationActionFilter]
    public class RebrandingApiController : ApiController
    {
        private IRebrandingEventService service;

        public RebrandingApiController(IRebrandingEventService service) {
            this.service = service;
        }
        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]
        public IPagedList<RebrandingMarketingAgreeEntry> GetAdminRebrandingMarketingEntryList([FromUri] RebrandingQueryOptions options) {
            var result = service.GetAdminRebrandingMarketingEntryList(options.month);
            return new SerializablePagedList<RebrandingMarketingAgreeEntry>(result, options.Page, options.PageSize);
        }
        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]
        public IPagedList<RebrandingConsultingAgreeEntry> GetAdminRebrandingConsultingEntryList([FromUri] RebrandingQueryOptions options) {
            var result = service.GetAdminRebrandingConsultingEntryList(options.month);
            return new SerializablePagedList<RebrandingConsultingAgreeEntry>(result, options.Page, options.PageSize);
        }
    }
}
