using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Samsonite.Mall.Infrastructure;

namespace Samsonite.Mall.Controllers {
    [RoutePrefix("")]
    public class HomeController : Controller {
        private ICommonProvider common;
        public HomeController(ICommonProvider common) {
            this.common = common;
        }
        [Route("")]
        public ActionResult Index() {
            //return View();
            //return RedirectToAction("Index","OneYearAnniversary");
            //return RedirectToAction("Index", "AsperoLaunching");
            return RedirectToAction("Index", "TwoYearAnniversary");
        }
        [Route("test")]
        public ActionResult Test() {
            return RedirectToAction("Index", "BagTotheFuture");
        }
    }
}