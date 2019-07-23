using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers {
    [RoutePrefix("")]
    public class HomeController : Controller {
        [Route("")]
        public ActionResult Index() {
            return View();
        }
    }
}