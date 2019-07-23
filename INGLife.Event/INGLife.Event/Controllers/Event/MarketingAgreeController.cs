using AutoMapper;
using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Services.MarketingAgree;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using INGLife.Event.Message.Services.Message;
using INGLife.Event.Models.KMCModels;
using INGLife.Event.Models.MarketingAgreeModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event
{
    [RoutePrefix("m")]
    public class MarketingAgreeController : Controller {
        private IMarketingAgreeService service;
        private IKMCService kmcService;
        private ICommonProvider common;
        private IUplusSmsApiService smsOpenApiService;

        private MapperConfiguration mapperConfig;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public MarketingAgreeController(IMarketingAgreeService service, IKMCService kmcService, ICommonProvider common, IUplusSmsApiService smsOpenApiService) {
            this.service = service;
            this.kmcService = kmcService;
            this.common = common;
            this.smsOpenApiService = smsOpenApiService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<MarketingAgreeEntry, MarketingAgreeViewModel>();
                cfg.CreateMap<MarketingAgreeModel, MarketingAgreeEntry>();
            });
        }

        [Route("")]
        // GET: MarketingAgree
        public ActionResult Index() {
            //var date = common.Now.ToString("yyyyMMddHHmmss");            

            //var urlCode = "003007";
            //var trUrl = "https://test.www.orange-event.kr/m/callback";
            //var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            var kmcModel = new RequestKMCModel { };

            // 실서버 적용시 urlCode,trUrl 실서버용으로 변경
            //if (kmcState == "release") {
            //    urlCode = "002003";
            //    trUrl = "https://www.orange-event.kr/m/callback";
            //}


            //kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);


            var model = new MarketingAgreeModels {
                MarketingAgreeModel = new MarketingAgreeModel { },
                RequestKMCModel = kmcModel
            };

            ViewBag.IsClose = true;
            return View(model);
        }

        /// <summary>
        /// 본인확인 서비스 요청 결과 수신
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("callback")]
        public ActionResult Callback(ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }

        [HttpPost]
        [Route("complete-marketing-agree-kmc", Name = "CompleteMarketingAgreeKMC")]
        public ActionResult CompleteMarketingAgreeKMC(ResponseKMCModel model) {
            try {

                throw new MarketingAgreeServiceException("400", "이벤트가 종료되었습니다.", null);

                //var result = kmcService.ResponseKMC(model);

                //var entry = new MarketingAgreeEntry {
                //    Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                //    Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                //    Mobile = result.Data.PhoneNo,
                //    BirthDay = string.IsNullOrEmpty(result.Data.M_BirthDay) ? result.Data.BirthDay : result.Data.M_BirthDay,
                //    Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                //    IpAddress = common.IpAddress,
                //    CreateDate = common.Now
                //};

                //var resultModel = service.CreateMarketingAgreeEntry(entry);
                //var data = mapperConfig.CreateMapper().Map<MarketingAgreeViewModel>(resultModel);

                //Session.Add("MarketingAgreeEntryId", resultModel.Id);

                //return Json(new {
                //    Result = result.Result,
                //    Message = result.Message,
                //    Data = data
                //});
            }
            catch ( KMCServiceException e ) {
                logger.Debug(">>>>>>>>>>> Response KMC Service");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                return Json(new {
                    Result = false,
                    Message = string.Format("본인인증 실패되었습니다. [ 본인인증을 다시 해주시길 바랍니다. ({0}) ]", e.Message)
                });
            } catch( MarketingAgreeServiceException e ) {
                return Json(new {
                    Result = false,
                    Message = e.Message
                });
            } catch( Exception e ) {
                logger.Error(e);

                return Json(new {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }

        }
        [HttpPost]
        [Route("create-entry", Name = "CreateMarketingAgreeEntry")]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateMarketingAgreeEntry(MarketingAgreeModels model) {
            try {

                throw new MarketingAgreeServiceException("400", "이벤트가 종료되었습니다.", null);

                //long? userId = Session["MarketingAgreeEntryId"] as long?;
                //if( !userId.HasValue ) {
                //    throw new MarketingAgreeServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                //}
                //var marketingAgreeModel = model.MarketingAgreeModel;

                //if( !ModelState.IsValid ) {
                //    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                //    if( errorProp != null ) {
                //        throw new MarketingAgreeServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                //    }
                //}

                //// 연락방식 체크
                //if( !model.MarketingAgreeModel.AllCheck ) {
                //    return Json(new {
                //        Result = false,
                //        IsRequiredAllCheck = false
                //    });
                //}


                //var user = service.GetMarketingAgreeEntryById(userId.Value);
                //// 기존 저장 개인정보와 다른값 체크
                //if( user.Name != marketingAgreeModel.Name || user.Mobile != marketingAgreeModel.Mobile || user.Gender != marketingAgreeModel.Gender || user.BirthDay != marketingAgreeModel.BirthDay ) {
                //    throw new MarketingAgreeServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                //}
                //var entry = mapperConfig.CreateMapper().Map<MarketingAgreeModel, MarketingAgreeEntry>(marketingAgreeModel, user);
                //var message = string.Format("안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프생명에 신청하신 '상품의 소개 등을 위한 개인(신용)정보 처리동의' (마케팅동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제 2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                ////entry.IsMessage = smsService.SendMMS("", entry.Mobile, "16885769", message, new List<string>());
                //entry.IsMessage = smsOpenApiService.SendMMS("", entry.Mobile, "0222009867", message,"L");
                //entry.UpdateDate = common.Now;

                //service.UpdateMarketingAgreeEntry(entry);

                //Session.Remove("MarketingAgreeEntryId");

                //return Json(new {
                //    Result = true,
                //});
            } catch( MarketingAgreeServiceException e ) {
                return Json(new {
                    Result = false,
                    IsRequiredAllCheck = true,
                    Message = e.Message
                });
            } catch( Exception e ) {
                logger.Error(e);
                return Json(new {
                    Result = false,
                    IsRequiredAllCheck = true,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}