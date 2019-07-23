using AutoMapper;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Services.OverFortyFiveDb;
using INGLife.Event.Models.OverFortyFiveDbModels;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INGLife.Event.Models.KMCModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace INGLife.Event.Controllers.Event
{
	[RoutePrefix("emoticon")]
	public class Orange4050Controller : Controller
    {
        private IOverFortyFiveService service;
        private IKMCService kmcService;
        private ICommonProvider common;
        private IUplusSmsApiService smsOpenApiService;

        private MapperConfiguration mapperConfig;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public Orange4050Controller(IOverFortyFiveService service, IKMCService kmcService, ICommonProvider common, IUplusSmsApiService smsOpenApiService)
        {
            this.service = service;
            this.kmcService = kmcService;
            this.common = common;
            this.smsOpenApiService = smsOpenApiService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<OverFortyFiveDbEntry, OverFortyFiveDbViewModel>();
                cfg.CreateMap<OverFortyFiveDbModel, OverFortyFiveDbEntry>();
            });
        }

        // GET: orange4050
        [Route("")]
        public ActionResult Index(string utm_source = "", string adkey="")
        {
            if (utm_source == "band")
            {
                ViewBag.url = "https://" + System.Web.HttpContext.Current.Request["HTTP_HOST"] + "/emoticon?utm_source=band&utm_medium=share&utm_campaign=emoticon";
                ViewBag.image = "https://www.orange-event.kr/Content/images/orange4050/sns/band.jpg";
            }
            else
            {
                ViewBag.url = "https://" + System.Web.HttpContext.Current.Request["HTTP_HOST"] + "/emoticon?utm_source=facebook&utm_medium=share&utm_campaign=emoticon";
                ViewBag.image = "https://www.orange-event.kr/Content/images/orange4050/sns/facebook.jpg";
            }

            if (adkey != null && adkey != "") {
                Session.Add("adkey", adkey);
            }

            var date = common.Now.ToString("yyyyMMddHHmmss");

            var urlCode = "004008";
            var trUrl = "http://dev.www.orange-event.kr/emoticon/callback";
            var kmcState = System.Configuration.ConfigurationManager.AppSettings["kmc.state"] as string;
            var kmcModel = new RequestKMCModel { };

            // 실서버 적용시 urlCode,trUrl 실서버용으로 변경
            if (kmcState == "release")
            {
                urlCode = "002007";
                trUrl = "https://www.orange-event.kr/emoticon/callback";
            }
            else if (kmcState == "debug")
            {
                urlCode = "003013";
                trUrl = "https://test.www.orange-event.kr/emoticon/callback";
            }

            // 이벤트종료일 제어
            if (!IsEventClose())
            {
                kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);
            }


            var model = new OverFortyFiveDbModels
            {
                OverFortyFiveDbModel = new OverFortyFiveDbModel { },
                RequestKMCModel = kmcModel
            };

            ViewBag.IsClose = IsEventClose();

            return View(model);
        }

        /// <summary>
        /// 본인확인 서비스 요청 결과 수신
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("callback")]
        public ActionResult Callback(ResponseKMCModel model)
        {
            if (model == null)
            {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }

        [HttpPost]
        [Route("complete-over-fortyfive-kmc", Name = "Complete4050KMC")]
        public ActionResult CompleteMarketingAgreeKMC(ResponseKMCModel model)
        {
            try
            {
                if (IsEventClose())
                {
                    throw new OverFortyFiveServiceException("400", "해당 이벤트는 선착순 마감되었습니다.", null);
                }

                var result = kmcService.ResponseKMC(model);

                if (int.Parse(result.Data.BirthDay.ToString().Substring(0, 4)) >= 1980)
                {
                    throw new OverFortyFiveServiceException("400", "40세 이상부터 가능한 이벤트입니다.\r\n[휴대폰 인증 기준, 1979년 이전 출생]\r\n(ex) 79년, 78년, 77년, …", null);
                }

                var entry = new OverFortyFiveDbEntry
                {
                    Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                    Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                    Mobile = result.Data.PhoneNo,
                    BirthDay = string.IsNullOrEmpty(result.Data.M_BirthDay) ? result.Data.BirthDay : result.Data.M_BirthDay,
                    Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                    IpAddress = common.IpAddress,
                    CreateDate = common.Now
                };

                var resultModel = service.CreateOverFortyFiveEntry(entry);
                var data = mapperConfig.CreateMapper().Map<OverFortyFiveDbViewModel>(resultModel);

                Session.Add("OverFortyFiveDbEntryId", resultModel.Id);

                return Json(new
                {
                    Result = result.Result,
                    Message = result.Message,
                    Data = data
                });
            }
            catch (KMCServiceException e)
            {
                logger.Debug(">>>>>>>>>>> Response KMC Service");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                return Json(new
                {
                    Result = false,
                    Message = string.Format("본인인증 실패되었습니다. [ 본인인증을 다시 해주시길 바랍니다. ({0}) ]", e.Message)
                });
            }
            catch (OverFortyFiveServiceException e)
            {
                return Json(new
                {
                    Result = false,
                    Message = e.Message
                });
            }
            catch (Exception e)
            {
                logger.Error(e);

                return Json(new
                {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }

        }
        [HttpPost]
        [Route("create-entry", Name = "Create4050Entry")]
        [ValidateAntiForgeryToken()]
        public ActionResult Create4050Entry(OverFortyFiveDbModels model)
        {
            int emoticonTypeOneLimit = 6000;
            int emoticonTypeTwoLimit = 10000;
            int emoticonTypeThreeLimit = 0;
            try
            {
                if (IsEventClose())
                {
                    throw new OverFortyFiveServiceException("400", "해당 이벤트는 선착순 마감되었습니다.", null);
                }

                long? userId = Session["OverFortyFiveDbEntryId"] as long?;
                if (!userId.HasValue)
                {
                    throw new OverFortyFiveServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }
                var overFortyFiveDbModel = model.OverFortyFiveDbModel;

                if (int.Parse(overFortyFiveDbModel.BirthDay.ToString().Substring(0, 4)) >= 1980)
                {
                    throw new OverFortyFiveServiceException("400", "40세 이상부터 가능한 이벤트입니다.\r\n[휴대폰 인증 기준, 1979년 이전 출생]\r\n(ex) 79년, 78년, 77년, …", null);
                }

                if (!ModelState.IsValid)
                {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null)
                    {
                        throw new OverFortyFiveServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }

                // 연락방식 체크
                if (!model.OverFortyFiveDbModel.AllCheck)
                {
                    return Json(new
                    {
                        Result = false,
                        IsRequiredAllCheck = false,
                        Message = "전화와 문자메시지, 두 가지 항목에 수신 동의를 하셔야 이벤트 참여가 가능합니다."
                    });
                }

                int emoticonCount = service.GetEmoticonTypeCount(model.OverFortyFiveDbModel.EmoticonType);
                int emoticonLimit = emoticonTypeThreeLimit;
                if (model.OverFortyFiveDbModel.EmoticonType == EmoticonType.Emoticon1)
                {
                    emoticonLimit = emoticonTypeOneLimit;
                }
                else if (model.OverFortyFiveDbModel.EmoticonType == EmoticonType.Emoticon2)
                {
                    emoticonLimit = emoticonTypeTwoLimit;
                }

                if (emoticonCount >= emoticonLimit) {
                    return Json(new
                    {
                        Result = false,
                        Message = "해당 이모티콘은 소진되었습니다. 이모티콘을 재선택 해주세요."
                    });
                }

                var user = service.GetOverFortyFiveEntryById(userId.Value);
                // 기존 저장 개인정보와 다른값 체크
                if (user.Name != overFortyFiveDbModel.Name || user.Mobile != overFortyFiveDbModel.Mobile || user.Gender != overFortyFiveDbModel.Gender || user.BirthDay != overFortyFiveDbModel.BirthDay)
                {
                    throw new OverFortyFiveServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                }
                var entry = mapperConfig.CreateMapper().Map<OverFortyFiveDbModel, OverFortyFiveDbEntry>(overFortyFiveDbModel, user);
                //var message = string.Format("[오렌지라이프] \r\n마케팅동의처리 안내\r\n안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프에 신청하신 '상품의 소개 등을 위한 개인(신용)정보 처리동의' (마케팅동의)가 정상 반영되어 안내 드립니다.", common.Now.Year, common.Now.Month, common.Now.Day);

                //entry.IsMessage = smsOpenApiService.SendMMS("", entry.Mobile, "0222009867", message, "L");    
                entry.UpdateDate = common.Now;

                if (Session["adkey"] != null)
                {
                    var result = callTnkPostbackUrl(Session["adkey"].ToString());

                    entry.TnkAdKey = Session["adkey"].ToString();
                    entry.TnkResult = result;
                    entry.TnkUpdateDate = common.Now;
                }
                service.UpdateOverFortyFiveEntry(entry);

                DateTime fridayString = DateTime.Today.AddDays(Convert.ToInt32(DayOfWeek.Friday) - Convert.ToInt32(DateTime.Today.DayOfWeek));

                if (DateTime.Today.DayOfWeek.ToString() == DayOfWeek.Friday.ToString() && DateTime.Today.Day > (int)DayOfWeek.Thursday)
                {
                    fridayString = DateTime.Today.AddDays(Convert.ToInt32(DayOfWeek.Friday) - Convert.ToInt32(DateTime.Today.DayOfWeek)).AddDays(7);
                }
                String dayString = fridayString.Day > 9 ? fridayString.Day.ToString() : "0" + fridayString.Day.ToString();

                var successMessge = "마케팅동의 및 이벤트 참여가 완료되었습니다.\r\n 이모티콘 지급일은 "+ fridayString.Month + "/"+ dayString + "(금)입니다.\r\n(카카오톡 이모티콘은 플러스 친구로 발송)";

                Session.Remove("adkey");
                Session.Remove("OverFortyFiveDbEntryId");

                return Json(new
                {
                    Result = true,
                    Message = successMessge
                });
            }
            catch (OverFortyFiveServiceException e)
            {
                return Json(new
                {
                    Result = false,
                    IsRequiredAllCheck = true,
                    Message = e.Message
                });
            }
            catch (Exception e)
            {
                logger.Error(e);
                return Json(new
                {
                    Result = false,
                    IsRequiredAllCheck = true,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }

        public string callTnkPostbackUrl(string adkey) {
            var client = new HttpClient();

            //string adkey = Session["adkey"].ToString();
            string appKey = ConfigurationManager.AppSettings["tnk,appkey"] as string;

            var url = "http://api2.tnkfactory.com/tnk/ad.g.ad?adkey="+ adkey + "&appkey=" + appKey;
            var response = client.GetAsync(url).Result;
            string responseString = "";

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                responseString = responseContent.ReadAsStringAsync().Result;
            }
            return responseString;
        }


        // 이벤트 종료일
        private bool IsEventClose()
        {
            return common.Now >= new DateTime(2018, 11, 1);
        }
    }
}