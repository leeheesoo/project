using Excel;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Entities.Word;
using KinderJoy.Site.Infrastructure.Admin;
using KinderJoy.Site.Models;
using KinderJoy.Site.Models.Admin.WordEvent;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KinderJoy.Site.Controllers
{
    [Authorize]
    [RoutePrefix("manager/AdminWordEvent")]
    public class AdminWordEventController : Controller
    {
        private AppDbContext db;

        public AdminWordEventController(AppDbContext db)
        {
            this.db = db;
        }
        [Route("")]
        public ActionResult Index(string SearchType = "", string SearchValue = "", string StartDate = "", string EndDate = "", int page = 1)
        {
            var list = db.WordEvents.AsQueryable();
            
            if(SearchType.Equals("Name"))
            {
                list = list.Where(e => e.PersonalInfo.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.PersonalInfo.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }

            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.PersonalInfo.Age == age);
            }

            if (SearchType.Equals("Gender"))
            {
                list = list.Where(e => e.PersonalInfo.Gender == SearchValue);
            }

            if (SearchType.Equals("GiftType"))
            {
                list = list.Where(e => e.GiftType == SearchValue);
            }

            if (string.IsNullOrEmpty(StartDate) == false)
            {
                DateTime startDate = Convert.ToDateTime(StartDate);
                list = list.Where(e => e.RegisteredAt >= startDate);
            }

            if (string.IsNullOrEmpty(EndDate) == false)
            {
                DateTime endDate = Convert.ToDateTime(EndDate);
                endDate = endDate.AddHours(23);
                endDate = endDate.AddMinutes(59);
                endDate = endDate.AddSeconds(59);
                list = list.Where(e => e.RegisteredAt <= endDate);
            }

            ViewBag.SearchType = SearchType;
            ViewBag.SearchValue = SearchValue;

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            return View(list.OrderByDescending(e=> e.Id).ToPagedList(page, 30));
        }

        [Route("Excel")]
        public void Excel(string SearchType = "", string SearchValue = "", string StartDate = "", string EndDate = "")
        {
            var list = db.WordEvents.AsQueryable();

            if (SearchType.Equals("Name"))
            {
                list = list.Where(e => e.PersonalInfo.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.PersonalInfo.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }

            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.PersonalInfo.Age == age);
            }

            if (SearchType.Equals("Gender"))
            {
                list = list.Where(e => e.PersonalInfo.Gender == SearchValue);
            }

            if (SearchType.Equals("GiftType"))
            {
                list = list.Where(e => e.GiftType == SearchValue);
            }
            
            if (string.IsNullOrEmpty(StartDate) == false)
            {
                DateTime startDate = Convert.ToDateTime(StartDate);
                list = list.Where(e => e.RegisteredAt >= startDate);
            }

            if (string.IsNullOrEmpty(EndDate) == false)
            {
                DateTime endDate = Convert.ToDateTime(EndDate);
                endDate = endDate.AddHours(23);
                endDate = endDate.AddMinutes(59);
                endDate = endDate.AddSeconds(59);
                list = list.Where(e => e.RegisteredAt <= endDate);
            }

            list = list.OrderByDescending(e => e.Id);
            var excel = from e in list
                        select new
                        {
                            이름 = e.PersonalInfo.Name,
                            연락처 = e.PersonalInfo.Mobile,
                            성별 = e.PersonalInfo.Gender,
                            나이 = e.PersonalInfo.Age,
                            상품타입 = e.GiftType,
                            응모채널 = e.Channel,
                            아이피 = e.Ip,
                            등록일 = e.RegisteredAt
                        };
            string filename = "[킨더조이]_이벤트_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            EventUtil.ExcelDownLoad(excel.ToList(), filename);
        }

        [Route("Share")]
        public ActionResult Share(string SearchType = "", string SearchValue = "", string StartDate = "", string EndDate = "", int page = 1)
        {
            var list = db.WordEventShares.AsQueryable();

            if (SearchType.Equals("Name"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }

            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.WordEvent.PersonalInfo.Age == age);
            }

            if (SearchType.Equals("Gender"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Gender == SearchValue);
            }

            if (SearchType.Equals("GiftType"))
            {
                list = list.Where(e => e.WordEvent.GiftType == SearchValue);
            }

            if (SearchType.Equals("SnsType"))
            {
                list = list.Where(e => e.SnsType == SearchValue);
            }
            
            if (string.IsNullOrEmpty(StartDate) == false)
            {
                DateTime startDate = Convert.ToDateTime(StartDate);
                list = list.Where(e => e.RegisteredAt >= startDate);
            }

            if (string.IsNullOrEmpty(EndDate) == false)
            {
                DateTime endDate = Convert.ToDateTime(EndDate);
                endDate = endDate.AddHours(23);
                endDate = endDate.AddMinutes(59);
                endDate = endDate.AddSeconds(59);
                list = list.Where(e => e.RegisteredAt <= endDate);
            }

            list = list.OrderByDescending(e => e.Id);
            
            ViewBag.SearchType = SearchType;
            ViewBag.SearchValue = SearchValue;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            return View(list.ToPagedList(page, 30));
        }

        [Route("ExcelShare")]
        public void ExcelShare(string SearchType = "", string SearchValue = "", string StartDate = "", string EndDate = "")
        {
            var list = db.WordEventShares.AsQueryable();

            if (SearchType.Equals("Name"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }

            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.WordEvent.PersonalInfo.Age == age);
            }

            if (SearchType.Equals("Gender"))
            {
                list = list.Where(e => e.WordEvent.PersonalInfo.Gender == SearchValue);
            }

            if (SearchType.Equals("GiftType"))
            {
                list = list.Where(e => e.WordEvent.GiftType == SearchValue);
            }

            if (SearchType.Equals("SnsType"))
            {
                list = list.Where(e => e.SnsType == SearchValue);
            }

            if (string.IsNullOrEmpty(StartDate) == false)
            {
                DateTime startDate = Convert.ToDateTime(StartDate);
                list = list.Where(e => e.RegisteredAt >= startDate);
            }

            if (string.IsNullOrEmpty(EndDate) == false)
            {
                DateTime endDate = Convert.ToDateTime(EndDate);
                endDate = endDate.AddHours(23);
                endDate = endDate.AddMinutes(59);
                endDate = endDate.AddSeconds(59);
                list = list.Where(e => e.RegisteredAt <= endDate);
            }

            list = list.OrderByDescending(e => e.Id);
            var excel = from e in list
                        select new
                        {
                            이름 = e.WordEvent.PersonalInfo.Name,
                            연락처 = e.WordEvent.PersonalInfo.Mobile,
                            성별 = e.WordEvent.PersonalInfo.Gender,
                            나이 = e.WordEvent.PersonalInfo.Age,
                            상품타입 = e.WordEvent.GiftType,
                            SNS = e.SnsType,
                            SNSID = e.SnsId,
                            POSTID = e.PostId,
                            등록일 = e.RegisteredAt
                        };
            string filename = "[킨더조이]_이벤트_공유_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            EventUtil.ExcelDownLoad(excel.ToList(), filename);
        }

        [Route("Stats")]
        public ActionResult Stats()
        {
            IEnumerable<WordStats> stats = db.Database.SqlQuery<WordStats>(@"
            SELECT
	            CONVERT(NVARCHAR(10), e.RegisteredAt, 126) as Date,
	            COUNT(*) as Count,
	            COUNT(DISTINCT p.Mobile) as DistinctCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'facebook' AND Channel = 'Web' ) as FacebookCount
                ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'facebook' AND Channel = 'Mobile' ) as FacebookMobileCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'twitter' AND Channel = 'Web') as TwitterCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'twitter' AND Channel = 'Mobile') as TwitterMobileCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'kakaostory' AND Channel = 'Web') as KakaostoryCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'kakaostory' AND Channel = 'Mobile') as KakaostoryMobileCount
	            ,(SELECT COUNT(*) FROM WordEventShares WHERE CONVERT(NVARCHAR(10), RegisteredAt, 126)=CONVERT(NVARCHAR(10), e.RegisteredAt, 126) AND SnsType = 'kakaotalk' ) as KakaotalkCount
            FROM
	            dbo.WordEventShares e
	            INNER JOIN
	            dbo.WordEvents m
	            ON e.WordEventId = m.Id
	            LEFT OUTER JOIN
	            dbo.PersonalInfos p
	            ON m.PersonalInfoId = p.PersonalInfoId
            GROUP BY CONVERT(NVARCHAR(10), e.RegisteredAt, 126)
            ORDER BY CONVERT(NVARCHAR(10), e.RegisteredAt, 126) DESC
            ");
            return View(stats);
        }

        [Route("Winners")]
        public ActionResult Winners(string SearchType = "", string SearchValue = "", int page = 1, int St = 1)
        {
            ViewBag.SearchType = SearchType;
            ViewBag.SearchValue = SearchValue;
            ViewBag.St = St;
            IEnumerable<WordWinners> list = db.Database.SqlQuery<WordWinners>(@"
            SELECT 
	            Name, Mobile, Age, MaleCount, FemaleCount, GiftMaleCount, GiftFemaleCount, TotalEntry, TotalShare
            FROM (
	            SELECT
		            MAX(p.Name) as Name
		            ,p.Mobile
		            ,p.Age
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inp.Gender = 'M' ) as MaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inp.Gender = 'F' ) as FemaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inm.GiftType = 'M' ) as GiftMaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inm.GiftType = 'F' ) as GiftFemaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile ) as TotalEntry
		            ,(SELECT COUNT(*) FROM dbo.WordEventShares ins INNER JOIN dbo.WordEvents inm ON ins.WordEventId = inm.Id INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile ) as TotalShare
	            FROM
		            dbo.WordEvents m
		            INNER JOIN
		            dbo.PersonalInfos p
		            ON m.PersonalInfoId = p.PersonalInfoId
	            WHERE
		            m.St = " + St.ToString() + @"
	            GROUP BY p.Mobile, p.Age
            ) tb
            ORDER BY tb.TotalEntry DESC, TotalShare DESC
            ");

            if (SearchType.Equals("Name"))
            {
                list = list.Where(e => e.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }
            
            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.Age == age);
            }
            return View(list.ToPagedList(page, 30));
        }

        [Route("ExcelWinners")]
        public void ExcelWinners(string SearchType = "", string SearchValue = "", int St = 1)
        {
            IEnumerable<WordWinners> list = db.Database.SqlQuery<WordWinners>(@"
            SELECT 
	            Name, Mobile, Age, MaleCount, FemaleCount, GiftMaleCount, GiftFemaleCount, TotalEntry, TotalShare, St
            FROM (
	            SELECT
		            MAX(p.Name) as Name
		            ,p.Mobile
		            ,p.Age
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inp.Gender = 'M' ) as MaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inp.Gender = 'F' ) as FemaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inm.GiftType = 'M' ) as GiftMaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile AND inm.GiftType = 'F' ) as GiftFemaleCount
		            ,(SELECT COUNT(*) FROM dbo.WordEvents inm INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile ) as TotalEntry
		            ,(SELECT COUNT(*) FROM dbo.WordEventShares ins INNER JOIN dbo.WordEvents inm ON ins.WordEventId = inm.Id INNER JOIN dbo.PersonalInfos inp ON inm.PersonalInfoId = inp.PersonalInfoId WHERE inp.Mobile = p.Mobile ) as TotalShare
                    ,Max(St) as St
	            FROM
		            dbo.WordEvents m
		            INNER JOIN
		            dbo.PersonalInfos p
		            ON m.PersonalInfoId = p.PersonalInfoId
	            WHERE
		            m.St = " + St.ToString() + @"
	            GROUP BY p.Mobile, p.Age
            ) tb
            ORDER BY tb.TotalEntry DESC, TotalShare DESC
            ");

            if (SearchType.Equals("Name"))
            {
                list = list.Where(e => e.Name == SearchValue);
            }

            if (SearchType.Equals("Mobile"))
            {
                list = list.Where(e => e.Mobile == EventUtil.FormatMobile(SearchValue, ""));
            }

            if (SearchType.Equals("Age"))
            {
                int age = Int32.Parse(SearchValue);
                list = list.Where(e => e.Age == age);
            }
            
            string filename = "[킨더조이]_이벤트_요약_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            EventUtil.ExcelDownLoad(list.ToList(), filename);
        }

        [Route("WinnersList")]
        public ActionResult WinnersList()
        {
            return View();
        }

        [Route("WinnersUpload")]
        public JsonResult WinnersUpload(HttpPostedFileBase UpFile, int St = 1)
        {
            ResultModel result = new ResultModel
            {
                Result = false,
                Message = ""
            };
            string extension = Path.GetExtension(UpFile.FileName).ToLower();

            if (extension != ".xls" && extension != ".xlsx")
            {
                result.Message = "엑셀 파일만 사용 가능합니다.";
                return Json(result);
            }

            IExcelDataReader excelReader;
            if (extension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(UpFile.InputStream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(UpFile.InputStream);
            }

            List<WordWinner> wordWinners = new List<WordWinner>();
            excelReader.IsFirstRowAsColumnNames = true;
            int i = 0;
            while (excelReader.Read())
            {
                if (i != 0)
                {
                    wordWinners.Add(new WordWinner
                    {
                        St = St,
                        Name = excelReader.GetString(0),
                        Mobile = excelReader.GetString(1),
                        GiftType = excelReader.GetString(2)
                    });
                }
                i++;
            }
            excelReader.Close();
            db.WordWinners.RemoveRange(db.WordWinners.Where(e => e.St == St));
            wordWinners.ForEach(n => db.WordWinners.Add(n));
            db.SaveChanges();
            result.Result = true;
            result.Message = "정상적으로 처리 되었습니다.";
            return Json(result);
        }

        [Route("WinnersMembers")]
        public ActionResult WinnersMembers(int St)
        {
            return View( db.WordWinners.Where(e => e.St == St).OrderByDescending(e => e.Id).ToList() );
        }
	}
}