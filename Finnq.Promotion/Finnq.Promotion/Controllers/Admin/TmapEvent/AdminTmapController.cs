using Finnq.Promotion.Domain.Services.TmapEvent;
using Finnq.Promotion.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers.Admin.TmapEvent {
    [RoutePrefix("manager/tmap-event")]
    [AccessIpAddressFilterAttribute(AllowIpAddresses = new string[] { "211.60.50.131" })]
    public class AdminTmapController : Controller {
        private ITmapEventEntryService service;
        private ICommonProvider common;
        public AdminTmapController(ITmapEventEntryService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        [Route("")]
        public ActionResult Index() {
            return View();
        }

        [Route("download")]
        public void Download() {
            var entry = service.FilterTmapEventEntry(new TmapEventEntryQueryOptions { Page = 1, PageSize = int.MaxValue })
                .OrderByDescending(x => x.CreateDate).Select(x => new {
                    채널 = x.IsMobileDevice ? "mobile" : "web",
                    이름 = x.Name,
                    휴대폰 = x.Phone,
                    참여일 = x.CreateDate
                });

            common.ExcelDownLoad(entry, "tmap-event-entry");
        }
    }
}