using AutoMapper;
using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Domain.Services.Rebranding;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using INGLife.Event.Models.KMCModels;
using INGLife.Event.Models.ReNrandingModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event
{
    [RoutePrefix("")]
    public class ReBrandingController : Controller {
        private IRebrandingEventService service;
        private IKMCService kmcService;
        private ICommonProvider common;
        private IUplusSmsApiService smsService;

        private MapperConfiguration mapperConfig;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public ReBrandingController(IRebrandingEventService service,IKMCService kmcService,ICommonProvider common,IUplusSmsApiService smsService) {
            this.service = service;
            this.kmcService = kmcService;
            this.common = common;
            this.smsService = smsService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<RebrandingMarketingAgreeEntry, RebrandingViewModel>();
                cfg.CreateMap<RebrandingConsultingAgreeEntry, RebrandingViewModel>();

                cfg.CreateMap<RebrandingMarketingModel, RebrandingMarketingAgreeEntry>().ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? e.Email1 + "@"+e.Email2 : null));
                cfg.CreateMap<RebrandingConsultingModel, RebrandingConsultingAgreeEntry>();
            });
        }

        /// <summary>
        /// 리브랜딩 마케팅동의 페이지
        /// </summary>
        /// <returns></returns>
        [Route("ma")]
        public ActionResult Index() {
            var isMarketingEntryLimit  = service.IsRebrandingMarketingEntryLimit(2590);
            var now = common.Now;
            var isClose = now >= new DateTime(2018, 10, 22);
            var isOpen = now >= new DateTime(2018, 10, 15);

            var date = common.Now.ToString("yyyyMMddHHmmss");

            var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            var urlCode = "004004";
            var trMarketingUrl = "http://dev.www.orange-event.kr/ma/callback";
            
            var kmcMarketingModel = new RequestKMCModel { };

            // 실서버 적용시 urlCode,trUrl 실서버용으로 변경
            if (kmcState == "release") {
                urlCode = "002005";
                trMarketingUrl = "https://www.orange-event.kr/ma/callback";
            } else if (kmcState == "debug") {
                urlCode = "003010";
                trMarketingUrl = "http://test.www.orange-event.kr/ma/callback";
            }

            ViewBag.IsClose = true;

            if ( !isClose && !isMarketingEntryLimit && isOpen) {
                ViewBag.IsClose = false;
                kmcMarketingModel = kmcService.RequestKMC(date, urlCode, trMarketingUrl);
            }
            
            var model = new RebrandingMarketingModels {
                RebrandingMarketingModel = new RebrandingMarketingModel { },
                RequestMarketingKMCModel = kmcMarketingModel
            };

            return View(model);
        }
        /// <summary>
        /// 리브랜딩 상담동의 페이지
        /// </summary>
        /// <returns></returns>
        [Route("c")]
        public ActionResult Consulting() {

            var isConsultingEntryLimit = service.IsRebrandingConsultingEntryLimit(300);
            var now = common.Now;
            var isClose = now >= new DateTime(2018, 10, 22);
            var isOpen = now >= new DateTime(2018, 10, 15);

            var date = common.Now.ToString("yyyyMMddHHmmss");

            var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            var urlCode = "004005";
            var trConsultingUrl = "http://dev.www.orange-event.kr/c/callback";

            var kmcConsultingModel = new RequestKMCModel { };

            // 실서버 적용시 urlCode,trUrl 실서버용으로 변경
            if (kmcState == "release") {
                urlCode = "002006";
                trConsultingUrl = "https://www.orange-event.kr/c/callback";
            } else if (kmcState == "debug") {
                urlCode = "003011";
                trConsultingUrl = "http://test.www.orange-event.kr/c/callback";
            }

            ViewBag.IsClose = true;

            if (!isClose && !isConsultingEntryLimit && isOpen) {
                kmcConsultingModel = kmcService.RequestKMC(date, urlCode, trConsultingUrl);
                ViewBag.IsClose = false;
            }

            var model = new RebrandingConsultingModels {
                RebrandingConsultingModel = new RebrandingConsultingModel { },
                RequestConsultingKMCModel = kmcConsultingModel
            };

            return View(model);
        }

        /// <summary>
        /// 본인확인 서비스 요청 결과 수신(마케팅동의)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ma/callback")]
        public ActionResult MarketingCallback(ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }
        /// <summary>
        /// 본인확인 서비스 요청 결과 수신(상담동의)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("c/callback")]
        public ActionResult ConsultingCallback(ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }
        
        [HttpPost]
        [Route("new-branding/complete-kmc", Name = "CompleteRebrandingKMC")]
        public ActionResult CompleteRebrandingKMC(RebrandingResponseKMCModel model) {
            try {
                if (common.Now >= new DateTime(2018, 10, 22)) {
                    throw new RebrandingEventServiceException("400", "이벤트가 종료되었습니다.", null);
                }
                var result = kmcService.ResponseKMC(model.ResponseKMCModel);
                var data = new RebrandingViewModel{ };
                if (model.RebrandingEventType == RebrandingEventType.MarketingAgree) {
                    if (service.IsRebrandingMarketingEntryLimit(2590)) {
                        throw new RebrandingEventServiceException("400", "선착순이 마감되었습니다.", null);
                    }
                    var entry = new RebrandingMarketingAgreeEntry {
                        Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                        Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                        Mobile = result.Data.PhoneNo,
                        BirthDay = string.IsNullOrEmpty(result.Data.M_BirthDay) ? result.Data.BirthDay : result.Data.M_BirthDay,
                        Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                        IpAddress = common.IpAddress,
                        CreateDate = common.Now,
                    };
                    var resultModel = service.CreateRebrandingMarketingAgreeEntry(entry);
                    data = mapperConfig.CreateMapper().Map<RebrandingViewModel>(resultModel);

                    Session.Add("RebrandingMarketingAgreeEntryId", resultModel.Id);
                } else if (model.RebrandingEventType == RebrandingEventType.ConsultingAgree) {
                    if (service.IsRebrandingConsultingEntryLimit(300)) {
                        throw new RebrandingEventServiceException("400", "선착순이 마감되었습니다.", null);
                    }
                    var entry = new RebrandingConsultingAgreeEntry {
                        Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                        Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                        Mobile = result.Data.PhoneNo,
                        BirthDay = string.IsNullOrEmpty(result.Data.M_BirthDay) ? result.Data.BirthDay : result.Data.M_BirthDay,
                        Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                        IpAddress = common.IpAddress,
                        CreateDate = common.Now,
                    };
                    var resultModel = service.CreateRebrandingConsultingAgreeEntry(entry);
                    data = mapperConfig.CreateMapper().Map<RebrandingViewModel>(resultModel);

                    Session.Add("RebrandingConsultingAgreeEntryId", resultModel.Id);
                }

                return Json(new {
                    Result = result.Result,
                    Message = result.Message,
                    Data = data
                });
            } catch (KMCServiceException e) {
                logger.Debug(">>>>>>>>>>> Response KMC Service");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);

                return Json(new {
                    Result = false,
                    Message = string.Format("본인인증 실패되었습니다. [ 본인인증을 다시 해주시길 바랍니다. ({0}) ]", e.Message)
                });
            } catch (RebrandingEventServiceException e) {
                return Json(new {
                    Result = false,
                    Message = e.Message
                });
            } catch (Exception e) {
                logger.Error(e);

                return Json(new {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }

        [HttpPost]
        [Route("new-branding/create-markeitng-entry", Name = "CreateRebrandingMarketingAgreeEntry")]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateRebrandingMarketingAgreeEntry(RebrandingMarketingModels model) {
            try {
                if (service.IsRebrandingMarketingEntryLimit(2590)) {
                    throw new RebrandingEventServiceException("400", "선착순이 마감되었습니다.", null);
                }
                long? userId = Session["RebrandingMarketingAgreeEntryId"] as long?;
                if (!userId.HasValue) {
                    throw new RebrandingEventServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }
                var rebrandingMarketingModel = model.RebrandingMarketingModel;

                if (!ModelState.IsValid) {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null) {
                        throw new RebrandingEventServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }
                var user = service.GetRebrandingMarketingAgreeEntryById(userId.Value);
                // 기존 저장 개인정보와 다른값 체크
                if (user.Name != rebrandingMarketingModel.Name || user.Mobile != rebrandingMarketingModel.Mobile || user.Gender != rebrandingMarketingModel.Gender || user.BirthDay != rebrandingMarketingModel.BirthDay) {
                    throw new RebrandingEventServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                }
                var entry = mapperConfig.CreateMapper().Map<RebrandingMarketingModel, RebrandingMarketingAgreeEntry>(rebrandingMarketingModel, user);
                var message = string.Format("[오렌지라이프] 마케팅동의 처리안내\r\n\r\n안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프에 신청하신\r\n'상품의 소개 등을 위한 개인(신용)정보 처리 동의'\r\n(마케팅동의)가 정상반영되어 안내드립니다.\r\n오렌지라이프 대표번호 : 1588-5005", common.Now.Year, common.Now.Month, common.Now.Day);
                entry.IsMessage = smsService.SendMMS("", entry.Mobile, "0222009867", message,"L");
                entry.UpdateDate = common.Now;

                service.UpdateRebrandingMarketingAgreeEntry(entry);

                Session.Remove("RebrandingMarketingAgreeEntryId");

                return Json(new {
                    Result = true
                });
            } catch (RebrandingEventServiceException e) {
                return Json(new {
                    Result = false,
                    Message = e.Message
                });
            } catch (Exception e) {
                logger.Error(e);
                return Json(new {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }

        [HttpPost]
        [Route("new-branding/create-consulting-entry", Name = "CreateRebrandingConsultingAgreeEntry")]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateRebrandingConsultingAgreeEntry(RebrandingConsultingModels model) {
            try {
                if (service.IsRebrandingConsultingEntryLimit(300)) {
                    throw new RebrandingEventServiceException("400", "선착순이 마감되었습니다.", null);
                }
                long? userId = Session["RebrandingConsultingAgreeEntryId"] as long?;
                if (!userId.HasValue) {
                    throw new RebrandingEventServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }
                var rebrandingConsultingModel = model.RebrandingConsultingModel;

                if (!ModelState.IsValid) {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null) {
                        throw new RebrandingEventServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }
                var user = service.GetRebrandingConsultingAgreeEntryById(userId.Value);
                // 기존 저장 개인정보와 다른값 체크
                if (user.Name != rebrandingConsultingModel.Name || user.Mobile != rebrandingConsultingModel.Mobile || user.Gender != rebrandingConsultingModel.Gender || user.BirthDay != rebrandingConsultingModel.BirthDay) {
                    throw new RebrandingEventServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                }
                var entry = mapperConfig.CreateMapper().Map<RebrandingConsultingModel, RebrandingConsultingAgreeEntry>(rebrandingConsultingModel, user);
                //var message = string.Format("안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 오렌지라이프생명보험(주)에 '상품의 소개 등을 위한 개인(신용)정보 처리동의'(상담동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제 2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                //entry.IsMessage = smsService.SendMMS("", entry.Mobile, "0222009867", message,"L");
                entry.IsMessage = false;
                entry.UpdateDate = common.Now;

                service.UpdateRebrandingConsultingAgreeEntry(entry);

                Session.Remove("RebrandingConsultingAgreeEntryId");

                return Json(new {
                    Result = true
                });
            } catch (RebrandingEventServiceException e) {
                return Json(new {
                    Result = false,
                    Message = e.Message
                });
            } catch (Exception e) {
                logger.Error(e);
                return Json(new {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}