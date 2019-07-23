using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using INGLife.Event.Domain.Services;
using INGLife.Event.Domain.Services.FinancialConsultantSharing;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Message.Services;
using INGLife.Event.Models.FinancialConsultantSharingModels;
using INGLife.Event.Models.FinancialConsultantSharingModels.New;
using INGLife.Event.Models.FinancialConsultantSharingModels.Origin;
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
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    public class FcEvntApiController : ApiController
    {
        private IFinancialConsultantSharingService financialConsultantSharingService;
        private MapperConfiguration mapperConfig;

        #region constructor
        public FcEvntApiController(IFinancialConsultantSharingService financialConsultantSharingService)
        {
            this.financialConsultantSharingService = financialConsultantSharingService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConsultantSharingOriginViewModel, FinancialConsultantOriginalCustomerEntry>();
                cfg.CreateMap<FinancialConsultantSharingNewViewModel, FinancialConsultantNewCustomerEntry>();                
                cfg.CreateMap<OriginalStatusModel, FinancialConsultantSharingOriginViewModel>();
            });
        }
        #endregion

        [Authorize]
        public IPagedList<FinancialConsultantSharingOriginViewModel> GetOriginal([FromUri] PageQueryOptions options)
        {
            var list = financialConsultantSharingService.GetAdminFinancialConsultantOriginalEntryList();
            var data = mapperConfig.CreateMapper().Map<IList<FinancialConsultantSharingOriginViewModel>>(list);
            return new SerializablePagedList<FinancialConsultantSharingOriginViewModel>(data, options.Page, options.PageSize);
        }

        [Authorize]
        public IPagedList<FinancialConsultantSharingNewViewModel> GetNew([FromUri] PageQueryOptions options)
        {
            var list = financialConsultantSharingService.GetAdminFinancialConsultantNewEntryList();
            var data = mapperConfig.CreateMapper().Map<IList<FinancialConsultantSharingNewViewModel>>(list);
            return new SerializablePagedList<FinancialConsultantSharingNewViewModel>(data, options.Page, options.PageSize);
        }
    }
}
