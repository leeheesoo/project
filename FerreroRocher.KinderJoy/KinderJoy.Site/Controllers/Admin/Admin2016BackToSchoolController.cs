using KinderJoy.Domain.Repository.BackToSchool2016;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/2016-backtoschool")]
    public class Admin2016BackToSchoolController : Controller
    {
        private IBackToSchool2016Repository repository;
        private ICommonProvider common;
        public Admin2016BackToSchoolController(IBackToSchool2016Repository repository, ICommonProvider common)
        {
            this.repository = repository;
            this.common = common;
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

        [Route("ExcelDownload")]
        public void ExcelDownload(string Type)
        {
            if (Type.ToLower().Equals("index"))
            {
                var query = repository.BackToSchool2016BingoQuiz.Select(e => new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuiz
                {
                    Name = e.Name,
                    Mobile = e.Mobile,
                    ZipCode = e.ZipCode,
                    Address1 = e.Address1,
                    Address2 = e.Address2,
                    Age = e.Age,
                    IpAddress = e.IpAddress,
                    Channel = e.Channel,
                    CreateDate = e.CreateDate
                });
                var data = query.OrderByDescending(e => e.CreateDate).AsEnumerable().Select(e => new
                {
                    참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    디바이스 = e.Channel,
                    아이피주소 = e.IpAddress,
                    이름 = e.Name,
                    연락처 = e.Mobile,
                    나이 = e.Age,
                    우편번호 = e.ZipCode,
                    기본주소 = e.Address1,
                    상세주소 = e.Address2
                }).ToList();

                common.ExcelDownLoad(data, "[2016 새학기 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            }else if (Type.ToLower().Equals("sns"))
            {
                var query = repository.BackToSchool2016BingoQuizSns.Select(e => new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNS
                {
                    IpAddress = e.BackToSchool2016BingoQuiz.IpAddress,
                    Channel = e.BackToSchool2016BingoQuiz.Channel,
                    Name = e.BackToSchool2016BingoQuiz.Name,
                    Mobile = e.BackToSchool2016BingoQuiz.Mobile,
                    ZipCode = e.BackToSchool2016BingoQuiz.ZipCode,
                    Address1 = e.BackToSchool2016BingoQuiz.Address1,
                    Address2 = e.BackToSchool2016BingoQuiz.Address2,
                    Age = e.BackToSchool2016BingoQuiz.Age,
                    SnsType = e.SnsType,
                    SnsId = e.SnsId,
                    SnsName = e.SnsName,
                    SnsNickName = e.SnsNickName,
                    ScrapUrl = e.ScrapUrl,
                    RegisterDate = e.RegisterDate
                }).OrderByDescending(e => e.RegisterDate);

                var sql = query.AsEnumerable().Select(e => new {
                    디바이스 = e.Channel,
                    이름 = e.Name,
                    연락처 = e.Mobile,
                    주소 = e.Address,
                    나이 = (e.Age > 0 ? e.Age.ToString() : ""),
                    SNS유형 = e.SnsType,
                    SNS아이디 = e.SnsId,
                    SNS이름 = e.SnsName,
                    SNS닉네임 = e.SnsNickName,
                    SNS스크랩URL = e.ScrapUrl,
                    아이피주소 = e.IpAddress,
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
                string filename = "[2016 새학기 이벤트] SNS공유_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            }else {
                var sns = repository.BackToSchool2016BingoQuiz.AsQueryable()
                    .Join(repository.BackToSchool2016BingoQuizSns, e => e.Id, p => p.BackToSchool2016BingoQuizId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name });

                var query = from s in sns
                            group s by s.Mobile into BingoQuizSns
                            select new Models.Admin.BackToSchool2016.BackToSchool2016BingoQuizSNSStats
                            {
                                Mobile = BingoQuizSns.Key,
                                Name = BingoQuizSns.Max(e => e.Name),
                                FacebookCount = BingoQuizSns.Count(e => e.SnsType == "facebook"),
                                KakaostoryCount = BingoQuizSns.Count(e => e.SnsType == "kakaostory"),
                                KakaotalkCount = BingoQuizSns.Count(e => e.SnsType == "kakaotalk"),
                                TotalCount = BingoQuizSns.Count()
                            };
                var sql = query.OrderByDescending(e => e.TotalCount).AsEnumerable().Select(e => new {
                    연락처 = e.Mobile,
                    이름 = e.Name,
                    총공유수 = e.TotalCount,
                    페이스북공유수 = e.FacebookCount,
                    카카오스토리공유수 = e.KakaostoryCount,
                    카카오톡공유수 = e.KakaotalkCount
                }).ToList();
                string filename = "[2016 새학기 이벤트] SNS공유통계_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            }
        }
    }
}