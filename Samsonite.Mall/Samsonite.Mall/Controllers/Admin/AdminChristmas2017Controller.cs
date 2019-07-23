using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2017-christmas")]
    public class AdminChristmas2017Controller : Controller
    {
        [Route("")]
        // GET: AdminChristmas2017
        public ActionResult Index()
        {
            return View();
        }
    }
}