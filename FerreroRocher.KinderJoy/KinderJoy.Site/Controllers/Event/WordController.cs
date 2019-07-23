using KinderJoy.Domain.Entities.Word;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Infrastructure.Word;
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

namespace KinderJoy.Site.Controllers.Event {
    [OutputCache(Duration = 0, NoStore = true)]
    [RedirectToMobile]
    [RoutePrefix("")]
    public class WordController : Controller {
        private IWordService service;

        public WordController(IWordService service) {
            this.service = service;
        }

        [Route("Event/Word")]
        [Route("m/Event/Word")]
        public ActionResult Index() {
            bool isEventDay = DateTime.Today <= new DateTime(2015, 9, 10);
            ViewBag.isEventDay = isEventDay;
            ViewBag.WinnerLastSt = 4;//service.CalcWinnerLastSt();
            return View();
        }

        [Route("Event/Word/Entry")]
        [Route("m/Event/Word/Entry")]
        [HttpPost]
        public JsonResult Entry(WordEntry entry) {
            WordResult result = new WordResult {
                Result = false,
                Message = ""
            };
            try {
                WordEvent wordEvent = service.EntryWord(entry, Request.ServerVariables["REMOTE_ADDR"]);
                Session["Word_Id"] = wordEvent.Id;
                result.Result = true;
                result.Message = "정상적으로 처리 되었습니다.";
            } catch (Exception e) {
                result.Result = false;
                result.Message = e.Message;
            }
            return Json(result);
        }

        [Route("Event/Word/Share")]
        [Route("m/Event/Word/Share")]
        [HttpPost]
        public JsonResult Share(WordShare entry) {
            WordResult result = new WordResult {
                Result = false,
                Message = ""
            };

            try {
                entry.WordEventId = (int)Session["Word_Id"];
                entry.Ip = Request.ServerVariables["REMOTE_ADDR"];
                service.ShareWord(entry);
                result.Result = true;
                result.Message = "정상적으로 처리 되었습니다.";
            } catch (Exception) {
                result.Result = false;
                result.Message = "쿠키가 만료 되었습니다. 페이지 새로고침 후 다시 시도해 주세요";
            }

            return Json(result);
        }

        [Route("Event/Word/TwitterShare")]
        [Route("m/Event/Word/TwitterShare")]
        [HttpPost]
        public ActionResult TwitterShare() {
            WordTwitterResult result = new WordTwitterResult {
                Result = false,
                Message = "페이지 새로고침 후 다시 시도해 주세요",
                PostId = "",
                SnsId = ""
            };
            try {
                string accessToken = (string)(Session["twitter_accessToken"]);
                string aceessTokenSecret = (string)(Session["twitter_accessTokenSecret"]);
                var userId = Session["twitter_id"];
                string userName = (string)(Session["twitter_name"]);
                string userNickname = (string)(Session["twitter_nick"]);
                bool isValid = accessToken == null || aceessTokenSecret == null || userId == null || userName == null || userNickname == null;

                if (isValid) {
                    result.Message = "트위터를 다시 로그인 해주세요";
                    return Json(result);
                }

                TwitterService service = new TwitterService(ConfigurationManager.AppSettings["sns.twitter.consumerkey"], ConfigurationManager.AppSettings["sns.twitter.consumersceret"]);
                service.AuthenticateWith(accessToken, aceessTokenSecret);

                TwitterStatus twitterStatus = service.SendTweet(new SendTweetOptions {
                    Status = "[킨더조이 TVCF 퀴즈 이벤트] 빈칸의 정답 입력하고 매주 추첨을 통해 2,000명에게 드리는 킨더조이 선물 받아가세요! http://goo.gl/zHuS7v #킨더조이 "
                });

                result.Result = true;
                result.Message = "정상적으로 처리 되었습니다.";
                result.SnsId = userNickname;
                result.PostId = twitterStatus.IdStr;

            } catch (Exception) {
                result.Result = false;
                result.Message = "트위터는 하루에 한번만 공유 가능합니다.";
            }
            return Json(result);
        }

        [Route("Event/Word/Track")]
        [Route("m/Event/Word/Track")]
        public ActionResult Track(string Source, string Media) {
            NameValueCollection queryString = Request.QueryString;
            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true) {
                foreach (string key in queryString.AllKeys) {
                    route.Add(key, queryString[key]);
                }
            }
            route.Add("utm_source", Source);
            route.Add("utm_medium", Media);
            route.Add("utm_campaign", "Word");
            bool isMobile = Request.Browser.IsMobileDevice;
            string url = Url.Action("Index", "Word", route);
            if (isMobile) {
                url = "/m" + url;
            }
            return new RedirectResult(url);
        }

        [Route("Event/Word/IsWinnerResultSt")]
        [Route("m/Event/Word/IsWinnerResultSt")]
        public JsonResult IsWinnerResultSt(int St) {
            WordResult result = new WordResult {
                Result = false,
                Message = ""
            };
            result.Result = service.IsWinnerResultSt(St);
            return Json(result);
        }

        [Route("Event/Word/WinnerResult")]
        [Route("m/Event/Word/WinnerResult")]
        public ActionResult WinnerResult(int St, string GiftType) {
            return View(service.GetWordWinners(St, GiftType));
        }
    }
}