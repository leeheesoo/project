using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Services.FinancialConsultantSharing;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Message.Services;
using INGLife.Event.Models;
using INGLife.Event.Models.FinancialConsultantSharingModels.New;
using INGLife.Event.Models.FinancialConsultantSharingModels.Origin;
using INGLife.Event.Models.KMCModels;
using NLog;
using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event
{
    [RoutePrefix("")]
	public class FcEventController : Controller
	{
        private IFinancialConsultantSharingService financialConsultantSharingService;
        private IUplusSmsApiService smsService;
        private ICommonProvider common;
        private IKMCService kmcService;

        private Logger logger = LogManager.GetCurrentClassLogger();
        private MapperConfiguration mapperConfig;

        string kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;

        public FcEventController(IFinancialConsultantSharingService financialConsultantSharingService, IUplusSmsApiService smsService, ICommonProvider common, IKMCService kmcService)
        {
            this.financialConsultantSharingService = financialConsultantSharingService;
            this.smsService = smsService;
            this.common = common;
            this.kmcService = kmcService;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConsultantOriginalCustomerEntry, FinancialConsultantSharingOriginCreateModel>();
                cfg.CreateMap<FinancialConsultantSharingOriginCreateModel, FinancialConsultantOriginalCustomerEntry>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? e.Email1 + "@" + e.Email2 : null));

                cfg.CreateMap<FinancialConsultantNewCustomerEntry, FinancialConsultantSharingNewCreateModel>();
                cfg.CreateMap<FinancialConsultantSharingNewCreateModel, FinancialConsultantNewCustomerEntry>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? e.Email1 + "@" + e.Email2 : null));
            });
        }

        /// ############################################################ 기존고객 ############################################################
        [Route("fc-sharing")]
		// GET: fc
		public ActionResult OriginCustomerIndex()
		{
            if (IsEventClose())
            {
                ViewBag.IsClose = true;
                ViewBag.Message = "이벤트가 종료되었습니다.";
                return View(new FinancialConsultantSharingOriginModels
                {
                    RequestKMCModel = new RequestKMCModel()
                });
            }

            var urlCode = "";
            var trUrl = "";
            var domain = "";
            switch (kmcState)
            {
                case "local":
                default:
                    urlCode = "004011";
                    domain = "http://dev.www.orange-event.kr";
                    trUrl = "http://dev.www.orange-event.kr/fc-sharing/callback";
                    break;
                case "debug":
                    urlCode = "003016";
                    domain = "http://test.www.orange-event.kr";
                    //본인인증 모듈 로드시에 익스플로러 팝업이 안열리는 현상이 있음. http 프로토콜을 사용할것.
                    trUrl = "http://test.www.orange-event.kr/fc-sharing/callback";
                    break;
                case "release":
                    urlCode = "002009";
                    domain = "https://www.orange-event.kr";
                    trUrl = "https://www.orange-event.kr/fc-sharing/callback";
                    break;
            }
            var date = common.Now.ToString("yyyyMMddHHmmss");
            var kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);
            //공통 kmc callback 을 사용해서 구분하기 위해 해당변수 사용
            kmcModel.PlusInfo = "origin";

            var model = new FinancialConsultantSharingOriginModels
            {
                RequestKMCModel = kmcModel
            };

            ViewBag.url = domain + "/fc-sharing";
            ViewBag.image = domain + "/Content/images/FcEvent/sns/kakao.jpg";
            ViewBag.IsClose = false;
            if (!financialConsultantSharingService.limitNewCustomer())
            {
                ViewBag.Message = "준비된 경품이 모두 소진되었습니다.";
                ViewBag.IsClose = true;
            }
            //페이지 새로 로드시에 카카오톡 공유 불가능하도록.
            Session.Remove("kakaoFlag");
            return View(model);
		}        

        [HttpPost]
        [Route("fc-sharing/checkFcCode", Name = "CheckFcCode")]        
        public JsonResult CheckFcCode(FcCodeModel fcCodeModel)
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }
            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };           
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null)
                    {
                        throw new EventServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }
                financialConsultantSharingService.isExistFC(fcCodeModel.FcCode);
                Session.Add("FcCode", fcCodeModel.FcCode);
                result = new JsonResultModel
                {
                    Result = true,
                    Message = "FC코드 인증에 성공하였습니다.",
                    Data = "success"
                };
            }
            catch (EventServiceException e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> CHECK FC CODE EVENT Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            return Json(result);
        }        

        [HttpPost]
        [Route("fc-sharing/create-entry", Name = "CreateFcOriginEntry")]
        [ValidateAntiForgeryToken()]
        public JsonResult CreateFcOriginEntry(FinancialConsultantSharingOriginModels model)
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }

            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            
            try
            {
                //신규회원 2000명 제한 추가
                if (!financialConsultantSharingService.limitNewCustomer())
                {
                    throw new EventServiceException("400", "준비된 경품이 모두 소진되었습니다.", null);
                }

                var entry = mapperConfig.CreateMapper().Map<FinancialConsultantOriginalCustomerEntry>(model.FinancialConsultantSharingOriginCreateModel);

                //신규 가입여부 확인                
                if (financialConsultantSharingService.existNewCustomer(entry.Mobile) != null)
                {
                    throw new EventServiceException("400", "신규가입고객은 참여를 하실 수 없습니다.", null);
                }

                

                //중복 참여 확인 - true                 
                if (!financialConsultantSharingService.depulicateOriginCustomer(entry.Mobile))
                {
                    throw new EventServiceException("400", "이미 참여하셨습니다.", entry.Mobile);
                };

                //fc 코드 다시 확인
                var FcCode = Session["FcCode"] as string;
                var FcFlag = string.IsNullOrEmpty(FcCode);
                if (!FcFlag)
                {
                    if(FcCode != model.FinancialConsultantSharingOriginCreateModel.FcCode)
                    {
                        throw new EventServiceException("400", "FC코드를 올바르게 입력해주십시오.", null);
                    }
                }
                else
                {
                    throw new EventServiceException("400", "FC코드를 인증을 받아주세요.", null);
                }
                
                // 본인인증 결과 확인
                var authSession = Session["FcOriginCustomerInfo"] as string;
                var authOriginEntryInfo = string.IsNullOrEmpty(authSession);
                if (authOriginEntryInfo || !authSession.Contains("^"))
                {
                    throw new EventServiceException("400", "본인 인증을 다시 받아주세요.[1]", null);
                }
                else
                {
                    string[] autoInfo = authSession.Split('^');
                    if (autoInfo[0] != model.FinancialConsultantSharingOriginCreateModel.Name || 
                        autoInfo[1] != model.FinancialConsultantSharingOriginCreateModel.Mobile || 
                        autoInfo[3] != model.FinancialConsultantSharingOriginCreateModel.BirthDay)
                    {
                        throw new EventServiceException("400", "본인 인증을 다시 받아주세요.[2]", null);
                    }
                }

                //랜덤 code 생성
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var list = Enumerable.Repeat(0, 8).Select(x => chars[random.Next(chars.Length)]);
                var randomCode = string.Join("", list);

                entry.CustomerRandomNum = randomCode;
                entry.Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC;
                entry.IpAddress = common.IpAddress;
                entry.CreateDate = common.Now;

                //데이터 저장                
                entry = financialConsultantSharingService.saveOriginEntry(entry);

                //문자발송
                var message = string.Format("안녕하세요, 고객님 {0}년 {1}월 {2}일에 \r\n오렌지라이프생명보험㈜ 에\r\n‘상품의 소개 등을 위한 개인(신용)정보 처리 동의‘(마케팅 동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제\r\n2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                if (kmcState.Equals("local"))
                {
                    // 로컬, 테스트서버 제외
                    entry.IsMessage = true;
                }
                else
                {
                    //문자결과 업데이트
                    if (smsService.SendMMS("", entry.Mobile, "0222008606", message, "L"))
                    {                        
                        entry.IsMessage = true;
                    }
                    else
                    {                        
                        entry.IsMessage = false;                        
                    };
                }

                //메세지 flag update                
                financialConsultantSharingService.updateOriginEntry(entry);                

                //본인인증 세션 지우기
                Session.Remove("FcOriginCustomerInfo");
                Session.Remove("FcCode");

                //카카오톡 공유를 위한 flag 생성
                Session.Add("kakaoFlag", randomCode+"^"+ entry.FcCode);

                result.Result = true;
                result.Message = "완료되었습니다.";

            }
            catch (EventServiceException e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> FC SHARING EVENT ENTRY SAVE (call FinancialConsultantOriginalCustomerEntry) Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            return Json(result);
        }

        /// <summary>
        /// 기존 참여여부 확인 절차
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("fc-sharing/customer", Name = "CheckOriginalCustomer")]        
        public JsonResult CheckOriginalCustomer(PhoneModel model)
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }

            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            try
            {

                string url = financialConsultantSharingService.getUrlToOriginCustomerByPhone(model.phone);

                result = new JsonResultModel
                {
                    Result = true,
                    Message = "FC코드 인증에 성공하였습니다.",
                    Data =url
                };
            }
            catch (EventServiceException e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> FC SHARING EVENT KAKAOTALK Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            return Json(result);
        }
        /// <summary>
        /// 카카오톡 공유 클릭시 세션 체크
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("fc-sharing/kakao-share", Name = "KakaoShare")]
        public ActionResult KakaoShare()
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }

            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };
            try
            {
                var kakaoStr = Session["kakaoFlag"] as string;
                var eventParticipateFlag = string.IsNullOrEmpty(kakaoStr);
                if (eventParticipateFlag)
                {
                    throw new EventServiceException("400", "이벤트에 먼저 참여해주세요!", null);
                }

                var kakaoFlagArr = kakaoStr.Split('^');
                var fcKaKaoModel = new FcKakaoShareModel
                {
                    FcCode = kakaoFlagArr[1],
                    RandomNum = kakaoFlagArr[0]
                };
                
                financialConsultantSharingService.isExistFC(fcKaKaoModel.FcCode);

                result = new JsonResultModel
                {
                    Result = true,
                    Data = "fc-sharing-new?rid=" + kakaoFlagArr[0] + "&fc=" + kakaoFlagArr[1]
                };
            }
            catch (EventServiceException e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> FC SHARING EVENT KAKAOTALK Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }

            return Json(result);
        }




        /// ############################################################ 신규고객 ############################################################
        [Route("fc-sharing-new")]
		// GET: fc
		public ActionResult NewCustomerIndex(FCNewAuthModel fCNewAuthModel)
		{
            if (IsEventClose())
            {
                ViewBag.IsClose = true;
                ViewBag.Message = "이벤트가 종료되었습니다.";
                return View(new FinancialConsultantSharingNewModels
                {
                    RequestKMCModel = new RequestKMCModel()
                });
            }

            var urlCode = "";
            var trUrl = "";
            var domain = "";

            switch (kmcState)
            {
                case "local":
                default:
                    urlCode = "004012";
                    domain = "http://test.www.orange-event.kr";
                    trUrl = "http://dev.www.orange-event.kr/fc-sharing-new/callback";
                    break;
                case "debug":
                    urlCode = "003018";
                    domain = "http://test.www.orange-event.kr";
                    //본인인증 모듈 로드시에 익스플로러 안열리는 현상이 있음.
                    trUrl = "http://test.www.orange-event.kr/fc-sharing-new/callback";
                    break;
                //TODO: 실서버 적용시 urlCode,trUrl 실서버용으로 변경
                case "release":
                    urlCode = "002010";
                    domain = "https://www.orange-event.kr";
                    trUrl = "https://www.orange-event.kr/fc-sharing-new/callback";
                    break;
            }

            ViewBag.url = domain + "/fc-sharing-new";
            ViewBag.image = domain + "/Content/images/FcEvent/sns/kakao.jpg";

            var model = new FinancialConsultantSharingNewModels();

            //존재하는 기존고객인지 확인
            var originCustomer = financialConsultantSharingService.existOriginCustomer(new FinancialConsultantOriginalCustomerEntry()
            {
                FcCode = fCNewAuthModel.fc,
                CustomerRandomNum = fCNewAuthModel.rid
            });

            //기존고객이 존재하면
            if (originCustomer != null) {                

                var date = common.Now.ToString("yyyyMMddHHmmss");
                var kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);
                kmcModel.PlusInfo = "new";
                model = new FinancialConsultantSharingNewModels
                {
                    RequestKMCModel = kmcModel
                };

                ViewBag.IsClose = false;
                ViewBag.FCCode = fCNewAuthModel.fc;
                ViewBag.RId = maskingName(originCustomer.Name);
                if (!financialConsultantSharingService.limitNewCustomer())
                {
                    ViewBag.IsClose = true;
                    ViewBag.Message = "준비된 경품이 모두 소진되었습니다.";
                }
                Session.Add("AuthInfo", fCNewAuthModel.rid + "^" + originCustomer.Name);
                return View(model);
            }
            else
            {
                ViewBag.IsClose = true;
                ViewBag.Message = "카카오톡 공유 메세지로 접속해주세요.";
                return View();
            }            
        }

        [HttpPost]
        [Route("fc-sharing-new/create-entry", Name = "CreateFcNewEntry")]
        [ValidateAntiForgeryToken()]
        public JsonResult CreateFcNewEntry(FinancialConsultantSharingNewModels model)
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }

            var result = new JsonResultModel { Result = false, Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다." };

            try
            {

                //신규회원 2000명 제한 추가
                if (!financialConsultantSharingService.limitNewCustomer())
                {
                    throw new EventServiceException("400", "준비된 경품이 모두 소진되었습니다.", null);
                }

                var entry = mapperConfig.CreateMapper().Map<FinancialConsultantNewCustomerEntry>(model.FinancialConsultantSharingNewCreateModel);

                //기존 가입여부 확인                
                if (financialConsultantSharingService.existOriginCustomer(entry) != null)
                {
                    throw new EventServiceException("400", "기존 가입고객은 신규가입을 하실 수 없습니다.", null);
                }

                //중복 참여 확인 - true
                if (!financialConsultantSharingService.depulicateNewCustomer(entry.Mobile))
                {
                    throw new EventServiceException("400", "이미 참여하셨습니다.", entry.Mobile);
                };

                //fc 코드 다시 확인
                financialConsultantSharingService.isExistFC(model.FinancialConsultantSharingNewCreateModel.FcCode);


                //본인인증 결과 확인
                var authSession = Session["FcNewCustomerInfo"] as string;
                var authOriginEntryInfo = string.IsNullOrEmpty(authSession);
                if (authOriginEntryInfo || !authSession.Contains("^"))
                {
                    throw new EventServiceException("400", "본인 인증을 다시 받아주세요.[1]", null);
                }
                else
                {
                    string[] autoInfo = authSession.Split('^');
                    if (autoInfo[0] != model.FinancialConsultantSharingNewCreateModel.Name ||
                        autoInfo[1] != model.FinancialConsultantSharingNewCreateModel.Mobile ||
                        autoInfo[3] != model.FinancialConsultantSharingNewCreateModel.BirthDay)
                    {
                        throw new EventServiceException("400", "본인 인증을 다시 받아주세요.[2]", null);
                    }
                }

                
                entry.Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC;
                entry.IpAddress = common.IpAddress;
                entry.CreateDate = common.Now;
                var authInfo = Session["AuthInfo"] as string;
                var randFlag = string.IsNullOrEmpty(authInfo);
                if (!randFlag || authInfo.Contains("^"))
                {                    
                    string[] infos = authInfo.Split('^');
                    entry.OriginCustomerRandomNum = infos[0];
                    entry.ProposerName = infos[1];
                }
                else
                {
                    throw new EventServiceException("400", "공유 URL 인증 시간이 만료되었습니다.\r\n다시 공유받은 링크로 접속바랍니다.", null);
                }


                //데이터 저장                
                entry = financialConsultantSharingService.saveNewEntry(entry);

                //문자발송
                var message = string.Format("안녕하세요, 고객님 {0}년 {1}월 {2}일에 \r\n오렌지라이프생명보험㈜ 에\r\n‘상품의 소개 등을 위한 개인(신용)정보 처리 동의‘(마케팅 동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제\r\n2016-0444호(2016.10.13)", common.Now.Year, common.Now.Month, common.Now.Day);
                if (kmcState.Equals("local"))
                {
                    // 로컬, 테스트서버 제외
                    entry.IsMessage = true;
                }
                else
                {
                    if (smsService.SendMMS("", entry.Mobile, "0222008606", message, "L"))
                    {
                        //문자완료 플레그 업데이트
                        entry.IsMessage = true;
                    }
                    else
                    {
                        //실패시 플레그 실패 업데이트 
                        entry.IsMessage = false;
                        //프로세스는 계속 진행
                    };
                }

                //메세지 flag update                
                financialConsultantSharingService.updateNewEntry(entry);

                //본인인증 세션 지우기
                Session.Remove("FcNewCustomerInfo");
                Session.Remove("AuthInfo");

                result.Result = true;
                result.Message = "이벤트 응모가 완료되었습니다.";

            }
            catch (EventServiceException e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                logger.Info(">>>>>>>> FC SHARING EVENT ENTRY SAVE (call FinancialConsultantOriginalCustomerEntry) Exception");
                logger.Debug("/////////// message:{0}, data:{1}", e.Message, e.Data);
            }
            return Json(result);
        }

        /// ############################################################## COMMON ##############################################################
        
        /// <summary>
        /// 본인확인서비스 callback uri 동일하게 처리해야함.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("fc-sharing/callback")]
        [Route("fc-sharing-new/callback")]
        public ActionResult Callback(ResponseKMCModel model)
        {
            // 이미 참여했으면 참여한 대상자 처리            
            if (model == null)
            {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }

        /// <summary>
        /// 본인확인 완료시 kmc response 받아서 가공.
        /// origin, new 둘다 사용
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("fc-sharing/complete-fc-sharing-kmc", Name = "CompleteFcSharingKMC")]
        public ActionResult CompleteFcSharingKMC(ResponseKMCModel model)
        {
            if (IsEventClose())
            {
                throw new EventServiceException("400", "이벤트가 종료되었습니다.", null);
            }
            try
            {
                var result = kmcService.ResponseKMC(model);
                var entry = new FinancialConsultantSharingKMCResultModel
                {
                    Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                    Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                    Mobile = string.IsNullOrEmpty(result.Data.PhoneNo) ? result.Data.PhoneNo : result.Data.PhoneNo,
                    Birth = string.IsNullOrEmpty(result.Data.BirthDay) ? result.Data.BirthDay : result.Data.BirthDay,
                };

                //요청을 어떤 uri로 주었냐에 따라 구분
                if (result.Data.PlusInfo == "origin")
                {
                    //중복 참여 확인 - true                 
                    if (!financialConsultantSharingService.depulicateOriginCustomer(entry.Mobile))
                    {
                        throw new EventServiceException("400", "이미 참여하셨습니다.", entry.Mobile);
                    };

                    //신규 가입여부 확인                
                    if (financialConsultantSharingService.existNewCustomer(entry.Mobile) != null)
                    {
                        throw new EventServiceException("400", "신규가입고객은 참여를 하실 수 없습니다.", null);
                    }

                    Session.Add("FcOriginCustomerInfo", entry.Name + "^" + entry.Mobile + "^" + entry.Gender + "^" + entry.Birth);
                }
                else if (result.Data.PlusInfo == "new")
                {
                    //중복 참여 확인 - true
                    if (!financialConsultantSharingService.depulicateNewCustomer(entry.Mobile))
                    {
                        throw new EventServiceException("400", "이미 참여하셨습니다.", entry.Mobile);
                    };

                    //기존 가입여부 확인
                    if (!financialConsultantSharingService.depulicateOriginCustomer(entry.Mobile))
                    {
                        throw new EventServiceException("400", "기존 가입고객은 신규가입을 하실 수 없습니다.", entry.Mobile);
                    }

                    Session.Add("FcNewCustomerInfo", entry.Name + "^" + entry.Mobile + "^" + entry.Gender + "^" + entry.Birth);
                }

                return Json(new
                {
                    Result = "200",
                    Message = "본인인증이 완료되었습니다.",
                    Data = entry
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
                logger.Error(e);
                return Json(new
                {
                    Result = false,
                    Message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 이름 마스킹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string maskingName(string name)
        {
            string mName = "";

            var isKorean = Regex.IsMatch(name, "^[ㄱ-ㅎ가-힣]*$");
            if (name.Length == 0 || name == null)
            {
                mName = "";
            }
            else if (name.Length == 1)
            {
                mName = "*";
            }
            else
            {
                char[] cName = name.ToCharArray();
                if (isKorean)
                {
                    for (int index = 0; index < cName.Length; index++)
                    {
                        if (index == 0 || index == cName.Length - 1)
                        {
                            continue;
                        }
                        cName[index] = '*';
                    }
                    mName = string.Join("", cName);
                }
                else
                {
                    for (int index = 0; index < cName.Length; index++)
                    {
                        if (index < 3 || index > cName.Length - 4)
                        {
                            continue;
                        }
                        cName[index] = '*';
                    }
                    mName = string.Join("", cName);
                }
            }
            return mName;
        }
        private bool IsEventClose()
        {
            return common.Now >= new DateTime(2018, 12, 13);
        }
    }
}