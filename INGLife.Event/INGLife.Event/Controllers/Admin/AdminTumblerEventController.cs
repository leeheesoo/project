using INGLife.Event.Domain.Services.Rebranding;
using INGLife.Event.Domain.Services.TumblerEvent;
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
    [RoutePrefix("manager")]
    public class AdminTumblerEventController : Controller
    {
        private ITumblerEventService service;
        private ICommonProvider common;

        public AdminTumblerEventController(ITumblerEventService service,ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        [Route("tumbler")]
        public ActionResult Tumbler()
        {
            return View();
        }
        [Route("secret")]
        public ActionResult Secret() {
            return View();
        }

        [Route("tumbler/excel")]
        public void ExcelDownload(TumblerEventQueryOptions options)
        {
            var entry = service.GetAdminTumblerEventEntryList(options.eventType).ToList();
            var data = entry.Select(x => new
            {
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = "Y",
                연락방식문자 = "Y",
                연락방식이메일 = x.EmailCheck == true ? "Y" : "N",
                연락방식우편 = x.PostCheck == true ? "Y" : "N",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                개인정보등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss")
            });
            common.ExcelDownLoad(data, "텀블러_마케팅동의_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("secret/excel")]
        public void ConsultingExcelDownload(TumblerEventQueryOptions options)
        {
            var entry = service.GetAdminTumblerEventEntryList(options.eventType).ToList();
            var data = entry.Select(x => new
            {
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = "Y",
                연락방식문자 = "Y",
                연락방식이메일 = x.EmailCheck == true ? "Y" : "N",
                연락방식우편 = x.PostCheck == true ? "Y" : "N",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                건강 = x.HealthCheck == true ? "Y" : "N",
                월급 = x.SalaryCheck == true ? "Y" : "N",
                저축 = x.FundCheck == true ? "Y" : "N",
                예비자금 = x.SavingCheck == true ? "Y" : "N",
                기타 = x.EtcCheck == true ? "Y" : "N",
                개인정보등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss")
            });
            common.ExcelDownLoad(data, "시크릿박스_응모자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}