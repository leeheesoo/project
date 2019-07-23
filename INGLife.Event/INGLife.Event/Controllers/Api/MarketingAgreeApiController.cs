using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.MarketingAgree;
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
    public class MarketingAgreeApiController : ApiController
    {
        private IMarketingAgreeService service;

        public MarketingAgreeApiController (IMarketingAgreeService service) {
            this.service = service;
        }

        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]

        public IPagedList<MarketingAgreeEntry> GetAdminKidsNoteEntryList ([FromUri] PageQueryOptions options) {
            var result = service.GetAdminMarketingAgreeEntryList();
            return new SerializablePagedList<MarketingAgreeEntry>(result, options.Page, options.PageSize);
        }
    }
}
