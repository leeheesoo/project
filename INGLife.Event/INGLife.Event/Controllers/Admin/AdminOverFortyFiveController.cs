using INGLife.Event.Domain.Services.OverFortyFiveDb;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Admin {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [RoutePrefix("manager/over-forty-five")]
    public class AdminOverFortyFiveController : Controller {
        private IOverFortyFiveService service;
        private ICommonProvider common;

        public AdminOverFortyFiveController (IOverFortyFiveService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        // GET: AdminMarketingAgree
        public ActionResult Index () {
            return View();
        }

        [Route("excel")]
        public void ExcelDownload () {
            var entry = service.GetAdminOverFortyFiveEntryList().ToList();

            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = "Y",
                연락방식문자 = "Y",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                개인정보등록일 = x.UpdateDate.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                이모티콘 = x.EmoticonTypeDisplayName,
            });
            common.ExcelDownLoad(data, "4050DB확보_참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}