using INGLife.Event.Infrastructures.Interfaces;
using System.Web.Mvc;

namespace INGLife.Event.Controllers {
    [RoutePrefix("")]
    public class HomeController : Controller {
        private ICommonProvider common;
        public HomeController(ICommonProvider common) {
            this.common = common;
        }
        [Route("")]
        public ActionResult Index() {
            return RedirectToAction("Index", "Orange4050");
        }
    }
}