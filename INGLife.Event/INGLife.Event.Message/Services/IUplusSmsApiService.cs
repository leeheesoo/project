using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Message.Services {
    public interface IUplusSmsApiService {
        bool SendMMS(string subject, string phone, string callback, string msg, string msgType);
    }
}
