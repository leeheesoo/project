using INGLife.Event.Infrastructures.Interfaces;
using System;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event {
    [RoutePrefix("nilririmanbo")]
    public class NilririmanboEventController : Controller {
        private ICommonProvider common;

        public NilririmanboEventController (ICommonProvider common) {
            this.common = common;
        }

        [Route("")]
        // GET: KidsNote
        public ActionResult Index () {
            ViewBag.IsOpen = IsEventOpen();
            ViewBag.IsClose = IsEventClose();
            return View();
        }
        // 이벤트 오픈일
        private bool IsEventOpen () {
            return common.Now >= new DateTime(2018, 6, 13);
        }
        // 이벤트 종료일
        private bool IsEventClose () {
            return common.Now >= new DateTime(2018, 7, 1);
        }
    }
}