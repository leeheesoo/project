using AutoMapper;
using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Infrastructures;
using INGLife.Event.Domain.Services.Managements;
using INGLife.Event.Infrastructure;
using INGLife.Event.Infrastructures;
using INGLife.Event.Models.ManagementModels;
using System.Collections.Generic;
using System.Web.Http;

namespace INGLife.Event.Controllers.Api {
    [OnApiException]
    [ValidationActionFilter]
    public class EventManagementController : ApiController {
        private IEventManagementService service;
        private MapperConfiguration mapperConfig;
        private ITimeProvider time;

        public EventManagementController(IEventManagementService service, ITimeProvider time) {
            this.service = service;
            this.time = time;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<EventManagement, EventManagementViewModel>();
                config.CreateMap<EventManagementCreateModel, EventManagement>();
            });
        }
        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [Authorize]
        public IList<EventManagementViewModel> Get() {
            var entry = service.EventManagements;
            var list = mapperConfig.CreateMapper().Map<IList<EventManagementViewModel>>(entry);
            return list;
        }

        [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public void Post([FromBody]EventManagementCreateModel model) {
            var entry = mapperConfig.CreateMapper().Map<EventManagement>(model);
            entry.CreateDate = time.Now;
            service.CreateEventManagement(entry);
        }
    }
}
