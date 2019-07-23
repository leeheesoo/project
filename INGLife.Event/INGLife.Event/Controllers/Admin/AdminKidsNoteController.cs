using INGLife.Event.Domain.Services.KidsNote;
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
    [RoutePrefix("manager/kids-note")]
    public class AdminKidsNoteController : Controller {
        private IKidsNoteService service;
        private ICommonProvider common;

        public AdminKidsNoteController (IKidsNoteService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        public ActionResult Index () {
            return View();
        }
        [Route("excel")]
        public void ExcelDownload () {
            var entry = service.GetAdminKidsNoteEntryList().ToList();
            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate,
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                자녀이름 = x.ChildName,
                자녀나이 = x.ChildAge == 0 ? "" : x.ChildAge + "",
                연락방식전화 = x.PhoneCheck.HasValue ? (x.PhoneCheck.Value ? "O" : "X") : "",
                연락방식문자 = x.MessageCheck.HasValue ? (x.MessageCheck.Value ? "O" : "X") : "",
                연락방식이메일 = x.EmailCheck.HasValue ? (x.EmailCheck.Value ? "O" : "X") : "",
                연락방식우편 = x.PostCheck.HasValue ? (x.PostCheck.Value ? "O" : "X") : "",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                개인정보등록일 = x.UpdateDate
            });
            common.ExcelDownLoad(data, "키즈노트이벤트_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}