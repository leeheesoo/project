using AutoMapper;
using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Services.Managements;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Models.ManagementModels;
using System.Collections.Generic;
using System.Web.Http;

namespace INGLife.Event.Controllers.Api {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize]
    [OnApiException]
    [ValidationActionFilter]
    public class AffiliatesController : ApiController {
        private IAffiliatesService service;
        private MapperConfiguration mapperConfig;

        public AffiliatesController(IAffiliatesService service) {
            this.service = service;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<Affiliates, AffiliatesViewModel>();
            });
        }
        public IList<AffiliatesViewModel> Get() {
            var entry = service.GetAffiliates;
            var list = mapperConfig.CreateMapper().Map<IList<AffiliatesViewModel>>(entry);
            return list;
        }

    }
}
