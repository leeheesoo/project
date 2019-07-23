using AutoMapper;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Services.TumblerEvent;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using INGLife.Event.Models;
using INGLife.Event.Models.KMCModels;
using INGLife.Event.Models.TumblerEventModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace INGLife.Event.Controllers.Event
{
	[RoutePrefix("tumbler")]
	public class TumblerEventController : Controller
	{
        private ICommonProvider common;
        private IKMCService kmcService;
        private IUplusSmsApiService smsService;
        private MapperConfiguration mapperConfig;
        private ITumblerEventService tumblerService;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public TumblerEventController(ICommonProvider common, IKMCService kmcService, IUplusSmsApiService smsService, ITumblerEventService tumblerService) {
            this.common = common;
            this.kmcService = kmcService;
            this.smsService = smsService;
            this.tumblerService = tumblerService;

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TumblerEventEntry, TumblerCreateModel>();
                cfg.CreateMap<TumblerCreateModel, TumblerEventEntry>().ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? string.Format("{0}@{1}", e.Email1, e.Email2) : null));
            });
        }

        [Route("")]
		public ActionResult Index()
		{
            var urlCode = "";
            var trUrl = "";
            var url = "";
            var image = "";

            var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            switch (kmcState)
            {
                case "local":
                default:
                    urlCode = "004010";
                    trUrl = "http://dev.www.orange-event.kr/tumbler/callback";
                    url = "https://test.www.orange-event.kr/tumbler";
                    image = "https://test.www.orange-event.kr/Content/images/TumblerEvent/sns/sns_1115.jpg";
                    break;
                case "debug":
                    urlCode = "003015";
                    trUrl = "https://test.www.orange-event.kr/tumbler/callback";
                    url = "https://test.www.orange-event.kr/tumbler";
                    image = "https://test.www.orange-event.kr/Content/images/TumblerEvent/sns/sns_1115.jpg";
                    break;
                case "release":
                    urlCode = "002011";
                    trUrl = "https://www.orange-event.kr/tumbler/callback";
                    url = "https://www.orange-event.kr/tumbler";
                    image = "https://www.orange-event.kr/Content/images/TumblerEvent/sns/sns_1115.jpg";
                    break;
            }

            var date = common.Now.ToString("yyyyMMddHHmmss");
            var tumblerkmcModel = kmcService.RequestPlusInfoKMC(date, urlCode, trUrl, "tumbler");
            //var secretBoxModel = kmcService.RequestPlusInfoKMC(date, urlCode, trUrl, "secret");
            var model = new TumblerEventModel
            {
                TumblerCreateModel = new TumblerCreateModel { Today = common.Now },
                //SecretBoxModel = new SecretBoxModel { Today = common.Now},
                TumblerAgreeKMCModel = tumblerkmcModel,
                //SecretBoxAgreeKMCModel = secretBoxModel
            };

            ViewBag.IsEvnetOneClose = IsEventOneClose();
            ViewBag.IsEvnetTwoClose = IsEventTwoClose();
            ViewBag.image = image;
            ViewBag.url = url;

            return View(model);
        }

        /// <summary>
        /// 본인확인 서비스 요청 결과 수신(마케팅동의)
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
        [Route("kmc/response", Name = "ResponseTumblerEventAgreeKMC")]
        public ActionResult ResponseTumblerEventAgreeKMC(ResponseKMCModel model)
        {
            try
            {
                //response kmc
                var responseKmc = kmcService.ResponseKMC(model);
                if (tumblerService.CheckEntry(responseKmc.Data.Name.Trim().ToLower(), responseKmc.Data.PhoneNo.Trim(), responseKmc.Data.Gender, responseKmc.Data.BirthDay) != null) {
                    throw new EventServiceException("400", "이미 참여하셨습니다.", null);
                }
                var tumblerEntry = new TumblerEventEntry();
                tumblerEntry = new TumblerEventEntry
                {
                    Name = string.IsNullOrEmpty(responseKmc.Data.M_Name) ? responseKmc.Data.Name : responseKmc.Data.M_Name,
                    Gender = string.IsNullOrEmpty(responseKmc.Data.M_Gender) ? responseKmc.Data.Gender : responseKmc.Data.M_Gender,
                    Mobile = responseKmc.Data.PhoneNo,
                    BirthDay = string.IsNullOrEmpty(responseKmc.Data.M_BirthDay) ? responseKmc.Data.BirthDay : responseKmc.Data.M_BirthDay,
                };
                    
                Session["MARKETING_AGREE_NAME"] = tumblerEntry.Name;
                Session["MARKETING_AGREE_GENDER"] = tumblerEntry.Gender;
                Session["MARKETING_AGREE_MOBILE"] = tumblerEntry.Mobile;
                Session["MARKETING_AGREE_BRITHDAY"] = tumblerEntry.BirthDay;

                var responseData = mapperConfig.CreateMapper().Map<TumblerCreateModel>(tumblerEntry);

                return Json(new
                {
                    Result = responseKmc.Result,
                    Message = responseKmc.Message,
                    EventType = responseKmc.Data.PlusInfo,
                    Data = responseData
                });
            }
            catch (KMCServiceException e)
            {
                logger.Info(">>>>>>>> 텀블러이벤트 KMC Service Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                return Json(new
                {
                    Result = false,
                    Message = string.Format("본인인증 실패되었습니다. [ 본인인증을 다시 해주시길 바랍니다. ({0}) ]", e.Message)
                });
            }
            catch (EventServiceException e)
            {
                return Json(new
                {
                    Result = false,
                    Message = e.Message
                });
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> 텀블러이벤트 Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                return Json(new
                {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 개인정보 저장 완료
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("entry/tumbler_create", Name = "CreateTumblerMarketingAgreeEntry")]
        [ValidateAntiForgeryToken]
        public JsonResult CreateTumblerMarketingAgreeEntry(TumblerEventModel model)
        {
            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            try
            {
                //session check
                string name = Session["MARKETING_AGREE_NAME"] as string;
                string gender = Session["MARKETING_AGREE_GENDER"] as string;
                string mobile = Session["MARKETING_AGREE_MOBILE"] as string;
                string birthDay = Session["MARKETING_AGREE_BRITHDAY"] as string;

                if (name == null || gender == null || mobile == null || birthDay == null)
                {
                    throw new EventServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }

                //model validation
                if (!ModelState.IsValid)
                {
                    if (model.TumblerCreateModel.EventType == "tumbler") {
                        ModelState.Remove("TumblerCreateModel.InterestCheck");
                        ModelState.Remove("TumblerCreateModel.SalaryCheck");
                        ModelState.Remove("TumblerCreateModel.SavingCheck");
                        ModelState.Remove("TumblerCreateModel.FundCheck");
                        ModelState.Remove("TumblerCreateModel.EtcCheck");
                    }
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null)
                    {
                        throw new EventServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }

                //set createModel
                var createModel = model.TumblerCreateModel;
                createModel.Name = name;
                createModel.Mobile = mobile;
                createModel.Gender = gender;
                createModel.BirthDay = birthDay;

                //count check
                var eventTypeCount = tumblerService.getEventTypeCount(model.TumblerCreateModel.EventType);
                if (model.TumblerCreateModel.EventType == "tumbler" && eventTypeCount >= 3500) {
                    throw new EventServiceException("400", "선착순 마감되었습니다.", null);
                }
                if (model.TumblerCreateModel.EventType == "secret" && eventTypeCount >= 200)
                {
                    throw new EventServiceException("400", "선착순 마감되었습니다.", null);
                }

                //check data
                var tumblerEventData = tumblerService.CheckEntry(createModel.Name.Trim().ToLower(), createModel.Mobile.Trim(), createModel.Gender, createModel.BirthDay);
                if (tumblerEventData != null) // 시크릿박스 or  텀블러 참여 가능
                {
                    throw new EventServiceException("400", "이미 참여하셨습니다.", null);
                }

                //mapper model to createEntry
                var createEntry = mapperConfig.CreateMapper().Map<TumblerEventEntry>(createModel);
                //send message
                var message = string.Format("안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프생명보험(주)에 '상품의 소개 등을 위한 개인(신용)정보 처리동의'(마케팅동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제 2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                createEntry.IsMessage = smsService.SendMMS("", createEntry.Mobile, "0222008606", message, "L");
               
                //save
                createEntry.CompleteDate = common.Now;
                createEntry.CreateDate = common.Now;
                createEntry.Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC;
                createEntry.IpAddress = common.IpAddress;
                tumblerService.Create(createEntry);

                //session remove
                Session.Remove("MARKETING_AGREE_NAME");
                Session.Remove("MARKETING_AGREE_GENDER");
                Session.Remove("MARKETING_AGREE_MOBILE");
                Session.Remove("MARKETING_AGREE_BRITHDAY");

                result.Result = true;
                result.Message = "완료되었습니다.";
            }
            catch (EventServiceException e)
            {
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> 재무콘서트 개인정보 저장 완료 (call CreateFinancialConcertMarketingAgreeEntry) Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }

            return Json(result);
        }

        // 이벤트 종료일
        private bool IsEventOneClose()
        {
            return tumblerService.getEventTypeCount("tumbler") >= 3500;
        }

        private bool IsEventTwoClose()
        {
            return tumblerService.getEventTypeCount("secret") >= 200;
        }
    }
}