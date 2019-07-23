using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.FinancialConcertMarketingAgree;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Models.FinancialConcertMarketingAgreeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using X.PagedList;

namespace INGLife.Event.Controllers.Api {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [OnApiException]
    [ValidationActionFilter]
    [Authorize]
    public class FinancialConcertMarketingAgreeApiController : ApiController {
        #region variables
        private IFinancialConcertMarketingAgreeService service;

        private MapperConfiguration mapperConfig;
        #endregion

        #region constructor
        public FinancialConcertMarketingAgreeApiController(IFinancialConcertMarketingAgreeService service) {
            this.service = service;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConcertMarketingAgreeAdminViewModel, FinancialConcertMarketingAgreeEntry>();
            });
        }
        #endregion

        public IPagedList<FinancialConcertMarketingAgreeAdminViewModel> Get([FromUri] PageQueryOptions options) {
            var list = service.GetAll();
            var data = mapperConfig.CreateMapper().Map<IList<FinancialConcertMarketingAgreeAdminViewModel>>(list);
            return new SerializablePagedList<FinancialConcertMarketingAgreeAdminViewModel>(data, options.Page, options.PageSize);
        }
    }
}
