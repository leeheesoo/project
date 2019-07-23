using AutoMapper;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Models.NinjaBarbie2016;
using KinderJoy.Domain.Service;
using KinderJoy.Domain.Service.NinjaBarbie2016;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.NinjaBarbie2016;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KinderJoy.Site.Controllers.Api
{
    [RoutePrefix("api/2016-ninjabarbie")]
    public class NinjaBarbie2016ApiController : ApiController
    {
        private INinjaBarbie2016Service service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;


        public NinjaBarbie2016ApiController(INinjaBarbie2016Service service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                //개인정보
                cfg.CreateMap<NinjaBarbie2016UserViewModel, NinjaBarbie2016User>();
                cfg.CreateMap<NinjaBarbie2016SurprizeViewModel, NinjaBarbie2016User>();
                cfg.CreateMap<NinjaBarbie2016SharingViewModel, NinjaBarbie2016Sharing>();
            });
        }

        #region 이벤트페이지
        [Route("save-user", Name = "NinjaBarbie2016SaveUser")]
        [HttpPost]
        public NinjaBarbie2016SurprizeViewModel SaveUser(NinjaBarbie2016UserViewModel model) {
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<NinjaBarbie2016UserViewModel, NinjaBarbie2016User>(model);
            entry.Channel = common.IsMobileDevice ? "Mobile" : "PC";
            entry.IpAddress = common.ClientIP;
            entry.CreateDate = common.Now;

            var user = service.CreateUser(entry);

            return new NinjaBarbie2016SurprizeViewModel {
                UserId = user.Id,
                SurprizeType = user.SurprizeType
            };
        }

        [Route("save-surprize", Name = "NinjaBarbie2016SaveSurprize")]
        [HttpPost]
        public NinjaBarbie2016SharingViewModel SaveSurprize(NinjaBarbie2016SurprizeViewModel model) {
            byte[] byteArray = Convert.FromBase64String(model.FacebookImage);
            MemoryStream stream = new MemoryStream(byteArray);
            model.FacebookImage = common.S3FileUpload(stream, "NinjaBarbie2016", "synthesisImage.jpg");

            byteArray = Convert.FromBase64String(model.KakaotalkImage);
            stream = new MemoryStream(byteArray);
            model.KakaotalkImage = common.S3FileUpload(stream, "NinjaBarbie2016", "synthesisImage.jpg");

            byteArray = Convert.FromBase64String(model.KakaostoryImage);
            stream = new MemoryStream(byteArray);
            model.KakaostoryImage = common.S3FileUpload(stream, "NinjaBarbie2016", "synthesisImage.jpg");

            var user = service.GetUsers().Where(x => x.Id == model.UserId).SingleOrDefault();
            if (user == null) {

            }
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<NinjaBarbie2016SurprizeViewModel, NinjaBarbie2016User>(model, user);
            var update = service.UpdateUser(entry);

            return new NinjaBarbie2016SharingViewModel {
                UserId = update.Id,
                FacebookImage = update.FacebookImage,
                KakaotalkImage = update.KakaotalkImage,
                KakaostoryImage = update.KakaostoryImage
            };
        }

        [Route("save-sharing", Name = "NinjaBarbie2016SaveSharing")]
        [HttpPost]
        public NinjaBarbie2016SharingViewModel SaveSharing(NinjaBarbie2016SharingViewModel model) {
            var mapper = mapperConfig.CreateMapper();
            var entry = mapper.Map<NinjaBarbie2016SharingViewModel, NinjaBarbie2016Sharing>(model);
            entry.CreateDate = common.Now;

            var user = service.CreateSharing(entry);

            return model;
        }
        #endregion

        #region 관리자페이지
        [Authorize]
        [Route("manager/get-users", Name = "AdminNinjaBarbie2016GetUsers")]
        public PagedList.IPagedList<NinjaBarbie2016User> AdminGetUsers(AdminNinjaBarbie2016UserQueryOptions options) {
            var users = service.GetUsers(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<NinjaBarbie2016User>(users, options.Page, options.PageSize);
        }
        [Authorize]
        [Route("manager/get-sharings", Name = "AdminNinjaBarbie2016GetSharings")]
        public PagedList.IPagedList<NinjaBarbie2016Sharing> AdminGetSharings(AdminNinjaBarbie2016SharingQueryOptions options) {
            var users = service.GetSharings(options).OrderByDescending(x => x.CreateDate);

            return new SerializablePagedList<NinjaBarbie2016Sharing>(users, options.Page, options.PageSize);
        }
        [Authorize]
        [Route("manager/get-statistics", Name = "AdminNinjaBarbie2016GetStatistics")]
        public PagedList.IPagedList<AdminNinjaBarbie2016StatisticsViewModel> AdminGetStatistics(PageQueryOptions options) {
            var list = service.GetStatistics();
            return new SerializablePagedList<AdminNinjaBarbie2016StatisticsViewModel>(list, options.Page, options.PageSize);
        }
        #endregion
    }
}
