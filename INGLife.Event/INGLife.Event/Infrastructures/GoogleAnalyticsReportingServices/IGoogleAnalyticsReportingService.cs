using INGLife.Event.Models.GoogleApisModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Infrastructures.GoogleAnalyticsReportingServices {
    public interface IGoogleAnalyticsReportingService {
        List<GoogleApisAnalyticsReportingResponseModel> GetData(GoogleApisAnalyticsReportingRequestModel model);
    }
}
