
using KinderJoy.Domain.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace KinderJoy.Site.Infrastructure {
    public class OnApiExceptionAttribute : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";

            if (actionExecutedContext.Exception is EventServiceException) {
                var e = actionExecutedContext.Exception as EventServiceException;
                var result = new EventServiceExceptionResult {
                    Message = actionExecutedContext.Exception.Message,
                    Code = e.Code,
                    Data = e.Data
                };
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, result);
            } else {

                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode, message);
            }
        }
    }

    public class EventServiceExceptionResult {
        public string Message { get; set; }
        public string Code { get; set; }
        public object Data { get; set; }
    }
}