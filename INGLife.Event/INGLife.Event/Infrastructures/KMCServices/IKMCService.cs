using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Infrastructures.KMCServices {
    public interface IKMCService {
        /// <summary>
        /// KMC 본인인증서비스 Request
        /// </summary>
        /// <param name="date">날짜</param>
        /// <param name="urlCode">url 등록하고 생성되는 urlcode</param>
        /// <param name="trUrl">callback url</param>
        /// <returns></returns>
        RequestKMCModel RequestKMC(string date, string urlCode, string trUrl);

        RequestKMCModel RequestPlusInfoKMC(string date, string urlCode, string trUrl, string plusInfo);
        /// <summary>
        /// KMC 본인인증서비스 Response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultMessage ResponseKMC(ResponseKMCModel model);
    }
}
