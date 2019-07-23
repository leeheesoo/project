using KinderJoy.Domain.Entities.MavelFrozen;
using KinderJoy.Domain.Models.MarvelFrozen;
using KinderJoy.Domain.Service.MavelFrozen;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2017-marvel-frozen")]
    public class AdminMarvelFrozenController : Controller
    {
        private ICommonProvider common;
        private IMavelFrozenService service;

        public AdminMarvelFrozenController(ICommonProvider common, IMavelFrozenService service) {
            this.common = common;
            this.service = service;
        }

        [Route("")]
        // GET: AdminMarvelFrozen
        public ActionResult Index()
        {
            return View();
        }

        [Route("sns")]
        public ActionResult Sns() {
            return View();
        }

        [Route("SnsStats")]
        public ActionResult SnsStats() {
            return View();
        }

        [Route("user-excel-download")]
        public void UserExcelDownload(AdminMarvelFrozenQueryOptions options) {
            var query = service.GetUser(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.Channel,
                IP = e.IpAddress,
                아이성별 = e.ChildGenderDisplayName,
                이름 = e.Name,
                연락처 = e.Mobile,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                우편번호 = e.ZipCode,
                나이 = e.Age
            }).ToList();
            common.ExcelDownLoad(data, "[2017 마블프로즌 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
        [Route("sns-excel-download")]
        public void SnsExcelDownload(AdminMarvelFrozenSNSQueryOptions options) {
            var query = service.GetSns(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                공유일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.User.Channel,
                이름 = e.User.Name,
                연락처 = e.User.Mobile,
                나이 = e.User.Age,
                SNS = e.SnsTypeDisplayName,
                아이디 = e.SnsId,
                닉네임 = e.SnsName,
                URL = e.Post,
                IP = e.User.IpAddress
            }).ToList();
            common.ExcelDownLoad(data, "[2017 마블프로즌 이벤트] 공유_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("stats-excel-download")]
        public void StatsExcelDownload() {
            var query = service.GetStatistics().OrderByDescending(x => x.TotalCount);
            var data = query.Select(e => new {
                연락처 = e.Mobile,
                이름 = e.Name,
                나이 = e.Age,
                아이성별 = e.ChildGender == GenderType.Boy ? "남아" : "여아",
                총공유수 = e.TotalCount,
                페이스북공유수 = e.FacebookCount,
                카카오스토리공유수 = e.KakaostoryCount,
                카카오톡공유수 = e.KakaotalkCount
            }).ToList();
            common.ExcelDownLoad(data, "[2017 마블프로즌 이벤트] 통계_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}