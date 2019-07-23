using INGLife.Event.Domain.Services.KidsNote;
using INGLife.Event.Domain.Services.Nilririmambo;
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
    [RoutePrefix("manager/nilriri-mambo")]
    public class AdminNilririmamboController : Controller {
        private INilririmamboService service;
        private ICommonProvider common;

        public AdminNilririmamboController (INilririmamboService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        // GET: AdminKidsNote
        public ActionResult Index () {
            return View();
        }
        [Route("analytics")]
        // GET: AdminKidsNote
        public ActionResult Analytics () {
            return View();
        }
        [Route("excel")]
        public void ExcelDownload () {
            var entry = service.GetAdminNilririmamboEntryList().ToList();
            var data = entry.Select(x => new {
                이름 = x.Name,
                핸드폰 = x.Mobile,
                인스타그램ID = x.InstagramId,
                FC코드 = x.FcCode,
                IP = x.IpAddress,
                참여일자 = x.CreateDate
            });
            common.ExcelDownLoad(data, "닐리리맘보_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}