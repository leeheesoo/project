using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.GoogleAnalyticsReportingServices;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Models.GoogleApisModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Admin {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [RoutePrefix("manager/ga")]
    public class AdminGoogleAnalyticsController : Controller {
        private IGoogleAnalyticsReportingService garService;
        private ICommonProvider common;
        public AdminGoogleAnalyticsController (IGoogleAnalyticsReportingService garService, ICommonProvider common) {
            this.garService = garService;
            this.common = common;
        }
        [Route("")]
        public ActionResult Index () {
            return View();
        }

        [Route("excel")]
        public void ExcelDownload(GoogleApisAnalyticsReportingRequestModel model) {
            var gaData = garService.GetData(model);
            var data = gaData.Select(x => new {
                날짜 = x.Date.ToString(),
                PV = x.PageViews,
                UV = x.UniquePageviews
            }).ToList();
            common.ExcelDownLoad(data, "GA데이터_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}