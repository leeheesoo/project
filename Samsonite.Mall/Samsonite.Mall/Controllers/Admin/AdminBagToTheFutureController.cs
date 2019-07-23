using LinqKit;
using Samsonite.Mall.Domain.Services.BagtotheFuture;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models.BagtotheFutureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("manager/bag-to-the-future")]
    public class AdminBagToTheFutureController : Controller
    {
        private IBagtotheFutureService service;
        private ICommonProvider common;

        public AdminBagToTheFutureController(IBagtotheFutureService service, ICommonProvider common) {
            this.service = service;
            this.common = common;
        }

        [Route("")]
        public ActionResult Index() {
            return View();
        }

        [Route("sns-user")]
        public ActionResult SnsUser() {
            return View();
        }

        [Route("sns-share")]
        public ActionResult SnsShare() {
            return View();
        }

        [Route("excel-download")]
        public void ExcelDownload(BagtotheFutureEntryQueryOptions options) {
            var entry = service.GetBagtotheFutureEntries().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate).Select(x => new {
                참여일 = x.CreateDate,
                채널 = x.Channel,
                아이피주소 = x.IpAddress,
                이름 = x.Name,
                연락처 = x.Mobile,
                이메일 = x.Email,
                아이디어명 = x.IdeaName,
                아이디어설명 = x.IdeaDescription,
                첨부파일 = x.File
            });

            common.ExcelDownLoad(entry, "bag-to-the-future-event01-entry");
        }

        [Route("excel-download/sns-user")]
        public void ExcelDownloadSnsUser(BagtotheFutureSnsUserQueryOptions options) {
            var entry = service.GetBagtotheFutureSnsUsers().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate).Select(x => new {
                    참여일 = x.CreateDate,
                    채널 = x.Channel,
                    아이피주소 = x.IpAddress,
                    이름 = x.Name,
                    연락처 = x.Mobile
                });

            common.ExcelDownLoad(entry, "bag-to-the-future-event02-user");
        }


        [Route("excel-download/sns")]
        public void ExcelDownloadSns(BagtotheFutureSnsQueryOptions options) {
            var entry = service.GetBagtotheFutureSns().AsQueryable().AsExpandable()
                .Where(options.BuildPredicate()).OrderByDescending(x => x.CreateDate).Select(x => new {
                    참여일 = x.CreateDate,
                    채널 = x.User.Channel,
                    아이피주소 = x.User.IpAddress,
                    이름 = x.User.Name,
                    연락처 = x.User.Mobile,
                    SNS유형 = x.SnsTypeDisplayName,
                    SNS닉네임 = x.SnsName,
                    SNS스크랩URL = x.Post
                });

            common.ExcelDownLoad(entry, "bag-to-the-future-event02-sns");
        }
    }
}