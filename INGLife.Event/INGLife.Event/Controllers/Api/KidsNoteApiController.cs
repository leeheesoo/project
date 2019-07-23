using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.KidsNote;
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
    public class KidsNoteApiController : ApiController
    {
        private IKidsNoteService service;

        public  KidsNoteApiController(IKidsNoteService service) {
            this.service = service;
        }
        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]

        public IPagedList<KidsNoteEntry> GetAdminKidsNoteEntryList([FromUri] PageQueryOptions options) {
            var result = service.GetAdminKidsNoteEntryList();
            return new SerializablePagedList<KidsNoteEntry>(result, options.Page, options.PageSize);
        }
    }
}
