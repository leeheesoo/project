using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Infrastructures {
    public class SendMessage {
        public bool SendSMS(string phone, string callback, string msg) {
            return true;
        }

        public bool SendMMS(string subject, string phone, string callback, string msg, List<string> files) {
            return true;
        }
    }
}
