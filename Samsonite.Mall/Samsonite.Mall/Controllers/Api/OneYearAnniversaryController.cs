using AutoMapper;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Services.OneYearAnniversary;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models;
using Samsonite.Mall.Models.OneYearAnniversaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LinqKit;

namespace Samsonite.Mall.Controllers.Api {
    /// <summary>
    /// 오행시짓기 댓글이벤트 Api 컨트롤러
    /// </summary>
    [RoutePrefix("api/1st-anniversary")]
    [OnApiException]
    public class OneYearAnniversaryController : ApiController {
        private IOneYearAnniversaryService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;
        public OneYearAnniversaryController(IOneYearAnniversaryService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<OneYearAnniversaryCreateModel, OneYearAnniversaryEntry>();
            });
        }

        [HttpGet]
        [Route("get/all")]
        public IQueryable<OneYearAnniversaryEntry> Get() {
            return service.GetOneYearAnniversaryEntryAll().AsQueryable();
        }

        [HttpGet]
        [Route("get")]
        public OneYearAnniversaryEntry Get(long id) {
            return service.GetOneYearAnniversaryEntry(id);
        }

        [HttpGet]
        [Route("get/list")]
        public SerializablePagedList<OneYearAnniversaryEntry> Get([FromUri]OneYearAnniversaryPageQueryOptions options) {
            var entry = service.GetOneYearAnniversaryEntryAll().AsQueryable()
                .AsExpandable().Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate).ToList();

            return new SerializablePagedList<OneYearAnniversaryEntry>(entry, options.Page, options.PageSize);
            /*
             var list = entry.ToList();
            list.Add(new OneYearAnniversaryEntry { CreateDate = new DateTime(2222,1,1) });
            return new SerializablePagedList<OneYearAnniversaryEntry>(list.OrderByDescending(x=>x.CreateDate), options.Page, options.PageSize);
            */
        }


        [HttpPost]
        [Route("post", Name = "CreateOneYearAnniversaryEntry")]
        public OneYearAnniversaryEntry Post(OneYearAnniversaryModel model) {
            if (common.Now >= new DateTime(2017, 4, 24)) {
                throw new OneYearAnniversaryServiceException("이벤트가 종료되었습니다.", model);
            }
            var entry = mapperConfig.CreateMapper().Map<OneYearAnniversaryEntry>(model.OneYearAnniversaryCreateModel);
            entry.IpAddress = common.IpAddress;
            entry.CreateDate = common.Now;
            entry.Channel = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "web";
            return service.CreateOneYearAnniversaryEntry(entry);
        }
        
        [HttpPut]
        [Route("put")]
        public void Put([FromBody]OneYearAnniversaryModel model) {
            var updateEntry = service.GetOneYearAnniversaryEntry(model.OneYearAnniversaryUpdateModel.Id);

            updateEntry.Name = model.OneYearAnniversaryUpdateModel.Name;
            updateEntry.SamsoniteId = model.OneYearAnniversaryUpdateModel.SamsoniteId;
            updateEntry.Mobile = model.OneYearAnniversaryUpdateModel.Mobile;
            service.UpdateOneYearAnniversaryEntry(updateEntry);
        }

        [Authorize]
        [HttpPut]
        [Route("put/admin")]
        public void Put([FromBody]AdminOneYearAnniversaryUpdateModel model) {
            var updateEntry = service.GetOneYearAnniversaryEntry(model.Id);
            updateEntry.CongratulationMessage = model.CongratulationMessage;
            updateEntry.AcrosticPoemFirst = model.AcrosticPoemFirst;
            updateEntry.AcrosticPoemSecond = model.AcrosticPoemSecond;
            updateEntry.AcrosticPoemThird = model.AcrosticPoemThird;
            updateEntry.AcrosticPoemFourth = model.AcrosticPoemFourth;
            updateEntry.AcrosticPoemFifth = model.AcrosticPoemFifth;
            updateEntry.Name = model.Name;
            updateEntry.SamsoniteId = model.SamsoniteId;
            updateEntry.Mobile = model.Mobile;
            updateEntry.IsShow = model.IsShow;

            service.UpdateOneYearAnniversaryEntry(updateEntry);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public void Delete(long id) {
            service.DeleteOneYearAnniversaryEntry(id);
        }
    }
}
