using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using INGLife.Event.Domain.Services.FinancialConsultantSharing;
using INGLife.Event.Domain.Services.Rebranding;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Models.FinancialConsultantSharingModels.Origin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Admin
{
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [RoutePrefix("manager/fc-sharing")]
    public class AdminFcEventController : Controller
    {
        private IFinancialConsultantSharingService financialConsultantSharingService;
        private ICommonProvider common;

        public AdminFcEventController(IFinancialConsultantSharingService service, ICommonProvider common)
        {
            this.financialConsultantSharingService = service;
            this.common = common;
        }
        [Route("fc-sharing")]
        public ActionResult FcOrinalSharing()
        {
            return View();
        }
        [Route("fc-sharing-new")]
        public ActionResult FcNewSharing()
        {
            return View();
        }

        [Route("fc-sharing/excel")]
        public void FcOrinalExcelDownload(RebrandingQueryOptions options)
        {
            var entry = financialConsultantSharingService.GetAdminFinancialConsultantOriginalEntryList().ToList();
            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = x.PhoneCheck == true ? "Y" : "N",
                연락방식문자 = x.MessageCheck == true ? "Y" : "N",
                연락방식이메일 = x.EmailCheck == true ? "Y" : "N",
                연락방식우편 = x.PostCheck == true ? "Y" : "N",
                보유기간 = x.RetentionPeriodOriginTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                재무상담사코드 = x.FcCode,
                고유번호 = x.CustomerRandomNum,
                신규가입자수 = x.Cnt
                //문자메세지발송여부 = x.IsMessage == true ? "발송" : "미발송",
            });
            common.ExcelDownLoad(data, "FC_기존고객_참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("fc-sharing-new/excel")]
        public void FcNewExcelDownload(RebrandingQueryOptions options)
        {
            var entry = financialConsultantSharingService.GetAdminFinancialConsultantNewEntryList().ToList();
            var data = entry.Select(x => new {
                본인인증등록일 = x.CreateDate.ToString(@"yyyy\/MM\/dd HH:mm:ss"),
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender == "0" ? "남" : "여",
                생년월일 = x.BirthDay,
                연락방식전화 = x.PhoneCheck == true ? "Y" : "N",
                연락방식문자 = x.MessageCheck == true ? "Y" : "N",
                연락방식이메일 = x.EmailCheck == true ? "Y" : "N",
                연락방식우편 = x.PostCheck == true ? "Y" : "N",
                관심분야 = x.RetentionPeriodNewTypeDisplayName,
                이메일 = x.Email,
                우편번호 = x.ZipCode,
                주소 = x.Address,
                상세주소 = x.AddressDetail,
                재무상담사코드 = x.FcCode,
                추천인이름 = x.ProposerName,
                추천인고유번호 = x.OriginCustomerRandomNum,
                //문자메세지발송여부 = x.IsMessage,
            });
            common.ExcelDownLoad(data, "FC_신규고객_참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}