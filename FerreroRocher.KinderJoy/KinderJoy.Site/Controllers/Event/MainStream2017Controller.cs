using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderJoy.Site.Infrastructure;

namespace KinderJoy.Site.Controllers.Event
{
    [RoutePrefix("event/main-stream")]
    public class MainStream2017Controller : Controller
    {
        private ICommonProvider common;
        public MainStream2017Controller(ICommonProvider common) {
            this.common = common;
        }
        [Route("")]
        public ActionResult Index() {
            ViewBag.IsClosed = "Y";
            if (common.Now < new DateTime(2016, 10, 16)) {
                ViewBag.IsClosed = "N";
            }
            return View();
        }
    }
}