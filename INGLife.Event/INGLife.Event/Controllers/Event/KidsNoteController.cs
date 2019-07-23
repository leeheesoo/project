using AutoMapper;
using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Services.KidsNote;
using INGLife.Event.Infrastructures.Interfaces;
using INGLife.Event.Infrastructures.KMCServices;
using INGLife.Event.Models.KidsNoteModels;
using INGLife.Event.Models.KMCModels;
using NLog;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Event {
    [RoutePrefix("kids-note")]
    public class KidsNoteController : Controller
    {
        private IKidsNoteService service;
        private IKMCService kmcService;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private ICommonProvider common;
        private MapperConfiguration mapperConfig;

        public KidsNoteController(IKidsNoteService service,IKMCService kmcService,ICommonProvider common) {
            this.service = service;
            this.kmcService = kmcService;
            this.common = common;

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<KidsNoteEntry, KidsNoteViewModel>();
                cfg.CreateMap<KidsNoteModel, KidsNoteEntry>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.EmailCheck ? e.Email1 + "@" + e.Email2 : null))
                .ForMember(dest => dest.ChildAge, opt => opt.MapFrom(e => e.ChildAge.Value));
            });
        }

        [Route("")]
        // GET: KidsNote
        public ActionResult Index()
        {
            var date = common.Now.ToString("yyyyMMddHHmmss");
           
            var urlCode = "003005";
            var trUrl = "https://test.www.orange-event.kr/kids-note/callback";
            var kmcState = ConfigurationManager.AppSettings["kmc.state"] as string;
            var kmcModel = new RequestKMCModel { };

            // 실서버 적용시 urlCode,trUrl 실서버용으로 변경
            if (kmcState == "release") {
                urlCode = "002001";
                trUrl = "https://www.orange-event.kr/kids-note/callback";
            }


            if (!IsEventClose())
            {
                kmcModel = kmcService.RequestKMC(date, urlCode, trUrl);

            }

            var model = new KidsNoteModels {
                KidsNoteModel = new KidsNoteModel { },
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
        public ActionResult Callback(ResponseKMCModel model) {
            if (model == null) {
                ViewBag.Message = "결과값 비정상";
            }
            return View(model);
        }

        [HttpPost]
        [Route("complete-kids-note-kmc", Name = "CompleteKidsNoteKMC")]
        public ActionResult CompleteKidsNoteKMC(ResponseKMCModel model) {
            try {
                if (IsEventClose()) {
                    throw new KidsNoteServiceException("400", "이벤트가 종료되었습니다.", null);
                }
                var result= kmcService.ResponseKMC(model);

                var entry = new KidsNoteEntry {
                    Name = string.IsNullOrEmpty(result.Data.M_Name) ? result.Data.Name : result.Data.M_Name,
                    Gender = string.IsNullOrEmpty(result.Data.M_Gender) ? result.Data.Gender : result.Data.M_Gender,
                    Mobile = result.Data.PhoneNo,
                    BirthDay = string.IsNullOrEmpty(result.Data.M_BirthDay) ? result.Data.BirthDay : result.Data.M_BirthDay,
                    Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                    IpAddress = common.IpAddress,
                    CreateDate = common.Now,
                    EventType = KidsNoteEventType.ThirdEvent    //4차이벤트 시작시 수정해야함
                };
                var resultModel = service.CreateKidsNoteEntry(entry);
                var data = mapperConfig.CreateMapper().Map<KidsNoteViewModel>(resultModel);

                Session.Add("KidsNoteEntryId", resultModel.Id);

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
            } catch (KidsNoteServiceException e) {
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
        [Route("create-entry", Name = "CreateKidsNoteEntry")]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateKidsNoteEntry(KidsNoteModels model) {
            try {
                if (IsEventClose()) {
                    throw new KidsNoteServiceException("400", "이벤트가 종료되었습니다.", null);
                }

                long? userId = Session["KidsNoteEntryId"] as long?;
                if (!userId.HasValue) {
                    throw new KidsNoteServiceException("400", "휴대폰 인증을 다시 받아주세요.", null);
                }
                var kidsNoteModel = model.KidsNoteModel;
                //2018 03 05 입력항목 간소화 처리:::LHS
                kidsNoteModel.ChildAge = 0;
                kidsNoteModel.ChildName = "";
                
                if (!ModelState.IsValid) {
                    var errorProp = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault();
                    if (errorProp != null) {
                        throw new KidsNoteServiceException("400", errorProp.Errors[0].ErrorMessage, null);
                    }
                }
                var user = service.GetKidsNoteEntryById(userId.Value);
                // 기존 저장 개인정보와 다른값 체크
                if (user.Name != kidsNoteModel.Name || user.Mobile != kidsNoteModel.Mobile || user.Gender != kidsNoteModel.Gender || user.BirthDay != kidsNoteModel.BirthDay) {
                    throw new KidsNoteServiceException("400", "휴대폰 인증을 받아 다시 시도해주세요.", null);
                }
                var entry = mapperConfig.CreateMapper().Map<KidsNoteModel, KidsNoteEntry>(kidsNoteModel, user);
                //var message = string.Format("안녕하세요, 고객님\r\n{0}년 {1}월 {2}일에 ING생명에 신청하신 '상품의 소개 등을 위한 개인(신용)정보 처리동의' (마케팅동의)가 정상 반영되어 안내 드립니다.\r\n준법감시인심의필 제 2016-0444호(2016.10.13)",common.Now.Year,common.Now.Month,common.Now.Day);
                //entry.IsMessage = smsService.SendMMS("", entry.Mobile, "16885769", message, new List<string>());
                entry.UpdateDate = common.Now;

                service.UpdateKidsNoteEntry(entry);

                Session.Remove("RechinComicsEntryId");

                return Json(new {
                    Result = true
                });
            } catch (KidsNoteServiceException e) {
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

        // 이벤트 선착순
        private bool IsEntryCheck() {
            return service.IsEntry(KidsNoteEventType.FirstEvent, 600);
        }
        // 이벤트 종료일
        private bool IsEventClose() {
            return common.Now >= new DateTime(2018,3,14);
        }
    }
}