using AutoMapper;
using KinderJoy.Domain.Entities.DisneyStarWars2018;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Service.DisneyStarWars2018;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.DisneyStarWars2018;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KinderJoy.Site.Controllers.Event
{
    [RoutePrefix("event/2018disney-starwars")]
    public class DisneyStarWars2018Controller : Controller
    {
        private IDisneyStarWars2018Service service;
        private ICommonProvider common;

        private DateTime startDate = new DateTime(2018,5,10);
        //private DateTime startDate = new DateTime(2018, 5, 8);
        private DateTime endDate = new DateTime(2018, 5, 31);


        private MapperConfiguration mapperConfig;

        public DisneyStarWars2018Controller(IDisneyStarWars2018Service service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<DisneyStarWars2018Model, DisneyStarWars2018InstantLottery>();
            });
        }

        [Route("")]
        public ActionResult Index()
        {
            //팝업 제어 변수
            ViewBag.IsShowInstantLottery = false;            

            string instantLotteryPop = Session["DisneyStarWarsPop"] as string;

            if (!string.IsNullOrEmpty(instantLotteryPop) && instantLotteryPop.Equals("SHOW"))
            {
                ViewBag.IsShowInstantLottery = true;
            }
            Session["DisneyStarWarsPop"] = null;

            return View();
        }

        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        /// <summary>
        /// 즉석당첨 : 네이버 사이트로 리다이렉트
        /// </summary>
        [Route("naver-landing")]
        public ActionResult InstantLotteryLanding()
        {
            Session["DisneyStarWarsStatus"] = "START";

            if (common.Now >= endDate || common.Now < startDate)
            {
                Session["DisneyStarWarsStatus"] = "FAIL";
            }

            if (Request.Browser.IsMobileDevice)
            {
                return new RedirectResult("http://m.naver.com/");
            }
            else
            {
                return new RedirectResult("http://www.naver.com/");
            }
        }

        // <summary>
        /// [즉석당첨 이벤트] 네이버에서 유입
        /// </summary>
        [Route("naver-searching")]
        [HttpGet]
        public ActionResult NaverSearching()
        {

            if (common.Now >= endDate || common.Now < startDate)
            {
                Session["DisneyStarWarsPop"] = "CLOSE";
            }
            else
            {
                Session["DisneyStarWarsPop"] = "SHOW";
            }
            NameValueCollection queryString = Request.QueryString;

            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true)
            {
                foreach (string key in queryString.AllKeys)
                {
                    if (key != null)
                    {
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
        public ActionResult InstantLottery()
        {
            try
            {
                string status = Session["DisneyStarWarsStatus"] as string;
                bool isChecked = false;

                string cookieName = "DisneyStarWarsINSTANTLOTTERY";
                if (!string.IsNullOrEmpty(status) && status.Equals("START") && Request.Cookies[cookieName] == null)
                {
                    isChecked = true;
                }
                Session["DisneyStarWarsStatus"] = null;

                var entry = service.CreateInstantLottery(new DisneyStarWars2018InstantLottery
                {
                    Channel = common.IsMobileDevice ? Domain.Abstract.ChannelType.Mobile : Domain.Abstract.ChannelType.PC,
                    PrizeType = DisneyStarWars2018InstantLotteryPrizeType.Loser,
                    IpAddress = common.ClientIP
                }, isChecked);

                if (entry.PrizeType != DisneyStarWars2018InstantLotteryPrizeType.Loser)
                {
                    //쿠키 생성
                    HttpCookie eventCookie = new HttpCookie(cookieName);
                    eventCookie.Value = "Y";
                    eventCookie.Expires = endDate;
                    eventCookie.Secure = true;
                    Response.Cookies.Add(eventCookie);
                }

                var model = new DisneyStarWars2018InstantLotteryViewModel
                {
                    Id = entry.Id,
                    PrizeName = "",
                    PrizeType = entry.PrizeType
                };

                switch (entry.PrizeType)
                {
                    case DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty:
                        model.PrizeName = "zgxicfvtbyn";
                        break;
                    case DisneyStarWars2018InstantLotteryPrizeType.DisneyQuenMirror:
                        model.PrizeName = "amsidrfrgohrj";
                        break;
                    case DisneyStarWars2018InstantLotteryPrizeType.StarWarsCupSet:
                        model.PrizeName = "gchujpkl";
                        break;                    
                    default:
                        model.PrizeName = "";
                        break;
                }

                Session["DisneyStarWarsEntryId"] = entry.Id;
                Session["DisneyStarWarsPrizeType"] = entry.PrizeType;

                return Json(new
                {
                    result = true,
                    data = model
                });
            }
            catch (Exception e)
            {
                var message = e.Message;
                if (!(e is EventServiceException))
                {
                    message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";
                }
                return Json(new
                {
                    result = false,
                    message = message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update-winner", Name = "DisneyStarWars2018UpdateWinner")]
        public ActionResult DisneyStarWars2018UpdateWinner(DisneyStarWars2018Model model)
        {
            try
            {
                if (common.Now >= endDate)
                {
                    throw new DisneyStarWars2018ServiceException("400", "이벤트가 종료되었습니다.", null);
                }
                // 당첨자 세션 check
                long? entryId = Session["DisneyStarWarsEntryId"] as long?;
                DisneyStarWars2018InstantLotteryPrizeType? prize = Session["DisneyStarWarsPrizeType"] as DisneyStarWars2018InstantLotteryPrizeType?;

                if (!entryId.HasValue || entryId.Value != model.Id)
                {
                    throw new DisneyStarWars2018ServiceException("400", "당첨자가 아닙니다.", model.Id);
                }

                if (!prize.HasValue || prize.Value != model.PrizeType)
                {
                    throw new DisneyStarWars2018ServiceException("400", "해당 경품 당첨자가 아닙니다.", model.PrizeType);
                }

                if (!ModelState.IsValid)
                {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null)
                    {
                        throw new DisneyStarWars2018ServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }


                var user = service.GetInstantLotteryWinner(model.Id);
                var entity = mapperConfig.CreateMapper().Map<DisneyStarWars2018Model, DisneyStarWars2018InstantLottery>(model, user);
                entity.UpdateDate = common.Now;
                service.UpdateInstantLotteryWinner(entity);



                return Json(new
                {
                    result = true
                });
            }
            catch (Exception e)
            {
                var message = e.Message;
                if (!(e is EventServiceException))
                {
                    message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.";
                }
                return Json(new
                {
                    result = false,
                    message = message
                });
            }
        }
    }
}