using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Exceptions {
    public class EventServiceException : Exception {
        public string Code { get; set; }
        public object Data { get; set; }

        public EventServiceException(string code, string message, object data)
            : base(message) {

            Code = code;
            Data = data;
        }
    }
}
