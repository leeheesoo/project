using KinderJoy.Domain.Abstract.Christmas2015;
using KinderJoy.Domain.Repository.Christmas2015;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Christmas2015;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KinderJoy.Site.Controllers.Event {
    /// <summary>
    /// 크리스마스 이벤트
    /// </summary>
    [RoutePrefix("event/2015-christmas")]
    public class Christmas2015Controller : Controller {
        /*
         * 네이버 검색 유입 Session : Session["2015ChristmasNaverSearching"] (true: 네이버 통해서 들어옴 / false : 그냥 들어옴)
         * 당첨여부
         */
        #region variables
        private IChristmas2015Repository repository;
        private ICommonProvider common;
        #endregion

        #region constructor
        public Christmas2015Controller(IChristmas2015Repository repository, ICommonProvider common) {
            this.repository = repository;
            this.common = common;
        }
        #endregion

        [Route("")]
        public ActionResult Index(long? mtk) {
            Response.AddHeader("Access-Control-Allow-Origin", "fr-kinderjoy.s3.amazonaws.com");
            //네이버 검색 : 팝업 노출
            ViewBag.ChristmasEvent1 = false;
            if (common.Now < new DateTime(2015, 12, 28))
            {
                string isShow = Session["Is2015ChristmasNaverSearchingShow"] as string;
                if (!string.IsNullOrEmpty(isShow) && isShow.Equals("SHOW"))
                {
                    ViewBag.ChristmasEvent1 = true;
                }
                else if (!string.IsNullOrEmpty(isShow) && isShow.Equals("CLOSE"))
                {
                    ViewBag.ChristmasEvent1 = false;
                }
            }
            Session["Is2015ChristmasNaverSearchingShow"] = null;

            ViewBag.Date = common.Now;

            ViewBag.Mtk = mtk;
            ViewBag.SnsImage = "https://fr-kinderjoy.s3-ap-northeast-1.amazonaws.com/Christmas2015/ffdc67b7-4648-41a1-8f5a-d8f0c37b4ce0.jpg?X-Amz-Expires=900&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAISFUX2YEIIJG35VQ/20151208/ap-northeast-1/s3/aws4_request&X-Amz-Date=20151208T060711Z&X-Amz-SignedHeaders=host&X-Amz-Signature=27121f8ca702feba7e0b123e2278c6c9ce38abebafb51e84dd9e36ec62e5fe0b";
            if (mtk.HasValue) {
                var makeTree = repository.Christmas2015MakeTree.Where(e => e.Id == mtk.Value).SingleOrDefault();
                if (makeTree != null) {
                    ViewBag.SnsImage = makeTree.SynthesisImage;
                }
            }
            return View();
        }

        #region [EVENT1] 즉석당첨
        /// <summary>
        /// [즉석당첨 이벤트] 검색하기 버튼 클릭 (네이버로 랜딩)
        /// </summary>
        [Route("naver-landing")]
        [HttpGet]
        public ActionResult NaverLanding() {
            //2015ChristmasNaverSearching 세션 생성
            Session["2015ChristmasNaverSearchingStatus"] = "START";

            if (Request.Browser.IsMobileDevice) {
                return Redirect("http://m.naver.com/");
            } else {
                return Redirect("http://www.naver.com/");
            }
        }

        /// <summary>
        /// [즉석당첨 이벤트] 네이버에서 유입 
        /// </summary>
        [Route("naver-searching")]
        [HttpGet]
        public ActionResult NaverSearching() {
            //세션 check
            var checkedNaverSearching = Session["2015ChristmasNaverSearching"] as string;

            if (common.Now >= new DateTime(2015, 12, 28)) { //이벤트 종료일 check (이벤트는 12/27 까지)
                Session["Is2015ChristmasNaverSearchingShow"] = "CLOSE";
            } else {
                Session["Is2015ChristmasNaverSearchingShow"] = "SHOW";
            }

            NameValueCollection queryString = Request.QueryString;
            RouteValueDictionary route = new RouteValueDictionary();
            if (queryString != null && queryString.HasKeys() == true) {
                foreach (string key in queryString.AllKeys) {
                    route.Add(key, queryString[key]);
                }
            }

            return RedirectToAction("Index", route);
        }

        /// <summary>
        /// 네이버 검색 : 즉석당첨 확인하기
        /// </summary>
        /// <returns></returns>
        [Route("save-naver-searching")]
        [HttpPost]
        public ActionResult CreateNaverSearching() {
            string status = Session["2015ChristmasNaverSearchingStatus"] as string;
            bool isOK = false;

            if (!string.IsNullOrEmpty(status) && status.Equals("START") && Request.Cookies["Kinderjoy2015Christmas"] == null) {
                isOK = true;
            }
            Session["2015ChristmasNaverSearchingStatus"] = null;

            //DB저장
            var entry = repository.CreateChristmas2015Win(new Domain.Entities.Christmas2015.Christmas2015Win {
                Channel = Request.Browser.IsMobileDevice ? "mobile" : "web",
                IpAddress = common.ClientIP,
                RegisterDate = common.Now
            }, isOK);

            if (entry.PrizeType != Christmas2015EnumConst.Christmas2015WinPrize.Loser) {
                //쿠키 생성
                HttpCookie eventCookie = Request.Cookies["Kinderjoy2015Christmas"];
                if (eventCookie == null) {
                    eventCookie = new HttpCookie("Kinderjoy2015Christmas");
                    eventCookie.Values.Add("Start", "Y");
                    eventCookie.Expires = new DateTime(2016, 1, 1);
                    Response.Cookies.Set(eventCookie);
                }
            }
            return Json(entry);
        }
        #endregion
    }
}