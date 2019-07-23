using AutoMapper;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Services.BagtotheFuture;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models.BagtotheFutureModels;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers {
    [RoutePrefix("bag-to-the-future")]
    public class BagTotheFutureController : Controller {

        private IBagtotheFutureService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public BagTotheFutureController(IBagtotheFutureService service, ICommonProvider common) {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<BagtotheFutureEntryCheckModel, BagtotheFutureEntryModel>();
            });
        }
        [Route("")]
        public ActionResult Index() {
            ViewBag.SnsType = "kakaostory";
            ViewBag.SnsUrl = "https://www.youtube.com/watch?v=-o11S_tYxPk";
            var utmMedium = Request.QueryString["utm_medium"] as string;
            var redirect = Request.QueryString["is_rd"] as string;

            if (!string.IsNullOrEmpty(utmMedium) && utmMedium.ToLower().Contains("_share")) {
                ViewBag.SnsType = utmMedium.ToLower().Replace("_share","");
                ViewBag.IsRedirect = "Y";
                if (!string.IsNullOrEmpty(redirect) && redirect.ToLower() == "y") {
                    ViewBag.IsRedirect = "N";
                }
                if (utmMedium.ToLower() != "kakaostory_share") {
                    ViewBag.SnsUrl = string.Format("https://samsonite.pentacle.co.kr/bag-to-the-future?utm_source=Promotion&utm_campaign=bagtothefuture&utm_medium={0}", utmMedium);
                }
            }
            return View();
        }

        [HttpPost]
        [Route("post", Name = "CreateCheckEntry")]
        public ActionResult Post(BagtotheFutureModel model) {
            if (ModelState.IsValid) {
                var entry = mapperConfig.CreateMapper().Map<BagtotheFutureEntryModel>(model.BagtotheFutureEntryCheckModel);
                if (model.BagtotheFutureEntryCheckModel.Attachment != null) {
                    var fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), Guid.NewGuid(), Path.GetExtension(model.BagtotheFutureEntryCheckModel.Attachment.FileName));
                    entry.File = common.S3FileUpload(model.BagtotheFutureEntryCheckModel.Attachment.InputStream,
                        "files/bag-to-the-future", fileName);
                }
                return Json(entry, "text/html");
            } else {
                string message = "";

                var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                if (errorProp != null) {
                    message = errorProp.Errors[0].ErrorMessage;
                }
                return Json(new {
                    result = false,
                    message = message
                }, "text /html");
            }
        }

        [HttpGet]
        [Route("redirect")]
        public ActionResult Redirect() {
            NameValueCollection queryString = Request.QueryString;
            return Redirect(string.Format("{0}?{1}", "https://www.samsonitemall.co.kr/bagtothefuture.html", queryString.ToString()));
        }

    }
}