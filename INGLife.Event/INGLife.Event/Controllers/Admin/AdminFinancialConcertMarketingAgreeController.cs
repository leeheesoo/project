using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Services.FinancialConcertMarketingAgree;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Models.FinancialConcertMarketingAgreeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Admin {
    /// <summary>
    /// 재무콘서트 마케팅동의 어드민페이지
    /// </summary>
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [RoutePrefix("manager/concert")]
    public class AdminFinancialConcertMarketingAgreeController : Controller {
        #region variables
        private IFinancialConcertMarketingAgreeService service;
        private ICommonProvider common;

        private MapperConfiguration mapperConfig;
        #endregion

        #region constuctor
        public AdminFinancialConcertMarketingAgreeController(IFinancialConcertMarketingAgreeService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConcertMarketingAgreeAdminViewModel, FinancialConcertMarketingAgreeEntry>();
            });
        }
        #endregion

        [Route("")]
        public ActionResult Index () {
            return View();
        }

        [Route("excel-download")]
        public void ExcelDownload () {
            var list = service.GetAll();
            var mapperData = mapperConfig.CreateMapper().Map<IList<FinancialConcertMarketingAgreeAdminViewModel>>(list);
            var data = mapperData.Select(x=> new {
                본인인증등록일시 = x.CreateDate,
                채널 = x.ChannelDisplayName,
                IP = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                성별 = x.Gender,
                생년월일 = x.BirthDay,
                선택회차 = x.ApplicationTurnDisplayName,
                연락방식_전화 = x.PhoneCheck? "O" : "X",
                연락방식_문자 = x.MessageCheck ? "O" : "X",
                연락방식_이메일 = x.EmailCheck ? "O" : "X",
                연락방식_우편 = x.PostCheck ? "O" : "X",
                보유기간 = x.RetentionPeriodTypeDisplayName,
                이메일 = x.Email,
                주소 = x.AddressDisplayName,
                응모완료일시 = x.CompleteDate
            });
            common.ExcelDownLoad(data, "FinancialConcert_MarketingAgree_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}