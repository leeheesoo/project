using AutoMapper;
using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Models.FindingDory2017;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.FindingDory2017;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.FindingDory2017;
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
    [RoutePrefix("api/2017-findingdory")]
    public class FindingDory2017Controller : ApiController
    {
        private IFindingDory2017Service service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public FindingDory2017Controller(IFindingDory2017Service service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FindingDory2017UserModel, FindingDory2017User>();
                cfg.CreateMap<FindingDory2017SNSModel, FindingDory2017SNS>();
            });
        }

        [Route("create-user", Name = "CreateFindingDory2017User")]
        [HttpPost]
        public FindingDory2017User CreateFindingDory2017User(FindingDory2017UserModel model)
        {
            if (common.Now >= new DateTime(2017, 3, 11))
            {
                throw new EventServiceException("400", "3월 10일 이벤트가 종료되었습니다.", null);
            }
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<FindingDory2017User>(model);

            entry.IpAddress = common.ClientIP;
            entry.Channel = common.IsMobileDevice ? "mobile" : "pc";
            entry.CreateDate = common.Now;

            var result = service.CreateUser(entry);
            return result;
        }

        [Route("create-sns", Name = "CreateFindingDory2017SNS")]
        [HttpPost]
        public FindingDory2017SNS CreateFindingDory2017SNS(FindingDory2017SNSModel model)
        {
            if (common.Now >= new DateTime(2017, 3, 11))
            {
                throw new EventServiceException("400", "3월 10일 이벤트가 종료되었습니다.", null);
            }
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<FindingDory2017SNS>(model);

            entry.CreateDate = common.Now;

            var result = service.CreateSNSShare(entry);
            return result;
        }

        [Authorize]
        [Route("get-users", Name = "GetFindingDory2017UserList")]
        [HttpGet]
        public PagedList.IPagedList<FindingDory2017User> GetFindingDory2017UserList([FromUri]AdminFindingDory2017QueryOptions options)
        {
            var users = service.GetUser(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<FindingDory2017User>(users, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("get-sns", Name = "GetFindingDory2017SNSList")]
        [HttpGet]
        public PagedList.IPagedList<FindingDory2017SNS> GetFindingDory2017SNSList([FromUri]AdminFindingDory2017SNSQueryOptions options)
        {

            var users = service.GetSns(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<FindingDory2017SNS>(users, options.Page, options.PageSize);
        }

        [Authorize]
        [Route("get-stats", Name = "GetFindingDory2017SNSStats")]
        public PagedList.IPagedList<AdminFindingDory2017StatsViewModel> GetFindingDory2017SNSStats([FromUri]PageQueryOptions options)
        {
            var list = service.GetStatistics().OrderByDescending(x => x.TotalCount);
            return new SerializablePagedList<AdminFindingDory2017StatsViewModel>(list, options.Page, options.PageSize);
        }
    }
}
