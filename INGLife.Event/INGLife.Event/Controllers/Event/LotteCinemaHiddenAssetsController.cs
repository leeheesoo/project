using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event
{
    [RoutePrefix("lotte-cinema")]
    public class LotteCinemaHiddenAssetsController : Controller
    {
        [Route("")]
        public ActionResult Intro()
        {
            return View();
        }

        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("asset1")]
        public ActionResult asset1()
        {
            return View();
        }

        [Route("asset2_1")]
        public ActionResult asset2_1()
        {
            return View();
        }

        [Route("asset2_2")]
        public ActionResult asset2_2()
        {
            return View();
        }

        [Route("asset2_3")]
        public ActionResult asset2_3()
        {
            return View();
        }

        [Route("asset2_4")]
        public ActionResult asset2_4()
        {
            return View();
        }

        [Route("asset2_5")]
        public ActionResult asset2_5()
        {
            return View();
        }

        [Route("asset2_6")]
        public ActionResult asset2_6()
        {
            return View();
        }
    }
}