using KinderJoy.Domain.Models.FindingDory2017;
using KinderJoy.Domain.Service.FindingDory2017;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2017-findingdory")]
    public class AdminFindingDory2017Controller : Controller
    {
        private ICommonProvider common;
        private IFindingDory2017Service service;

        public AdminFindingDory2017Controller(ICommonProvider common,IFindingDory2017Service service)
        {
            this.common = common;
            this.service = service;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("sns")]
        public ActionResult Sns()
        {
            return View();
        }

        [Route("SnsStats")]
        public ActionResult SnsStats()
        {
            return View();
        }


        [Route("user-excel-download")]
        public void UserExcelDownload(AdminFindingDory2017QueryOptions options)
        {
            var query = service.GetUser(options).OrderByDescending(x => x.CreateDate).ToList();
            var data = query.Select(e => new {
                참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                디바이스 = e.Channel,
                IP = e.IpAddress,
                이름 = e.Name,
                연락처 = e.Mobile,
                주소 = e.Address,
                상세주소 = e.AddressDetail,
                우편번호 = e.ZipCode,
                나이 = e.Age
            }).ToList();
            common.ExcelDownLoad(data, "[2017 도리를찾아서 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
        [Route("sns-excel-download")]
        public void SnsExcelDownload(AdminFindingDory2017SNSQueryOptions options)
        {
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
            common.ExcelDownLoad(data, "[2017 도리를찾아서 이벤트] 공유_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [Route("stats-excel-download")]
        public void StatsExcelDownload()
        {
            var query = service.GetStatistics().OrderByDescending(x => x.TotalCount);
            var data = query.Select(e => new {
                연락처 = e.Mobile,
                이름 = e.Name,
                총공유수 = e.TotalCount,
                페이스북공유수 = e.FacebookCount,
                카카오스토리공유수 = e.KakaostoryCount,
                카카오톡공유수 = e.KakaotalkCount
            }).ToList();
            common.ExcelDownLoad(data, "[2017 도리를찾아서 이벤트] 통계_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}