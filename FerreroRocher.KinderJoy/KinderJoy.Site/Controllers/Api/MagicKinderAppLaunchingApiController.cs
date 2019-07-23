using AutoMapper;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Models.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.MagicKinderAppLaunchingEvent;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KinderJoy.Site.Controllers.Api
{
    [OnApiException]
    [ValidationActionFilter]
    [RoutePrefix("api/magickinder-launching")]
    public class MagicKinderAppLaunchingApiController : ApiController
    {
        private IMagicKinderAppLaunchingService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public MagicKinderAppLaunchingApiController(IMagicKinderAppLaunchingService service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MagicKinderAppLaunchingEventModel,MagicKinderAppLaunching>();
                cfg.CreateMap<MagicKinderAppLaunching, MagicKinderAppLaunchingViewModel>();
            });
        } 
        
        [Route("get-list",Name = "GetMagicKinderAppLaunching")]
        [HttpGet]
        public IList<MagicKinderAppLaunchingViewModel> GetMagicKinderAppLaunching()
        {
            var list = service.GetMagicKinderAppLaunching().OrderByDescending(x=>x.CreateDate).ToList();
            var result = mapperConfig.CreateMapper().Map<IList<MagicKinderAppLaunchingViewModel>>(list);
            return result;
        }

        [Authorize]
        [Route("get-admin-list", Name = "GetAdminMagicKinderAppLaunching")]
        [HttpGet]
        public IPagedList<MagicKinderAppLaunching> GetAdminMagicKinderAppLaunching([FromUri]MagicKinderAppLaunchingQueryOptions options)
        {
            var result = service.GetAdminMagicKinderAppLaunching(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<MagicKinderAppLaunching>(result, options.Page, options.PageSize);
        }


        [Authorize]
        [Route("get-admin-statistics", Name = "GetAdminMagicKinderAppLaunchingStatistics")]
        [HttpGet]
        public IPagedList<AdminMagicKinderStatsViewModel> GetAdminMagicKinderAppLaunchingStatistics([FromUri]PageQueryOptions options)
        {
            var result = service.GetStatistics();

            return new SerializablePagedList<AdminMagicKinderStatsViewModel>(result, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("change-isshow", Name = "AdminUpdateIsShowMagicKinderAppLaunching")]
        [HttpPost]
        public void AdminUpdateIsShowMagicKinderAppLaunching(MagicKinderAppLaunchingIsShowChangeQueryOptions option)
        {
            service.UpdateIsShowMagicKinderAppLaunching(option.Id);
        }
    }
}
