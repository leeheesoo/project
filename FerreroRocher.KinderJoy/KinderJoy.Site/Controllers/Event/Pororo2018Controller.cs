using AutoMapper;
using KinderJoy.Domain.Entities.Pororo2018;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Service.Pororo2018;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Pororo2018;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KinderJoy.Site.Controllers.Event
{
	[RoutePrefix("event/2018-pororo")]
	public class Pororo2018Controller : Controller
    {
        private IPororo2018Service service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;

        public Pororo2018Controller(IPororo2018Service service,ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Pororo2018Model, Pororo2018InstantLottery>();
            });
        }

        [Route("")]
        // GET: Pororo2018
        public ActionResult Index()
        {
            ViewBag.IsShowInstantLottery = false;

            string instantLotteryPop = Session["PororoPop"] as string;

            if (!string.IsNullOrEmpty(instantLotteryPop) && instantLotteryPop.Equals("SHOW")) {
                ViewBag.IsShowInstantLottery = true;
            }
            Session["PororoPop"] = null;

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
            Session["PororoStatus"] = "START";

            if (common.Now >= new DateTime(2018, 2, 12) || common.Now < new DateTime(2018, 1, 17)) {
                Session["PororoStatus"] = "FAIL";
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
        
            if (common.Now >= new DateTime(2018, 2, 12) || common.Now < new DateTime(2018, 1, 17)) {
                Session["PororoPop"] = "CLOSE";
            } else {
                Session["PororoPop"] = "SHOW";
            }
            NameValueCollection queryString = Request.QueryString;

            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true) {
                foreach (string key in queryString.AllKeys) {
                    if (key != null) {
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
                string status = Session["PororoStatus"] as string;
                bool isChecked = false;

                string cookieName = "POROROINSTANTLOTTERY";
                if (!string.IsNullOrEmpty(status) && status.Equals("START") && Request.Cookies[cookieName] == null) {
                    isChecked = true;
                }
                Session["PororoStatus"] = null;

                var entry = service.CreateInstantLottery(new Pororo2018InstantLottery {
                    Channel = common.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC,
                    PrizeType = Pororo2018InstantLotteryPrizeType.Loser,
                    IpAddress = common.ClientIP
                }, isChecked);

                if (entry.PrizeType != Pororo2018InstantLotteryPrizeType.Loser) {
                    //쿠키 생성
                    HttpCookie eventCookie = new HttpCookie(cookieName);
                    eventCookie.Value = "Y";
                    eventCookie.Expires = new DateTime(2018, 2, 12);
                    eventCookie.Secure = true;
                    Response.Cookies.Add(eventCookie);
                }

                var model = new Pororo2018InstantLotteryViewModel {
                    Id = entry.Id,
                    PrizeName = "wpqowreorrto",
                    PrizeType = entry.PrizeType
                };

                switch (entry.PrizeType) {
                    case Pororo2018InstantLotteryPrizeType.JoyGifticon:
                        model.PrizeName = "wpqowreorrto";
                        break;
                    case Pororo2018InstantLotteryPrizeType.DIYPack:
                        model.PrizeName = "wcarsodnfg";
                        break;
                    case Pororo2018InstantLotteryPrizeType.MarkerPen:
                        model.PrizeName = "wpzaxtctvy";
                        break;
                    case Pororo2018InstantLotteryPrizeType.Sketchbook:
                        model.PrizeName = "wrquwpeere";
                        break;
                    default:
                        model.PrizeName = "wpqowreorrto";
                        break;
                }

                Session["PororoEntryId"] = entry.Id;
                Session["PororoPrizeType"] = entry.PrizeType;

                return Json(new {
                    result = true,
                    data = model
                });
            } catch (Exception e) {
                var message = e.Message;
                if(!(e is EventServiceException)) {
                    message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";
                }
                return Json(new {
                    result = false,
                    message = message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update-winner", Name = "Pororo2018UpdateWinner")]
        public ActionResult Pororo2018UpdateWinner(Pororo2018Model model) {
            try {
                if (common.Now >= new DateTime(2018, 2, 12)) {
                    throw new Pororo2018ServiceException("400", "이벤트가 종료되었습니다.", null);
                }
                // 당첨자 세션 check
                long? entryId = Session["PororoEntryId"] as long?;
                Pororo2018InstantLotteryPrizeType? prize = Session["PororoPrizeType"] as Pororo2018InstantLotteryPrizeType?;

                if (!entryId.HasValue || entryId.Value != model.Id) {
                    throw new Pororo2018ServiceException("400", "당첨자가 아닙니다.", model.Id);
                }
                if (!prize.HasValue || prize.Value != model.PrizeType) {
                    throw new Pororo2018ServiceException("400", "해당 경품 당첨자가 아닙니다.", model.PrizeType);
                }
                if (!ModelState.IsValid) {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null) {
                        throw new Pororo2018ServiceException("400",errorProp.Errors[0].ErrorMessage,null);
                    }
                }
                var user = service.GetInstantLotteryWinner(model.Id);
                var entity = mapperConfig.CreateMapper().Map<Pororo2018Model, Pororo2018InstantLottery>(model, user);
                entity.UpdateDate = common.Now;
                service.UpdateInstantLotteryWinner(entity);

                return Json(new {
                    result = true
                });
            } catch (Exception e) {
                var message = e.Message;
                if (!(e is EventServiceException)) {
                    message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";
                }
                return Json(new {
                    result = false,
                    message = message
                });
            }
        }
    }
}