using INGLife.Event.Message.Infrastructures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Message.Repositories.Message {
    public class SmsRepository: ISmsRepository {
        private SmsDbContext db;
        public SmsRepository(SmsDbContext db) {
            this.db = db;
        }
        /// <summary>
        /// SMS 발송
        /// </summary>
        /// <param name="phone">수신번호</param>
        /// <param name="callback">발신번호(펜타클기획자내선번호 or ING 고객센터 번호 등 사전에 등록된 전화번호)</param>
        /// <param name="msg">문자내용</param>
        /// <returns></returns>
        public bool SendSMS(string phone, string callback, string msg) {
            var result = db.Database.ExecuteSqlCommand("INSERT INTO SC_TRAN (TR_SENDDATE, TR_SENDSTAT, TR_MSGTYPE, TR_PHONE, TR_CALLBACK, TR_MSG) VALUES( GETDATE(), '0', '0', {0}, {1}, {2})", phone, callback, msg);
            return (result  > 0 ? true : false);
        }

        /// <summary>
        /// MMS 발송
        /// </summary>
        /// <param name="subject">제목</param>
        /// <param name="phone">수신번호</param>
        /// <param name="callback">발신번호(펜타클기획자내선번호 or ING 고객센터 번호 등 사전에 등록된 전화번호)</param>
        /// <param name="msg">문자내용</param>
        /// <param name="files">이미지url</param>
        /// <returns></returns>
        public bool SendMMS(string subject, string phone, string callback, string msg, List<string> files) {
            var result = db.Database.ExecuteSqlCommand(@"INSERT INTO MMS_MSG (SUBJECT, PHONE, CALLBACK, STATUS, REQDATE, MSG, FILE_CNT, FILE_PATH1, TYPE) 
                                                        VALUES({0}, {1}, {2}, '0', GETDATE(), {3}, {4}, {5}, '0')",
                                                        subject, phone, callback, msg, files.Count, (files != null && files.Count > 0) ? files[0] : null);
            return (result > 0 ? true : false);
        }
    }
}
