using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event {
    /// <summary>
    /// 새학기 이벤트 (빙고퀴즈)
    /// </summary>
    [RoutePrefix("event/2016-backtoschool")]
    public class BackToSchool2016Controller : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }        
    }
}