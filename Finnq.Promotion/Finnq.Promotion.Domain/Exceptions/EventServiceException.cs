using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Exceptions {
    public class EventServiceException : Exception {
        public string Code { get; set; }
        public new object Data { get; set; }

        public EventServiceException(string code, string message, object data)
            : base(message) {

            Code = code;
            Data = data;
        }

        public EventServiceException(string message, object data) : base(message) {
            Data = data;
        }
    }
}
