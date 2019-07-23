using INGLife.Event.Message.Repositories.Message;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Message.Services.Message {
    public class SmsService : ISmsService {
        private ISmsRepository repository;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public SmsService(ISmsRepository repository) {
            this.repository = repository;
        }
        public bool SendSMS(string phone, string callback, string msg) {
            try {
                return repository.SendSMS(phone, callback, msg);
            } catch (Exception e) {
                logger.Error("[Sms Service] Send SMS error: " + e.Message);
                return false;
            }
        }

        public bool SendMMS(string subject, string phone, string callback, string msg, List<string> files) {
            try {
                return repository.SendMMS(subject, phone, callback, msg, files);
            } catch (Exception e) {
                logger.Error("[Sms Service] Send MMS error: " + e.Message);
                return false;
            }
        }
    }
}
