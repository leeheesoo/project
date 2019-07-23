using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Message.Services.Message {
    public interface ISmsService {
        /// <summary>
        /// SMS 발송
        /// </summary>
        /// <param name="phone">수신번호</param>
        /// <param name="callback">발신번호(펜타클기획자내선번호 or ING 고객센터 번호 등 사전에 등록된 전화번호)</param>
        /// <param name="msg">문자내용</param>
        /// <returns></returns>
        bool SendSMS(string phone, string callback, string msg);

        /// <summary>
        /// MMS 발송
        /// </summary>
        /// <param name="subject">제목</param>
        /// <param name="phone">수신번호</param>
        /// <param name="callback">발신번호(펜타클기획자내선번호 or ING 고객센터 번호 등 사전에 등록된 전화번호)</param>
        /// <param name="msg">문자내용</param>
        /// <param name="files">이미지url</param>
        /// <returns></returns>
        bool SendMMS(string subject, string phone, string callback, string msg, List<string> files);
    }
}
