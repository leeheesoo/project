using LinqKit;
using Samsonite.Mall.Domain.Services.TwoYearAnniversary;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models.TwoYearAnniversaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2st-anniversary")]
    public class AdminTwoYearAnniversaryController : Controller
    {
        private ITwoYearAnniversaryService service;
        private ICommonProvider common;
        public AdminTwoYearAnniversaryController(ITwoYearAnniversaryService service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;
        }

        // GET: AdminTwoYearAnniversary
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("2stPrize")]
        public ActionResult PrizeSetting()
        {
            return View();
        }

        [Route("download/excel")]
        public void ExcelDownload(TwoYearAnniversaryPageQueryOptions option)
        {            
            var entry = service.GetOneYearAnniversaryEntryAll().AsQueryable().AsExpandable().Where(option.BuildPredicate()).OrderByDescending(x => x.CreateDate).Select(x => new {
                참여일 = x.CreateDate,
                채널 = x.ChannelName,
                아이피주소 = x.IpAddress,                                
                쌤소나이트아이디 = x.SamsoniteId,
                쌤소나이트복호화 = x.SamsoniteEncryptionId, //decrption 
                당첨경품 = x.PrizeName          
            });

            common.ExcelDownLoad(entry, "2st-anniversary-entry");
        }
    }
}