using Finnq.Promotion.Domain.Services.RouletteEvent;
using Finnq.Promotion.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Controllers.Admin.RouletteEvent
{
    [RoutePrefix("manager/t-roulette")]
    [Authorize(Roles = "Administrators,Pentacle")]
    [AccessIpAddressFilterAttribute(AllowIpAddresses = new string[] { "211.60.50.131" })]
    public class AdminTRouletteEventController : Controller
    {
        private ITRouletteEventService service;
        private ICommonProvider common;

        public AdminTRouletteEventController(ITRouletteEventService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }
        [Route("")]
        // GET: AdminTRouletteEvent
        public ActionResult Index() {
            return View();
        }

        [Route("prize-setting")]
        public ActionResult InstantLotteryPrizeSetting() {
            return View();
        }

        [Route("download")]
        public void Download(TRouletteEventQueryOptions option) {
            var entry = service.GetAdminTRouletteEventEntryList(option).ToList();
            var data = entry.Select(e => new {
                채널 = e.ChannelName,
                IP = e.IpAddress,
                이름 = e.Name,
                휴대폰 = e.Mobile,
                개인정보등록일 = e.CreateDate,
                경품 = e.PrizeName,
                당첨일 = e.UpdateDate
            });

            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss_") + "T_룰렛이벤트_참여자리스트";

            common.ExcelDownLoad(data, fileName);
        }
    }
}