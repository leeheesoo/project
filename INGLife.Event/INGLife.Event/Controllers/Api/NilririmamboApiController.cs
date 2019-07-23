using AutoMapper;
using INGLife.Event.Domain.Entities.MamboEvent;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.Nilririmambo;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Models.NilririmamboModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using X.PagedList;

namespace INGLife.Event.Controllers.Api
{

    [OnApiException]
    [ValidationActionFilter]
    public class NilririmamboApiController : ApiController
    {
        private INilririmamboService service;
        private MapperConfiguration mapperConfig;
        private ICommonProvider common;

        public NilririmamboApiController(INilririmamboService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                //개인정보
                cfg.CreateMap<NilririmamboModel, NilririmanboEntry>();
            });
        }

        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]
        public IPagedList<NilririmanboEntry> GetAdminNilririmamboEntryList([FromUri] PageQueryOptions options) {
            var result = service.GetAdminNilririmamboEntryList();
            return new SerializablePagedList<NilririmanboEntry>(result, options.Page, options.PageSize);
        }

        [HttpPost]
        public NilririmanboEntry createdEntry(NilririmamboModel model)
        {
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<NilririmamboModel, NilririmanboEntry>(model);
            
            entry.CreateDate = common.Now;
            entry.IpAddress = common.IpAddress;
            var result = service.CreateNilririmamboEntry(entry);
            return result;
        }
    }
}
