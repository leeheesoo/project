using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers {
    [RoutePrefix("1st-anniversary")]
    public class OneYearAnniversaryController : Controller {
        public OneYearAnniversaryController() {
        }
        [Route("")]
        public ActionResult Index() {
            //Request.Headers.Add("X-Frame-Options", "ALLOW-FROM https://staging-asia-samsonite.demandware.net");
            return View();
        }
    }
}