using System;

namespace Samsonite.Mall.Domain.Exceptions {
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
