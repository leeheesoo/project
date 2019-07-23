using KinderJoy.Domain.Service.DisneyStarWars2018;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/disney-starwars")]
    public class AdminDisneyStarWars2018Controller : Controller
    {
        private IDisneyStarWars2018Service service;
        private ICommonProvider common;

        public AdminDisneyStarWars2018Controller(IDisneyStarWars2018Service service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("prize-setting")]
        public ActionResult InstantLotteryPrizeSetting()
        {
            return View();
        }

        [Route("download")]
        public void Download(DisneyStarWars2018QueryOptions option)
        {
            var entry = service.GetAdminDisneyStarWars2018InstatLotteryList(option).ToList();
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

            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss_") + "디즈니_스타워즈_참여자리스트";

            common.ExcelDownLoad(data, fileName);
        }
    }
}