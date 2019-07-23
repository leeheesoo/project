using KinderJoy.Domain.Models.NinjaBarbie2016;
using KinderJoy.Domain.Service.NinjaBarbie2016;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2016-ninjabarbie")]
    public class AdminNinjaBarbie2016Controller : Controller
    {
        private ICommonProvider common;
        private INinjaBarbie2016Service service;

        public AdminNinjaBarbie2016Controller(ICommonProvider common, INinjaBarbie2016Service service) {
            this.common = common;
            this.service = service;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("user-excel-download")]
        public void UserExcelDownload(AdminNinjaBarbie2016UserQueryOptions options) {
            var query = service.GetUsers(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.Channel,
                IP = e.IpAddress,
                이름 = e.Name,
                연락처 = e.Mobile,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                우편번호 = e.ZipCode,
                나이 = e.Age,
                선택장난감 = e.SurprizeTypeDisplayName
            }).ToList();
            common.ExcelDownLoad(data, "[2016 닌자바비 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("sharing")]
        public ActionResult Sharing() {
            return View();
        }
        [Route("sharing-excel-download")]
        public void SharingExcelDownload(AdminNinjaBarbie2016SharingQueryOptions options) {
            var query = service.GetSharings(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                공유일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.User.Channel,
                이름 = e.User.Name,
                연락처 = e.User.Mobile,
                나이 = e.User.Age,
                선택장난감 = e.User.SurprizeTypeDisplayName,
                SNS = e.SnsTypeDisplayName,
                아이디 = e.SnsId,
                닉네임 = e.SnsName,
                URL = e.Post,
                IP = e.User.IpAddress
            }).ToList();
            common.ExcelDownLoad(data, "[2016 닌자바비 이벤트] 공유_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("statistics")]
        public ActionResult Statistics() {
            return View();
        }
        [Route("statistics-excel-download")]
        public void StatisticsExcelDownload() {
            var query = service.GetStatistics();
            var data = query.Select(e => new {    
                연락처 = e.Mobile,
                이름 = e.Name,
                나이 = e.Age,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                우편번호 = e.ZipCode,
                선택장난감 = e.SurprizeTypeDisplayName,
                총공유수 = e.TotalCount,
                페이스북공유수 = e.FacebookCount,
                카카오스토리공유수 = e.KakaostoryCount,
                카카오톡공유수 = e.KakaotalkCount
            }).ToList();
            common.ExcelDownLoad(data, "[2016 닌자바비 이벤트] 통계_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}