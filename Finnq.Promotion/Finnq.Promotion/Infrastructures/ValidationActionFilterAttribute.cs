using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Finnq.Promotion.Infrastructures {
    public class ValidationActionFilterAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
            var modelState = actionContext.ModelState;

            if (actionContext.ActionArguments.Any(kv => kv.Value == null)) {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Arguments cannot be null");
            }
            if (!modelState.IsValid) {
                var result = new InvalidRequestResult {
                    Message = "누락된 값이 있습니다."
                };
                result.ModelState = modelState.Where(x => x.Value.Errors.Any()).ToDictionary(x => x.Key, y => y.Value.Errors.Select(x => x.ErrorMessage).ToList());
                if (result.ModelState.Values.ToList().Any()) {
                    result = new InvalidRequestResult {
                        Message = result.ModelState.Values.FirstOrDefault()[0]
                    };
                }
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
        }
    }

    public class InvalidRequestResult {
        public string Message { get; set; }
        public Dictionary<string, List<string>> ModelState { get; set; }

        public void AddModelError(string key, string modelError) {
            if (ModelState == null) {
                ModelState = new Dictionary<string, List<string>>();
            }

            if (ModelState.ContainsKey(key)) {
                ModelState[key].Add(modelError);
            } else {
                var el = new List<string>();
                el.Add(modelError);
                ModelState[key] = el;
            }

        }
    }
}