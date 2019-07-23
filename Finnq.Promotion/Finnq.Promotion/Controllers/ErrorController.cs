using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers {
    [RoutePrefix("error")]
    public class ErrorController : Controller {
        [Route("notfound")]
        public ActionResult NotFound() {
            return View();
        }
        [Route("unknown")]
        public ActionResult Unknown() {
            return View("Unknown");
        }
        [Route("internalservererror")]
        public ActionResult InternalServerError() {
            return View("InternalServerError");
        }
        [Route("forbidden")]
        public ActionResult Forbidden() {
            return View("Forbidden");
        }
    }
}