using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers
{
    [RoutePrefix("roulette")]
    public class RouletteEventController : Controller
    {
        [Route("")]
        // GET: Roulette
        public ActionResult Index()
        {
            return View();
        }
    }
}