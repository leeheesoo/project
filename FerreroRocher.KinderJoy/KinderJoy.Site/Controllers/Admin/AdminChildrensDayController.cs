using KinderJoy.Domain.Repository.ChildrensDay;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin {
    [Authorize]
    [RoutePrefix("manager/childrensday")]
    public class AdminChildrensDayController : Controller {
        private IChildrensDayRepository repository;
        private ICommonProvider common;

        public AdminChildrensDayController(IChildrensDayRepository repository, ICommonProvider common) {
            this.repository = repository;
            this.common = common;
        }

        [Route("")]
        public ActionResult Index() {
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

        [Route("ExcelDownload")]
        public void ExcelDownload(string Type) {
            if (Type.ToLower().Equals("index")) {
                var query = repository.ChildrensDayHiddenPicture.Select(e => new Models.Admin.ChildrensDay.ChildrensDayHiddenPicture {
                    Name = e.Name,
                    Mobile = e.Mobile,
                    Age = e.Age,
                    Gender = e.Gender,
                    IpAddress = e.IpAddress,
                    Channel = e.Channel,
                    CreateDate = e.CreateDate
                });
                var data = query.OrderByDescending(e => e.CreateDate).AsEnumerable().Select(e => new {
                    참여일 = e.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    디바이스 = e.Channel,
                    아이피주소 = e.IpAddress,
                    이름 = e.Name,
                    나이 = e.Age,
                    연락처 = e.Mobile,
                    장난감 = e.Gender,
                }).ToList();

                common.ExcelDownLoad(data, "[2016 어린이날 이벤트] 참여자_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            } else if (Type.ToLower().Equals("sns")) {
                var query = repository.ChildrensDayHiddenPictureSns.Select(e => new Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNS {
                    IpAddress = e.ChildrensDayHiddenPicture.IpAddress,
                    Channel = e.ChildrensDayHiddenPicture.Channel,
                    Name = e.ChildrensDayHiddenPicture.Name,
                    Mobile = e.ChildrensDayHiddenPicture.Mobile,
                    Age = e.ChildrensDayHiddenPicture.Age,
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
                    나이 = (e.Age > 0 ? e.Age.ToString() : ""),
                    연락처 = e.Mobile,
                    주소 = e.Address,
                    SNS유형 = e.SnsType,
                    SNS아이디 = e.SnsId,
                    SNS이름 = e.SnsName,
                    SNS닉네임 = e.SnsNickName,
                    SNS스크랩URL = e.ScrapUrl,
                    아이피주소 = e.IpAddress,
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
                string filename = "[2016 어린이날 이벤트] SNS공유_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else {
                var sns = repository.ChildrensDayHiddenPicture.AsQueryable()
                    .Join(repository.ChildrensDayHiddenPictureSns, e => e.Id, p => p.ChildrensDayHiddenPictureId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name ,Age = e.Age});

                var query = from s in sns
                            group s by s.Mobile into HiddenPicture
                            select new Models.Admin.ChildrensDay.ChildrensDayHiddenPictureSNSState {
                                Mobile = HiddenPicture.Key,
                                Name = HiddenPicture.Max(e => e.Name),
                                Age = HiddenPicture.Max(e => e.Age),
                                FacebookCount = HiddenPicture.Count(e => e.SnsType == "facebook"),
                                KakaostoryCount = HiddenPicture.Count(e => e.SnsType == "kakaostory"),
                                KakaotalkCount = HiddenPicture.Count(e => e.SnsType == "kakaotalk"),
                                TotalCount = HiddenPicture.Count()
                            };
                var sql = query.OrderByDescending(e => e.TotalCount).AsEnumerable().Select(e => new {
                    연락처 = e.Mobile,
                    이름 = e.Name,
                    나이 = (e.Age > 0 ? e.Age.ToString() : ""),
                    총공유수 = e.TotalCount,
                    페이스북공유수 = e.FacebookCount,
                    카카오스토리공유수 = e.KakaostoryCount,
                    카카오톡공유수 = e.KakaotalkCount
                }).ToList();
                string filename = "[2016 어린이날 이벤트] SNS공유통계_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            }
        }
    }
}