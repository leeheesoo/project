using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Services.TwoYearAnniversary;
using Samsonite.Mall.Infrastructure;
using Samsonite.Mall.Models.TwoYearAnniversaryModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Controllers
{
    [RoutePrefix("second-anniversary")]
    public class TwoYearAnniversaryController : Controller
    {
        private ITwoYearAnniversaryService service;
        private ICommonProvider common;

        public TwoYearAnniversaryController(ITwoYearAnniversaryService service,ICommonProvider common)
        {
            this.service = service;
            this.common = common;
        }
        // GET: TwoYearAnniversary
        [Route("")]
        public ActionResult Index(string custNo="")
        {
            //Session["TwoAnniversarySamsoniteId"] = custNo;
            //string samsoniteId = Session["TwoAnniversarySamsoniteId"] as string;

            ViewBag.CustNo = custNo;

            return View();
        }
        

        [Route("create-entry", Name = "CreateTwoYearAnniversaryEntry")]
        [HttpPost]
        public ActionResult CreateTwoYearAnniversaryEntry(TwoYearCustNoModel custNoModel)
        {
            try
            {
                if (common.Now >= new DateTime(2018,4,19))
                {
                    throw new TwoYearAnniversaryServiceException("400", "이벤트가 종료되었습니다.", null);
                }

                if (string.IsNullOrEmpty(custNoModel.TwoYearCustNo))
                {
                    throw new TwoYearAnniversaryServiceException("400", "쌤소나이트에 로그인 후 시도해주세요.", null);
                }

                var encrytionId = custNoModel.TwoYearCustNo;
                var samsoniteId = decrypt(encrytionId);
                //string samsoniteId= Session["TwoAnniversarySamsoniteId"] as string;
                var entry = new TwoYearAnniversaryEntry()
                {
                    SamsoniteId = encrytionId,
                    SamsoniteEncryptionId = samsoniteId,
                    CreateDate = common.Now,
                    Channel = common.IsMobileDevice ? Domain.Entities.Abstract.ChannelType.Mobile : Domain.Entities.Abstract.ChannelType.PC,
                    IpAddress = common.IpAddress
                };

                var user = service.CreateTwoYearAnniversaryEntry(entry);

                var coupon = service.UpdateTwoYearAnniversaryWinCoupon(user.Id);

                if (coupon != null) {
                    var couponUrl = ConfigurationManager.AppSettings["samsonite.coupon.url"] as string; 

                    string url = string.Format(couponUrl+ "?custNo={0}&coupon={1}", user.SamsoniteId, coupon.CouponCode);
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.Method = "GET";
                    //httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    // 실서버 반영시 주석 해제
                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse()) { }                   
                }
              

                var model = new TwoYearAnniversaryViewModel()
                {
                    PrizeType = user.PrizeType,
                    RouletteType = 4,
                    PrizeName = "wlaossdefr",
                };
                /*
                 *  - 서버 룰렛 순서 = ["1만원쿠폰","스타벅스 기프티콘",  "키즈백팩", "쌤소나이트 정품가방", "20만원쿠폰", "라인프렌즈 콜라보 캐리어"]
                    - 뷰단 룰렛 순서 = ["라인프렌즈 콜라보 캐리어", "20만원쿠폰", "키즈백팩", "스타벅스 기프티콘", "1만원쿠폰", "쌤소나이트 정품가방"]
                 */
                switch (user.PrizeType)
                {
                    case TwoYearAnniversarInstantPrizeType.Loser:
                    default:
                        model.PrizeName = "wlaossdefr";
                        model.RouletteType = 4;
                        break;
                    case TwoYearAnniversarInstantPrizeType.StarBucks:
                        model.PrizeName = "wzzxjcvvl";
                        model.RouletteType = 3;
                        break;
                    case TwoYearAnniversarInstantPrizeType.AtKidsBagPack:
                        model.PrizeName = "wgrfkdqskad";
                        model.RouletteType = 2;
                        break;
                    case TwoYearAnniversarInstantPrizeType.OriginalBag:
                        model.PrizeName = "wtrrkeqwkqd";
                        model.RouletteType = 5;
                        break;
                    case TwoYearAnniversarInstantPrizeType.Coupon_20:
                        model.PrizeName = "wlzknjvhhgs";
                        model.RouletteType = 1;
                        break;
                    case TwoYearAnniversarInstantPrizeType.LineFriendsCarrier:
                        model.PrizeName = "wmznobfvlcdxj";
                        model.RouletteType = 0;
                        break;
                }

                return Json(new
                {
                    result = true,
                    data = model
                });
            }
            catch (TwoYearAnniversaryServiceException e)
            {
                return Json(new
                {
                    result = false,
                    message = e.Message
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = false,
                    message = "서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다."
                });
            }
        }



        public static byte[] FromHexByte(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        private string decrypt(string encryptCustNo)
        {
            // "8c6621e42a1b8e853a3bb6cca844ad998ddb90859417200cacd426defafbacc8";
            var key = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.ASCII.GetBytes("SamsoniteKR"));// "SamsoniteKR"을 md5 hashing한 hex값
            var iv = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };  //iv
            var decrypted = "";
            var rijAlg = new RijndaelManaged();
            rijAlg.Key = key;
            rijAlg.BlockSize = 128;
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.IV = iv;
            rijAlg.Padding = PaddingMode.PKCS7;
            
            try
            {
                var buffer = FromHexByte(encryptCustNo);
                ICryptoTransform decryptor = rijAlg.CreateDecryptor();
                using (var ms = new MemoryStream(buffer))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                        cs.Close();
                    }
                    ms.Close();
                }
            }
            catch (Exception e)
            {
                decrypted = "부정참여자";
            }
            return decrypted;
        }
    }
}