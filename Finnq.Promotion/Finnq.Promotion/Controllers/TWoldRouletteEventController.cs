using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers
{
    [RoutePrefix("tworld/event01")]
    public class TWoldRouletteEventController : Controller
    {
        [Route("")]
        // GET: TWoldRouletteEvent
        public ActionResult Index()
        {
            return View();
        }
    }
}