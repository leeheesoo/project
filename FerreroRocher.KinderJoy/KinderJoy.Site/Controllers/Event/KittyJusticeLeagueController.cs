using AutoMapper;
using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Service.KittyJusticeLeague;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.KittyJusticeleage;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KinderJoy.Site.Controllers.Event
{
    [RoutePrefix("event/2017-HK_JL")]
    public class KittyJusticeLeagueController : Controller
    {
        private IKittyJusticeLeagueService service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;

        public KittyJusticeLeagueController(IKittyJusticeLeagueService service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<KittyJusticeLeagueModel, KittyJusticeLeagueInstantLottery>();
            });
        }

        [Route("")]
        // GET: KittyJusticeLeague
        public ActionResult Index()
        {
            ViewBag.IsShowInstantLottery = false;
         
            string instantLotteryPop = Session["KittyJusticeLeaguePop"] as string;

            if (!string.IsNullOrEmpty(instantLotteryPop) && instantLotteryPop.Equals("SHOW")) {
                ViewBag.IsShowInstantLottery = true;
            }
            Session["KittyJusticeLeaguePop"] = null;

            return View();
        }
        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy() {
            return View();
        }
        /// <summary>
        /// 즉석당첨 : 네이버 사이트로 리다이렉트
        /// </summary>
        [Route("naver-landing")]
        public ActionResult InstantLotteryLanding() {
            Session["KittyJusticeLeagueStatus"] = "START";
            if (common.Now >= new DateTime(2017, 12, 18) || common.Now < new DateTime(2017, 11, 20)) {
                Session["KittyJusticeLeagueStatus"] = "FAIL";
            }

            if (Request.Browser.IsMobileDevice) {
                return new RedirectResult("http://m.naver.com/");
            } else {
                return new RedirectResult("http://www.naver.com/");
            }
        }

        // <summary>
        /// [즉석당첨 이벤트] 네이버에서 유입
        /// </summary>
        [Route("naver-searching")]
        [HttpGet]
        public ActionResult NaverSearching() {
            if (common.Now >= new DateTime(2017, 12, 18) || common.Now < new DateTime(2017, 11, 20)) {
                Session["KittyJusticeLeaguePop"] = "CLOSE";
            } else {
                Session["KittyJusticeLeaguePop"] = "SHOW";
            }
            NameValueCollection queryString = Request.QueryString;
            
            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true) {
                foreach (string key in queryString.AllKeys) {
                    if(key != null) {
                        route.Add(key, queryString[key]);
                    }
                }
            }
            return new RedirectResult(Url.Action("Index", route));
        }
        // <summary>
        /// [즉석당첨 이벤트] 경품 로직
        /// </summary>
        [Route("instant-lottery")]
        [HttpPost]
        public ActionResult InstantLottery() {
            try {
                string status = Session["KittyJusticeLeagueStatus"] as string;
                bool isChecked = false;

                string cookieName = "KITTYJUSTICELEAGUEINSTANTLOTTERY";
                if (!string.IsNullOrEmpty(status) && status.Equals("START") && Request.Cookies[cookieName] == null) {
                    isChecked = true;
                }
                Session["DietCenter_InstantLottery_Status"] = null;

                var entry = service.CreateInstantLottery(new KittyJusticeLeagueInstantLottery {
                    Channel = common.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC,
                    PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser,
                    IpAddress = common.ClientIP
                }, isChecked);

                if (entry.PrizeType != KittyJusticeLeagueInstantLotteryPrizeType.Loser) {
                    //쿠키 생성
                    HttpCookie eventCookie = new HttpCookie(cookieName);
                    eventCookie.Value = "Y";
                    eventCookie.Expires = new DateTime(2017, 12, 18);
                    eventCookie.Secure = true;
                    Response.Cookies.Add(eventCookie);
                }

                var model = new KittyJusticeLeagueInstantLotteryViewModel {
                    Id = entry.Id,
                    PrizeName = "waqpwpelre",
                    PrizeType = entry.PrizeType
                };

                switch (entry.PrizeType) {
                    case KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon:
                        model.PrizeName = "waqpwpelre";
                        break;
                    case KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock:
                        model.PrizeName = "wlaisodn";
                        break;
                    case KittyJusticeLeagueInstantLotteryPrizeType.JusticeLeagueNanoBlock:
                        model.PrizeName = "wjaishdyfe";
                        break;
                    case KittyJusticeLeagueInstantLotteryPrizeType.ChirstmasTree:
                        model.PrizeName = "wyzexcchvabn";
                        break;
                    case KittyJusticeLeagueInstantLotteryPrizeType.ChristmasCard:
                        model.PrizeName = "wyloouvnea";
                        break;
                    default:
                        model.PrizeName = "waqpwpelre";
                        break;
                }

                Session["KittyJusticeLeagueEntryId"] = entry.Id;
                Session["KittyJusticeLeaguePrizeType"] = entry.PrizeType;

                return Json(new {
                    result = true,
                    data = model
                });
            } catch (Exception e) {
                return Json(new {
                    result = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [Route("update-winner", Name = "KittyJusticeLeagueUpdateWinner")]
        public ActionResult KittyJusticeLeagueUpdateWinner(KittyJusticeLeagueModel model) {
            try {
                if(common.Now >= new DateTime(2017, 12, 18)) {
                    throw new KittyJusticeLeagueServiceException("400", "이벤트가 종료되었습니다.", null);
                }
                // 당첨자 세션 check
                long? entryId = Session["KittyJusticeLeagueEntryId"] as long?;
                KittyJusticeLeagueInstantLotteryPrizeType? prize = Session["KittyJusticeLeaguePrizeType"] as KittyJusticeLeagueInstantLotteryPrizeType?;

                if (!entryId.HasValue || entryId.Value != model.Id) {
                    throw new KittyJusticeLeagueServiceException("400", "당첨자가 아닙니다.", model.Id);
                }
                if (!prize.HasValue || prize.Value != model.PrizeType) {
                    throw new KittyJusticeLeagueServiceException("400", "해당 경품 당첨자가 아닙니다.", model.PrizeType);
                }

                var user = service.GetInstantLotteryWinner(model.Id);
                var entity = mapperConfig.CreateMapper().Map<KittyJusticeLeagueModel, KittyJusticeLeagueInstantLottery>(model, user);
                entity.UpdateDate = common.Now;
                service.UpdateInstantLotteryWinner(entity);

                return Json(new {
                    result = true
                });
            } catch (Exception e) {
                return Json(new {
                    result = false,
                    message = e.Message
                });
            }
        }
    }
}