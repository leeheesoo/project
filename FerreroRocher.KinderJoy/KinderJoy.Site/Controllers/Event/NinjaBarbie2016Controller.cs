using KinderJoy.Domain.Service.NinjaBarbie2016;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event {
    /// <summary>
    /// 2016 닌자바비 이벤트
    /// </summary>
    [RoutePrefix("event/2016-ninjabarbie")]
    public class NinjaBarbie2016Controller : Controller {

        private INinjaBarbie2016Service service;
        private ICommonProvider common;

        public NinjaBarbie2016Controller(INinjaBarbie2016Service service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        
        [Route("")]
        [Route("{userId}")]
        public ActionResult Index(long? userId) {
            if (userId.HasValue) {
                var user = service.GetUsers().Where(x => x.Id == userId.Value && x.KakaostoryImage != null).SingleOrDefault();
                if (user != null) {
                    ViewBag.UserId = user.Id;
                    ViewBag.OgImage = user.FacebookImage;
                }
            }
            ViewBag.DateTimeNow = common.Now;
            return View();
        }

        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy() {
            return View();
        }
    }
}