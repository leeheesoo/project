using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event
{
    [RoutePrefix("event/2017-findingdory")]
    public class FindingDory2017Controller : Controller
    {
        [Route("")]
        // GET: FindingDory2017
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