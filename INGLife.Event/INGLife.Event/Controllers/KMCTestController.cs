using INGLife.Event.Domain.Infrastructures;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Models.KMCModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace INGLife.Event.Controllers {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [RoutePrefix("kmc")]
    public class KMCTestController : Controller {
        private ITimeProvider time;
        private IKMCService kmcService;

        public KMCTestController(ITimeProvider time, IKMCService kmcService) {
            this.time = time;
            this.kmcService = kmcService;
        }
        /// <summary>
        /// 본인확인 서비스 요청 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index() {
            var date = time.Now.ToString("yyyyMMddHHmmss");
            var model = kmcService.RequestKMC(date, "003003", "https://test.www.orange-event.kr/kmc/callback");
            return View(model);
        }
        
        /// <summary>
        /// 본인확인 서비스 요청 결과 수신
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("callback")]
        public ActionResult Callback(ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }
    }
    public class SendSMSModel {
        public string Phone { get; set; }
        public string Msg { get; set; }
    }

    public class SendMMSModel : SendSMSModel {
        public string Subject { get; set; }
        public List<string> Files { get; set; }
    }
}