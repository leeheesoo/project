using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.GoogleAnalyticsReportingServices;
using INGLife.Event.Models.GoogleApisModels;
using System.Collections.Generic;
using System.Web.Http;

namespace INGLife.Event.Controllers.Api {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [OnApiException]
    [ValidationActionFilter]
    public class GoogleApisAnalyticsReportingController : ApiController {
        private IGoogleAnalyticsReportingService garService;
        public GoogleApisAnalyticsReportingController(IGoogleAnalyticsReportingService garService) {
            this.garService = garService;
        }

        [HttpGet]
        public List<GoogleApisAnalyticsReportingResponseModel> Get([FromUri]GoogleApisAnalyticsReportingRequestModel model) {
            return garService.GetData(model);
        }
    }
}
