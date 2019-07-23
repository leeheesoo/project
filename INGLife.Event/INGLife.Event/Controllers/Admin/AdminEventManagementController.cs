using INGLife.Event.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize(Roles = "Administrators, Pentacle")]
    [RoutePrefix("manager/event-management")]
    public class AdminEventManagementController : Controller {
        [Route("")]
        public ActionResult Index() {
            return View();
        }
    }
}