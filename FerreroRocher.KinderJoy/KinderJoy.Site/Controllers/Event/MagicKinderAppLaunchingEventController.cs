using AutoMapper;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.MagicKinderAppLaunchingEvent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Event
{ 
    [RoutePrefix("event/2017-magickinderapp-launching")]
    public class MagicKinderAppLaunchingEventController : Controller
    {
        private IMagicKinderAppLaunchingService service;
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public MagicKinderAppLaunchingEventController(IMagicKinderAppLaunchingService service, ICommonProvider common)
        {
            this.service = service;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MagicKinderAppLaunchingEventModel, MagicKinderAppLaunching>();
            });
        }

        [Route("")]
        public ActionResult Index(string utm_source="")
        {
            ViewBag.AppStoreUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-ios----prod55856c53e6d3fbb512691a0897&network_id=5640&device_id=device_id&site_id=1";
            ViewBag.GooglePlayUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-android----prod55856d9720fe9d9fa6085f6c67&network_id=5640&device_id=device_id&site_id=1&append_app_conv_trk_params=1";

            if(utm_source == "Naver")
            {
                ViewBag.AppStoreUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-ios----prod55856c53e6d3fb082b22b8bc5d&network_id=5640&device_id=device_id&site_id=1";
                ViewBag.GooglePlayUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-android----prod55856d9720fe91a5616803d9e1&network_id=5640&device_id=device_id&site_id=1&append_app_conv_trk_params=1";
            }else if(utm_source == "Kakaostory")
            {
                ViewBag.AppStoreUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-ios----prod55856c53e6d3f2180eb687ba1f&network_id=5640&device_id=device_id&site_id=1";
                ViewBag.GooglePlayUrl = "https://control.kochava.com/v1/cpi/click?campaign_id=komagic-kinder-android----prod55856d9720fe94a1fff44a1fda&network_id=5640&device_id=device_id&site_id=1&append_app_conv_trk_params=1";
            }

            return View();
        }

        [Route("create", Name = "CreateMagicKinderAppLaunching")]
        [HttpPost]
        public ActionResult CreateMagicKinderAppLaunching(MagicKinderAppLaunchingEventModel model)
        {
           
            try
            {
                //if (common.Now >= new DateTime(2017, 4, 15))
                //{
                //    throw new Exception("이벤트가 종료되었습니다.");
                //}
                if (!ModelState.IsValid)
                {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null)
                    {
                        throw new Exception(errorProp.Errors[0].ErrorMessage);
                    }
                }

                var mapper = mapperConfig.CreateMapper();
                var entry = mapper.Map<MagicKinderAppLaunching>(model);

                entry.IpAddress = common.ClientIP;
                entry.Channel = common.IsMobileDevice ? "mobile" : "pc";
                entry.CreateDate = common.Now;
                entry.IsShow = false;

                var img = Image.FromStream(model.ScreenShotFile.InputStream);
                //이미지 회전 check
                if (img.PropertyIdList.Contains(0x0112))
                {
                    int rotationValue = img.GetPropertyItem(0x0112).Value[0];
                    switch (rotationValue)
                    {
                        case 1:
                            // No rotation required.
                            break;
                        case 2:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            img.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 5:
                            img.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            img.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }
                    // This EXIF data is now invalid and should be removed.
                    img.RemovePropertyItem(0x0112);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    //회전시킨 이미지를 스트림에 저장
                    img.Save(ms, ImageFormat.Jpeg);

                    //회전시킨 원본이미지 s3에 upload
                    entry.ScreenShot = common.S3FileUpload(ms, "MagicKinderAppLaunching2017", model.ScreenShotFile.FileName.ToString());
                }
                var result = service.CreateMagicKinderAppLaunching(entry);

                return Json(new
                {
                    result = true,
                    data = result
                });
            }
            catch(Exception e)
            {
                return Json(new
                {
                    result = false,
                    message = e.Message
                });
            }
        }
    }
}