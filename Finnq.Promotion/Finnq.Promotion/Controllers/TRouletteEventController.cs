using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers
{
    [RoutePrefix("tmembership")]
    public class TRouletteEventController : Controller
    {
        [Route("event01")]
        // GET: TRouletteEvent
        public ActionResult Index()
        {
            return View();
        }
    }
}