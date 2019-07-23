using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Services.FinancialConcertMarketingAgree;
using INGLife.Event.Infrastructures;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using INGLife.Event.Models;
using INGLife.Event.Models.FinancialConcertMarketingAgreeModels;
using INGLife.Event.Models.KMCModels;
using NLog;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace INGLife.Event.Controllers.Event {
    [RoutePrefix("concert")]
    public class FinancialConcertMarketingAgreeController : Controller {

        #region variables
        private ICommonProvider common;

        private IKMCService kmcService;
        private IFinancialConcertMarketingAgreeService service;
        private IUplusSmsApiService smsService;

        private MapperConfiguration mapperConfig;

        private Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region constructor
        public FinancialConcertMarketingAgreeController (ICommonProvider common, IKMCService kmcService, IFinancialConcertMarketingAgreeService service, IUplusSmsApiService smsService) {
            this.common = common;
            this.kmcService = kmcService;
            this.service = service;
            this.smsService = smsService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConcertMarketingAgreeEntry, FinancialConcertMarketingAgreeCertModel>();
                cfg.CreateMap<FinancialConcertMarketingAgreeCreateModel, FinancialConcertMarketingAgreeEntry>().ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? string.Format("{0}@{1}", e.Email1, e.Email2) : null));
            });
        }
        #endregion

        [Route("")]
        public ActionResult Index () {
            var urlCode = "";
            var trUrl = "";

            var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            switch (kmcState) {
                case "local":
                default:
                    urlCode = "004009";
                    trUrl = "http://dev.www.orange-event.kr/concert/callback";
                    break;
                case "debug":
                    urlCode = "003014";
                    trUrl = "https://test.www.orange-event.kr/concert/callback";
                    break;
                //TODO: 실서버 적용시 urlCode,trUrl 실서버용으로 변경
                case "release":
                    urlCode = "002008";
                    trUrl = "https://www.orange-event.kr/concert/callback";
                    break;
            }

            var date = common.Now.ToString("yyyyMMddHHmmss");
            var kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);            
            var model = new FinancialConcertMarketingAgreeModel {
                FinancialConcertMarketingAgreeCreateModel = new FinancialConcertMarketingAgreeCreateModel(),
                FinancialConcertMarketingAgreeKMCModel = kmcModel
            };

            ViewBag.AvailableApplicationTurn = 3;
            if (common.Now >= new DateTime(2018, 10, 30, 6, 0, 0)) {
                ViewBag.AvailableApplicationTurn = 0;
            } else if (common.Now >= new DateTime(2018, 10, 23, 23, 0, 0)) {
                ViewBag.AvailableApplicationTurn = 1;
            } else if (common.Now >= new DateTime(2018, 10, 16, 6, 0, 0)) {
                ViewBag.AvailableApplicationTurn = 2;
            }

            return View(model);
        }

        /// <summary>
        /// 본인확인 서비스 요청 결과 수신(마케팅동의)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("callback")]
        public ActionResult Callback (ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }


        [HttpPost]
        [Route("kmc/response", Name = "ResponseFinancialConcertMarketingAgreeKMC")]
        public JsonResult ResponseFinancialConcertMarketingAgreeKMC (ResponseKMCModel model) {
            //var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            var result = new JsonResultModel { Result = true, Message = "이미 종료된 이벤트입니다." };
            /*
            try {
                //response kmc
                var responseKmc = kmcService.ResponseKMC(model);

                //create entry
                var entry = new FinancialConcertMarketingAgreeEntry {
                    Name = string.IsNullOrEmpty(responseKmc.Data.M_Name) ? responseKmc.Data.Name : responseKmc.Data.M_Name,
                    Gender = ((string.IsNullOrEmpty(responseKmc.Data.M_Gender) ? responseKmc.Data.Gender : responseKmc.Data.M_Gender) == "0" ? "남" : "여"),
                    Mobile = responseKmc.Data.PhoneNo,
                    BirthDay = string.IsNullOrEmpty(responseKmc.Data.M_BirthDay) ? responseKmc.Data.BirthDay : responseKmc.Data.M_BirthDay,
                    Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                    IpAddress = common.IpAddress,
                    CreateDate = common.Now
                };
                long entryId = service.Create(entry);

                //session save
                Session["FINANCIAL_CONCERT_MARKETING_AGREE_ENTRY_ID"] = entryId;

                var responseData = mapperConfig.CreateMapper().Map<FinancialConcertMarketingAgreeCertModel>(entry);

                result.Result = responseKmc.Result;
                result.Message = responseKmc.Message;
                result.Data = responseData;

            } catch (KMCServiceException e) {
                logger.Info(">>>>>>>> 재무콘서트 KMC Service Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                result.Message = string.Format("본인인증 실패되었습니다. [본인인증을 다시 해주시길 바랍니다. ({0})]", e.Message);
            } catch (EventServiceException e) {
                result.Message = e.Message;
            } catch (Exception e) {
                logger.Info(">>>>>>>> 재무콘서트 Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            */
            return Json(result);
        }

        /// <summary>
        /// 개인정보 저장 완료
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("entry/create", Name = "CreateFinancialConcertMarketingAgreeEntry")]
        [ValidateAntiForgeryToken]
        public JsonResult CreateFinancialConcertMarketingAgreeEntry (FinancialConcertMarketingAgreeModel model) {
            // var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            var result = new JsonResultModel { Result = true, Message = "이미 종료된 이벤트입니다." };
            /*
            try {
                //session check
                long? entryId = Session["FINANCIAL_CONCERT_MARKETING_AGREE_ENTRY_ID"] as long?;
                if (!entryId.HasValue) {
                    throw new EventServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }

                //model validation
                if (!ModelState.IsValid) {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null) {
                        throw new EventServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }

                //check application turn
                var availableApplicationTurn = checkAvailableApplicationTurn(common.Now, model.FinancialConcertMarketingAgreeCreateModel.ApplicationTurn);
                if (!availableApplicationTurn) {
                    throw new EventServiceException("400", "해당 회차는 이미 종료되었습니다.", null);
                }

                //set createModel
                var updateModel = model.FinancialConcertMarketingAgreeCreateModel;

                //check cert data
                var data = service.CheckCertEntry(entryId.Value, updateModel.Name.Trim().ToLower(), updateModel.Mobile.Trim(), updateModel.Gender, updateModel.BirthDay);
                if (data == null) {
                    throw new EventServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                }

                //mapper model to updateEntry
                var updateEntry = mapperConfig.CreateMapper().Map<FinancialConcertMarketingAgreeCreateModel, FinancialConcertMarketingAgreeEntry>(updateModel, data);

                //send message
                var message = string.Format("안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프생명보험(주)에 '상품의 소개 등을 위한 개인(신용)정보 처리동의'(마케팅동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제 2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                updateEntry.IsMessage = smsService.SendMMS("", updateEntry.Mobile, "0222009806", message, "L");

                //save            
                updateEntry.CompleteDate = common.Now;
                service.Update(updateEntry);

                //session remove
                Session.Remove("FINANCIAL_CONCERT_MARKETING_AGREE_ENTRY_ID");

                result.Result = true;
                result.Message = "완료되었습니다.";
            } catch (EventServiceException e) {
                result.Message = e.Message;
            } catch (Exception e) {
                logger.Info(">>>>>>>> 재무콘서트 개인정보 저장 완료 (call CreateFinancialConcertMarketingAgreeEntry) Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            */
            return Json(result);
        }

        private bool checkAvailableApplicationTurn (DateTime today, FinancialConcertMarketingAgreeApplicationTurnEnum applicationTurn) {
            if (applicationTurn == FinancialConcertMarketingAgreeApplicationTurnEnum.FIRST && today >= new DateTime(2018, 10, 16, 6, 0, 0)) {
                return false;
            } else if (applicationTurn == FinancialConcertMarketingAgreeApplicationTurnEnum.SECOND && today >= new DateTime(2018, 10, 23, 23, 0, 0)) {
                return false;
            } else if (applicationTurn == FinancialConcertMarketingAgreeApplicationTurnEnum.THIRD && today >= new DateTime(2018, 10, 30, 6, 0, 0)) {
                return false;
            }
            return true;
        }
    }
}