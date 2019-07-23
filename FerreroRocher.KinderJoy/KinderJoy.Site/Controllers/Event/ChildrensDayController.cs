using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event
{
    /// <summary>
    /// 어린이날 이벤트(숨은 그림 찾기)
    /// </summary>
    [RoutePrefix("event/2016-childrens-day")]
    public class ChildrensDayController : Controller
    {
        [Route("")]
        // GET: ChildrensDay
        public ActionResult Index()
        {
            return View();
        }
    }
}