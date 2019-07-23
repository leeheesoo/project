using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderJoy.Domain.Entities.MainStream;
using KinderJoy.Domain.Service.MainStream;
using KinderJoy.Site.Infrastructure;

namespace KinderJoy.Site.Controllers.Admin {
    [Authorize]
    [RoutePrefix("manager/main-stream")]
    public class AdminMainStreamController : Controller {
        private ICommonProvider common;
        private IMainStreamService mainStreamService;
        public AdminMainStreamController(ICommonProvider common, IMainStreamService mainStreamService) {
            this.common = common;
            this.mainStreamService = mainStreamService;
        }

        [Route("")]
        public ActionResult Index() {
            var msSurpriseType = new List<SelectListItem>();
            foreach (var data in Enum.GetValues(typeof(MainStreamSurpriseType)).OfType<MainStreamSurpriseType>().ToList()) {
                var field = data.GetType().GetField(data.ToString()).GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;
                msSurpriseType.Add(new SelectListItem { Text = field.GetName(), Value = data.ToString("D") });
            }
            ViewBag.MainStreamSurprise = msSurpriseType;
            return View();
        }

        [Route("sns")]
        public ActionResult Sns() {
            return View();
        }

        [Route("sns-stats")]
        public ActionResult SnsStats() {
            return View();
        }
        [Route("excel-download")]
        public void ExcelDownload(string Type) {
            if (Type.ToLower().Equals("index")) {
                var query = mainStreamService.MainStreamSurprises.OrderByDescending(x => x.CreateDate);
                var data = query.AsEnumerable().Select(e => new {
                    참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    디바이스 = e.Channel,
                    아이피주소 = e.IpAddress,
                    이름 = e.Name,
                    나이 = e.Age,
                    성별 = e.Gender,
                    서프라이즈 = common.GetDisplayName<MainStreamSurpriseType>(e.Quiz),
                    연락처 = e.Mobile
                }).ToList();
                common.ExcelDownLoad(data, "[메인스트림 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));

            } else if (Type.ToLower().Equals("sns")) {
                var query = mainStreamService.MainStreamSurpriseSNSs.Include(x=>x.MainStreamSurprise).OrderByDescending(e => e.RegisterDate);
                var data = query.AsEnumerable().Select(e => new {
                    디바이스 = e.MainStreamSurprise.Channel,
                    이름 = e.MainStreamSurprise.Name,
                    나이 = e.MainStreamSurprise.Age,
                    연락처 = e.MainStreamSurprise.Mobile,
                    SNS유형 = e.SnsType,
                    SNS아이디 = e.SnsId,
                    SNS이름 = e.SnsName,
                    SNS닉네임 = e.SnsNickName,
                    SNS스크랩URL = e.ScrapUrl,
                    아이피주소 = e.MainStreamSurprise.IpAddress,
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
                common.ExcelDownLoad(data, "[메인스트림 이벤트] SNS공유_" + DateTime.Now.ToString("yyyyMMddHHmmss"));

            } else if (Type.ToLower().Equals("snsstats")) {
                var query = mainStreamService.GetMainStreamSurpriseSnsStats().OrderByDescending(x => x.TotalCount);
                var data = query.AsEnumerable().Select(e => new {
                    연락처 = e.Mobile,
                    이름 = e.Name,
                    나이 = (e.Age > 0 ? e.Age.ToString() : ""),
                    성별 = e.Gender,
                    총공유수 = e.TotalCount,
                    페이스북공유수 = e.FacebookCount,
                    카카오스토리공유수 = e.KakaostoryCount,
                    카카오톡공유수 = e.KakaotalkCount
                }).ToList();
                common.ExcelDownLoad(data, "[메인스트림 이벤트] SNS공유통계_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            } 
        }
    }

}