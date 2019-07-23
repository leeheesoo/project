using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers {
    [RoutePrefix("aspero")]
    public class AsperoLaunchingController : Controller {
        [Route("")]
        public ActionResult Index() {
            return View();
        }
    }
}