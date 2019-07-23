using KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2017-magickinderapp-launching")]
    public class AdminMagicKinderAppLaunchingController : Controller
    {
        private IMagicKinderAppLaunchingService service;
        private ICommonProvider common;

        public AdminMagicKinderAppLaunchingController(ICommonProvider common, IMagicKinderAppLaunchingService service)
        {
            this.common = common;
            this.service = service;
        }


        [Route("")]
        // GET: AdminMagicKinderAppLaunching
        public ActionResult Index()
        {
            return View();
        }

        [Route("statistics")]
        // GET: AdminMagicKinderAppLaunching
        public ActionResult Statistics()
        {
            return View();
        }

        [Route("excel-download")]
        public void ExcelDownload(MagicKinderAppLaunchingQueryOptions options)
        {
            var query = service.GetAdminMagicKinderAppLaunching(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.Channel,
                IP = e.IpAddress,
                이름 = e.Name,
                연락처 = e.Mobile,
                우편번호 = e.ZipCode,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                나이 = e.Age,
                스크린샷타입 = e.ScreenShotTypeDisplayName,
                한줄평 = e.Comment
            }).ToList();
            common.ExcelDownLoad(data, "[2017 매직킨더앱런칭 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("stats-excel-download")]
        public void StatisticsExcelDownload()
        {
            var query = service.GetStatistics();
            var data = query.Select(e => new {
                이름 = e.Name,
                연락처 = e.Mobile,
                총참여 = e.TotalCount,
                총참여노출 = e.TotalIsShowCount,
                밑그림그리고칠하기 = e.ColoringCount,
                밑그림그리고칠하기노출 = e.ColoringIsShowCount,
                비디오시청 = e.VideoCount,
                비디오시청노출 = e.VideoIsShowCount,
                놀면서배워요 = e.PlayingCount,
                놀면서배워요노출 = e.PlayingIsShowCount,
                동화 = e.StoryCount,
                동화노출 = e.StoryIsShowCount,
                우리별체험 = e.EarthTripCount,
                우리별체험노출 = e.EarthTripIsShowCount,
                기타 = e.EtcCount,
                기타노출 = e.EtcIsShowCount
            }).ToList();
            common.ExcelDownLoad(data, "[2017 매직킨더앱런칭 이벤트] 통계_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}