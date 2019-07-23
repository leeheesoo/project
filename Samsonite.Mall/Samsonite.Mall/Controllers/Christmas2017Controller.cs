using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers
{
    [RoutePrefix("2017-christmas")]
    public class Christmas2017Controller : Controller
    {
        [Route("")]
        // GET: Christmas2017
        public ActionResult Index()
        {
            return View();
        }
    }
}