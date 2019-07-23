using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Word;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TweetSharp;

namespace KinderJoy.Site.Controllers
{
    [OutputCache(Duration = 0, NoStore = true)]
    [RedirectToMobile]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("m")]
        public ActionResult Index(string type="")
        {
            if(type.Equals("main")) {
                return View();
            }

            NameValueCollection queryString = Request.QueryString;
            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true) {
                foreach (string key in queryString.AllKeys) {
                    route.Add(key, queryString[key]);
                }
            }
            return new RedirectResult(Url.Action("Gate", route));

        }

        [Route("About")]
        [Route("m/About")]
        public ActionResult About()
        {
            return View();
        }

        [Route("Flavor")]
        [Route("m/Flavor")]
        public ActionResult Flavor()
        {
            return View();
        }

        [Route("Together")]
        [Route("m/Together")]
        public ActionResult Together()
        {
            return View();
        }

        [Route("Image")]
        [Route("m/Image")]
        public ActionResult Image()
        {
            return View();
        }

        [Route("Image/Track/{Source}/{Media}/{Content}")]
        [Route("m/Image/Track/{Source}/{Media}/{Content}")]
        public ActionResult Track(string Source, string Media, string Content)
        {
            NameValueCollection queryString = Request.QueryString;
            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true)
            {
                foreach (string key in queryString.AllKeys)
                {
                    route.Add(key, queryString[key]);
                }
            }
            route.Add("utm_source", Source);
            route.Add("utm_medium", Media);
            route.Add("utm_content", Content);
            route.Add("utm_campaign", "kinderjoy-toy");
            bool isMobile = Request.Browser.IsMobileDevice;
            string url = Url.Action("Image", "Home", route);
            if (isMobile)
            {
                url = "/m" + url;
            }
            return new RedirectResult(url);
        }

        [Route("Image/TwitterShare/{toyFlag}")]
        [Route("m/Image/TwitterShare/{toyFlag}")]
        [HttpPost]
        public ActionResult TwitterShare(string toyFlag)
        {
            WordTwitterResult result = new WordTwitterResult
            {
                Result = false,
                Message = "페이지 새로고침 후 다시 시도해 주세요",
                PostId = "",
                SnsId = ""
            };
            try
            {
                string accessToken = (string)(Session["twitter_accessToken"]);
                string aceessTokenSecret = (string)(Session["twitter_accessTokenSecret"]);
                var userId = Session["twitter_id"];
                string userName = (string)(Session["twitter_name"]);
                string userNickname = (string)(Session["twitter_nick"]);
                bool isValid = accessToken == null || aceessTokenSecret == null || userId == null || userName == null || userNickname == null;

                if (isValid)
                {
                    result.Message = "트위터를 다시 로그인 해주세요";
                    return Json(result);
                }

                TwitterService service = new TwitterService(ConfigurationManager.AppSettings["sns.twitter.consumerkey"], ConfigurationManager.AppSettings["sns.twitter.consumersceret"]);
                service.AuthenticateWith(accessToken, aceessTokenSecret);

                string scrapInfo = "#킨더조이 쿵푸팬더와 함께 우리아이 새학기를 응원하세요! 지금, 아이와 함께 킨더조이 속 장난감을 확인해보세요! https://www.kinderjoy.co.kr/Image/Track/toyPage/twitter/" + toyFlag;
                if (!toyFlag.Equals("new")) {
                    scrapInfo = "[킨더조이의 상상하는 즐거움!] 지금, 아이와 함께 킨더조이 속 장난감을 확인해보세요! https://www.kinderjoy.co.kr/Image/Track/toyPage/twitter/" + toyFlag;
                }

                TwitterStatus twitterStatus = service.SendTweet(new SendTweetOptions
                {
                    Status = scrapInfo
                });

                result.Result = true;
                result.Message = "정상적으로 처리 되었습니다.";
                result.SnsId = userNickname;
                result.PostId = twitterStatus.IdStr;

            }
            catch (Exception)
            {
                result.Result = false;
                result.Message = "트위터는 하루에 한번만 공유 가능합니다.";
            }
            return Json(result);
        }

        [Route("Image_art")]
        [Route("m/Image_art")]
        public ActionResult Image_art()
        {
            return View();
        }

        [Route("Image_focus")]
        [Route("m/Image_focus")]
        public ActionResult Image_focus()
        {
            return View();
        }

        [Route("ToyView")]
        [Route("m/ToyView")]
        public ActionResult ToyView()
        {
            return View();
        }

        [Route("TvCF")]
        [Route("m/TvCF")]
        public ActionResult TvCF()
        {
            return View();
        }

        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy() {
            return View();
        }

        [Route("gate")]
        [Route("m/gate")]
        public ActionResult Gate() {
            return View();
        }
    }
}