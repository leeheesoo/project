using KinderJoy.Domain.Service.KittyJusticeLeague;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2017-kitty-justiceleague")]
    public class AdminKittyJusticeLeagueController : Controller
    {
        private IKittyJusticeLeagueService service;
        private ICommonProvider common;

        public AdminKittyJusticeLeagueController(IKittyJusticeLeagueService service,ICommonProvider common) {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        // GET: AdminKittyJusticeLeague
        public ActionResult Index()
        {
            return View();
        }

        [Route("prize-setting")]
        public ActionResult InstantLotteryPrizeSetting() {
            return View();
        }

        [Route("download")]
        public void Download(KittyJusticeLeagueQueryOptions option) {
            var entry = service.GetAdminKittyJusticeLeagueEntryList(option).ToList();
            var data = entry.Select(e => new {
                당첨일 = e.CreateDate,
                채널 = e.ChannelName,
                IP = e.IpAddress,
                이름 = e.Name,
                연락처 = e.Mobile,
                우편번호 = e.ZipCode,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                경품 = e.PrizeName,
                개인정보등록일 = e.UpdateDate
            });

            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss_") + "키티_저스티스리그_참여자리스트";

            common.ExcelDownLoad(data, fileName);
        }
    }
}