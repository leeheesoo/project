using INGLife.Event.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace INGLife.Event.Infrastructures {
    public class OnApiExceptionAttribute : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";

            if (actionExecutedContext.Exception is EventServiceException) {
                var e = actionExecutedContext.Exception as EventServiceException;
                var result = new EventServiceExceptionResult {
                    message = actionExecutedContext.Exception.Message ,
                    code = e.Code ,
                    data = e.Data
                };
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest , result);
            } else {

                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode , message);
            }
        }
    }

    public class EventServiceExceptionResult {
        public string message { get; set; }
        public string code { get; set; }
        public object data { get; set; }
    }
}