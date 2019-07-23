using INGLife.Event.Domain.Services.Rebranding;
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
    [RoutePrefix("manager/new-branding")]
    public class AdminRebrandingController : Controller
    {
        private IRebrandingEventService service;
        private ICommonProvider common;

        public AdminRebrandingController(IRebrandingEventService service,ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        [Route("marketing")]
        public ActionResult MarketingAgree()
        {
            return View();
        }
        [Route("consulting")]
        public ActionResult ConsultingAgree() {
            return View();
        }

        [Route("marketing/excel")]
        public void ExcelDownload(RebrandingQueryOptions options) {
            var entry = service.GetAdminRebrandingMarketingEntryList(options.month).ToList();
            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = x.PhoneCheck ? "Y" : "N",
                연락방식문자 = x.MessageCheck? "Y" : "N",
                연락방식이메일 = x.EmailCheck ? "Y" : "N",
                연락방식우편 = x.PostCheck ? "Y" : "N",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                개인정보등록일 = x.UpdateDate.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss")
            });
            common.ExcelDownLoad(data, "뉴브랜딩_마케팅동의_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("consulting/excel")]
        public void ConsultingExcelDownload(RebrandingQueryOptions options) {
            var entry = service.GetAdminRebrandingConsultingEntryList(options.month).ToList();
            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                관심분야 = x.AttetionTypoeDisplayName,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                개인정보등록일 = x.UpdateDate.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss")
            });
            common.ExcelDownLoad(data, "뉴브랜딩_상담동의_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}