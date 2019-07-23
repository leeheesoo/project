using Samsonite.Mall.Domain.Services.OneYearAnniversary;
using Samsonite.Mall.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers.Admin {
    [Authorize]
    [RoutePrefix("manager/1st-anniversary")]
    public class AdminOneYearAnniversaryController : Controller {
        private IOneYearAnniversaryService service;
        private ICommonProvider common;
        public AdminOneYearAnniversaryController(IOneYearAnniversaryService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        [Route("")]
        public ActionResult Index() {
            return View();
        }

        [Route("download/excel")]
        public void ExcelDownload() {
            var entry = service.GetOneYearAnniversaryEntryAll().OrderByDescending(x=>x.CreateDate).Select(x => new {
                참여일 = x.CreateDate,
                채널 = x.Channel,
                아이피주소 = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                쌤소나이트아이디 = x.SamsoniteId,
                오행시_쌤 = x.AcrosticPoemFirst,
                오행시_소 = x.AcrosticPoemSecond,
                오행시_나 = x.AcrosticPoemThird,
                오행시_이 = x.AcrosticPoemFourth,
                오행시_트 = x.AcrosticPoemFifth,
                축하메시지 = x.CongratulationMessage
            });

            common.ExcelDownLoad(entry, "1st-anniversary-entry");
        }
    }
}