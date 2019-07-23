using INGLife.Event.Domain.Infrastructures;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Models.KMCModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace INGLife.Event.Controllers.Api {
    [OnApiException]
    public class KMCController : ApiController {
        private ITimeProvider time;
        private IKMCService kmcService;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public KMCController(ITimeProvider time, IKMCService kmcService) {
            this.time = time;
            this.kmcService = kmcService;
        }
        /// <summary>
        /// Request KMC Service
        /// </summary>
        /// <returns></returns>
        public RequestKMCModel Get() {
            var date = time.Now.ToString("yyyyMMddHHmmss");
            var model = kmcService.RequestKMC(date, "003003", "https://test.www.orange-event.kr/kmc/callback");
            return model;
        }

        /// <summary>
        /// Response KMC Service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultMessage Post([FromBody]ResponseKMCModel model) {
            var result = new ResultMessage { Result = false, Message = "본인인증을 다시 해주시길 바랍니다." };
            try {
                result = kmcService.ResponseKMC(model);
                //db에 저장
            } catch (KMCServiceException e) {
                logger.Debug(">>>>>>>>>>> Response KMC Service");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
                result = new ResultMessage {
                    Result = false,
                    Message = string.Format("본인인증을 다시 해주시길 바랍니다. ({0})", e.Message)
                };
            } catch (Exception e) {
                logger.Error(e);
            }
            return result;
        }
    }
}
