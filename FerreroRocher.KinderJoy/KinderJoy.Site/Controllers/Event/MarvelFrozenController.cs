using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event
{
    [RoutePrefix("event/2017-Marvel_frozen")]
    public class MarvelFrozenController : Controller
    {
        [Route("")]
        // GET: MarvelFrozen
        public ActionResult Index()
        {
            return View();
        }

        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}