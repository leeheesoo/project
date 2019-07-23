using AutoMapper;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Services.TwoYearAnniversary;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models;
using Samsonite.Mall.Models.TwoYearAnniversaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LinqKit;
using Samsonite.Mall.Domain.Exceptions;
using Samsonite.Mall.Models.OneYearAnniversaryModels;
using X.PagedList;

namespace Samsonite.Mall.Controllers.Api {
    /// <summary>
    /// 오행시짓기 댓글이벤트 Api 컨트롤러
    /// </summary>
    [RoutePrefix("api/2st-anniversary")]
    [OnApiException]
    [ValidationActionFilter]
    public class TwoYearAnniversaryController : ApiController {
        private ITwoYearAnniversaryService service;        
        private ICommonProvider common;

        public TwoYearAnniversaryController(ITwoYearAnniversaryService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }


        [HttpGet]
        [Authorize]
        [Route("admin-entry-list", Name = "SelectTwoYearAnniversaryEntry")]
        public IPagedList<TwoYearAnniversaryEntry> Entry([FromUri]TwoYearAnniversaryPageQueryOptions entry)
        {            
            var result = service.SelectTwoYearAnniversaryEnty().AsExpandable().Where(entry.BuildPredicate()).OrderByDescending(x => x.CreateDate);
            return new SerializablePagedList<TwoYearAnniversaryEntry>(result, entry.Page, entry.PageSize);
        }



        [HttpGet]
        [Authorize]
        [Route("admin-prize-list")]
        public IQueryable<TwoYearAnniversaryInstantPrizeSetting> CreateValentineInstantLotteryEntry()
        {
            
            return service.GetTwoYearAnniversaryPrizeList().AsQueryable();
        }        

        [HttpPost]
        [Authorize]
        [Route("create-admin-prize", Name = "CreateTwoYearAnniversaryPrizeSetting") ]
        public void Entry(TwoYearAnniversaryInstantPrizeSetting entity)
        {
            service.CreateTwoYearAnniversaryPrize(entity);            
        }

        [HttpPost]
        [Authorize]
        [Route("update-admin-entry")]
        public void UpdateEntry(TwoYearAnniversaryInstantPrizeSetting entity)
        {
            service.UpdateTwoYearAnniversaryPrize(entity);
        }



        //이벤트 기한 설정 메소드
        public void DueToEventDate(DateTime date , string msg)
        {
            if (common.Now >= date)
            {
                throw new TwoYearAnniversaryServiceException("400", msg, null);
            }
        }
    }    
}
