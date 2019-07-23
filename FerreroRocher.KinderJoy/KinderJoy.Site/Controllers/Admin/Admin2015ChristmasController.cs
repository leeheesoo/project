using KinderJoy.Domain.Abstract.Christmas2015;
using KinderJoy.Domain.Repository.Christmas2015;
using KinderJoy.Site.Infrastructure;
using KinderJoy.Site.Models.Admin.Christmas2015;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Controllers.Admin {
    [Authorize]
    [RoutePrefix("manager/2015-christmas")]
    public class Admin2015ChristmasController : Controller {
        private IChristmas2015Repository repository;
        private ICommonProvider common;

        public Admin2015ChristmasController(IChristmas2015Repository repository, ICommonProvider common) {
            this.repository = repository;
            this.common = common;
        }
        public string GetPrizeTypeName(Christmas2015EnumConst.Christmas2015WinPrize prizeType) {
            var field = prizeType.GetType().GetField(prizeType.ToString());
            var attrib = field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;
            return attrib == null ? "" : attrib.GetName();
        }
        [Route("")]
        public ActionResult Index() {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem {
                Text = "----",
                Value = ""
            });
            list.Add(new SelectListItem{
                Text = GetPrizeTypeName(Christmas2015EnumConst.Christmas2015WinPrize.Loser),
                Value = Christmas2015EnumConst.Christmas2015WinPrize.Loser.ToString("D")
            });
            list.Add(new SelectListItem {
                Text = GetPrizeTypeName(Christmas2015EnumConst.Christmas2015WinPrize.Kinder),
                Value = Christmas2015EnumConst.Christmas2015WinPrize.Kinder.ToString("D")
            });
            list.Add(new SelectListItem {
                Text = GetPrizeTypeName(Christmas2015EnumConst.Christmas2015WinPrize.Cloak),
                Value = Christmas2015EnumConst.Christmas2015WinPrize.Cloak.ToString("D")
            });
            ViewBag.PrizeTypeList = list;
            return View();
        }
        
        [Route("win-list")]
        public ActionResult WinList() {
            return View();
        }

        [Route("win-by-ip")]
        public ActionResult WinListByIPAddress() {
            return View();
        }

        [Route("make-tree")]
        public ActionResult MakeTree() {
            return View();
        }

        [Route("make-tree-sns")]
        public ActionResult MakeTreeSNS() {
            return View();
        }

        [Route("make-tree-sns-stats")]
        public ActionResult MakeTreeSNSStats() {
            return View();
        }
        [Route("excel-upload")]
        [HttpPost]
        public ActionResult ExcelUpload(WinEntry model) {
            string message = "완료되었습니다.";
            List<Dictionary<string, object>> saveDataList = null;
            try {
                if (model.Data == null) {
                    throw new Exception("업로드할 파일을 선택해주세요.");
                }
                var excelData = model.Data;
                saveDataList = common.ExcelUpload(excelData, true);
                repository.CreateChristmas2015WinSettings(saveDataList);
            } catch (Exception e) {
                message = e.Message;
            }
            return RedirectToAction("WinList", "Admin2015Christmas", new { returnMsg = message });
        }

        #region 엑셀다운로드
        [Route("excel-download")]
        public void ExcelDownload(string Type) {
            //즉석당첨 참여자
            if (Type.ToLower().Equals("index")) {
                var query = repository.Christmas2015Wins.Select(e => new WinListEntry {
                    Name = e.Name,
                    Mobile = e.Mobile,
                    Zipcode = e.Zipcode,
                    Address1 = e.Address1,
                    Address2 = e.Address2,
                    PrizeType = e.PrizeType,
                    IpAddress = e.IpAddress,
                    Channel = e.Channel,
                    RegisterDate = e.RegisterDate
                });

                var sql = query.OrderByDescending(e => e.RegisterDate).AsEnumerable().Select(e => new {
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    디바이스 = e.Channel,
                    당첨경품 = e.PrizeName,
                    아이피주소 = e.IpAddress,
                    이름 = e.Name,
                    연락처 = e.Mobile,
                    우편번호 = e.Zipcode,
                    기본주소 = e.Address1,
                    상세주소 = e.Address2
                }).ToList();
                string filename = "[2015 크리스마스이벤트] 즉석당첨 이벤트 참여자_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else if (Type.ToLower().Equals("winlistbyipaddress")) {
                var query = repository.Christmas2015Wins.GroupBy(e => e.IpAddress).Select(e => new WinListByIpAddress {
                    IpAddress = e.Key,
                    TotalCount = e.Count(),
                    LoserCount = e.Count(x => x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Loser),
                    KinderCount = e.Count(x => x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Kinder),
                    CloakCount = e.Count(x => x.PrizeType == Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrize.Cloak)
                }).OrderByDescending(e=>e.IpAddress);

                var sql = query.AsEnumerable().Select(e => new {
                    아이피주소 = e.IpAddress,
                    총참여횟수 = e.TotalCount,
                    꽝 = e.LoserCount,
                    킨더조이기프티콘 = e.KinderCount,
                    망토 = e.CloakCount
                });
                string filename = "[2015 크리스마스이벤트] 즉석당첨 이벤트 타입찾기 IP통계_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else if (Type.ToLower().Equals("winlist")) { //즉석당첨 통계
                var query = repository.Christmas2015WinPrizeSettings.Select(e => new KinderJoy.Domain.Abstract.Christmas2015.Christmas2015EnumConst.Christmas2015WinPrizeSetting {
                    Date = e.Date,
                    PrizeType = e.PrizeType,
                    TotalCount = e.TotalCount,
                    WinnerCount = e.WinnerCount,
                    StartTime = e.StartTime,
                    Rate = e.Rate
                }).OrderBy(e => e.Date);

                var sql = query.AsEnumerable().Select(e => new {
                    날짜 = e.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    경품유형 = e.PrizeName,
                    총수량 = e.TotalCount,
                    당첨수량 = e.WinnerCount,
                    시작시간 = e.StartTime,
                    확률 = e.Rate
                }).ToList();
                string filename = "[2015 크리스마스이벤트] 즉석당첨 이벤트 당첨로직_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else if (Type.ToLower().Equals("maketree")) {
                var query = repository.Christmas2015MakeTree.Select(e => new MakeTreeEntry {
                    Name = e.Name,
                    Mobile = e.Mobile,
                    Zipcode = e.Zipcode,
                    Address1 = e.Address1,
                    Address2 = e.Address2,
                    Age = e.Age,
                    IpAddress = e.IpAddress,
                    Channel = e.Channel,
                    RegisterDate = e.RegisterDate,
                    Toy1 = e.Toy1,
                    Toy2 = e.Toy2,
                    Toy3 = e.Toy3,
                    Toy4 = e.Toy4,
                    Toy5 = e.Toy5,
                    Toy6 = e.Toy6,
                    Toy7 = e.Toy7,
                    SynthesisImage = e.SynthesisImage,
                    Content = e.Content
                }).OrderByDescending(e => e.RegisterDate);
                var sql = query.AsEnumerable().Select(e => new {
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    디바이스 = e.Channel,
                    선택한장난감1 = e.Toy1Name,
                    선택한장난감2 = e.Toy2Name,
                    선택한장난감3 = e.Toy3Name,
                    선택한장난감4 = e.Toy4Name,
                    선택한장난감5 = e.Toy5Name,
                    선택한장난감6 = e.Toy6Name,
                    선택한장난감7 = e.Toy7Name,
                    아이피주소 = e.IpAddress,
                    이름 = e.Name,
                    연락처 = e.Mobile,
                    주소 = e.Address,
                    나이 = (e.Age>0 ? e.Age.ToString() : ""),
                    합성이미지 = e.SynthesisImage,
                    합성내용 = e.Content
                }).ToList();
                string filename = "[2015 크리스마스이벤트] 트리만들기 이벤트 참여자_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else if (Type.ToLower().Equals("maketreesns")) {
                var query = repository.Christmas2015MakeTreeSNSShares.Select(e => new MakeTreeSNSEntry {
                    IpAddress = e.Christmas2015MakeTree.IpAddress,
                    Channel = e.Christmas2015MakeTree.Channel,
                    Name = e.Christmas2015MakeTree.Name,
                    Mobile = e.Christmas2015MakeTree.Mobile,
                    Zipcode = e.Christmas2015MakeTree.Zipcode,
                    Address1 = e.Christmas2015MakeTree.Address1,
                    Address2 = e.Christmas2015MakeTree.Address2,
                    Age = e.Christmas2015MakeTree.Age,
                    SnsType = e.SnsType,
                    SnsId = e.SnsId,
                    SnsName = e.SnsName,
                    SnsNickName = e.SnsNickName,
                    ScrapUrl = e.ScrapUrl,
                    RegisterDate = e.RegisterDate
                }).OrderByDescending(e => e.RegisterDate);

                var sql = query.AsEnumerable().Select(e => new {
                    디바이스 = e.Channel,
                    이름 = e.Name,
                    연락처 = e.Mobile,
                    주소 = e.Address,
                    나이 = (e.Age > 0 ? e.Age.ToString() : ""),
                    SNS유형 = e.SnsType,
                    SNS아이디 = e.SnsId,
                    SNS이름 = e.SnsName,
                    SNS닉네임 = e.SnsNickName,
                    SNS스크랩URL = e.ScrapUrl,
                    아이피주소 = e.IpAddress,
                    참여일 = e.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
                string filename = "[2015 크리스마스이벤트] 트리만들기 이벤트 SNS공유_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            } else {
                var sns = repository.Christmas2015MakeTree.AsQueryable()
                    .Join(repository.Christmas2015MakeTreeSNSShares, e => e.Id, p => p.Christmas2015MakeTreeId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name });

                var query = from s in sns
                            group s by s.Mobile into makeTreeSns
                            select new MakeTreeSNSStats {
                                Mobile = makeTreeSns.Key,
                                Name = makeTreeSns.Max(e => e.Name),
                                FacebookCount = makeTreeSns.Count(e => e.SnsType == "facebook"),
                                KakaostoryCount = makeTreeSns.Count(e => e.SnsType == "kakaostory"),
                                KakaotalkCount = makeTreeSns.Count(e => e.SnsType == "kakaotalk"),
                                TotalCount = makeTreeSns.Count()
                            };
                var sql = query.OrderByDescending(e => e.TotalCount).AsEnumerable().Select(e => new {
                    연락처 = e.Mobile,
                    이름 = e.Name,
                    총공유수 = e.TotalCount,
                    페이스북공유수 = e.FacebookCount,
                    카카오스토리공유수 = e.KakaostoryCount,
                    카카오톡공유수 = e.KakaotalkCount
                }).ToList();
                string filename = "[2015 크리스마스이벤트] 트리만들기 이벤트 SNS공유통계_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                common.ExcelDownLoad(sql, filename);
            }
        }
        #endregion

    }
}