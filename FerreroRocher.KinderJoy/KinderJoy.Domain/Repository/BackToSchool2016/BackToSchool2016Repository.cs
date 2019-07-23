using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.BackToSchool2016;
using KinderJoy.Domain.Infrastructure;
using System.Data.Entity.SqlServer;

namespace KinderJoy.Domain.Repository.BackToSchool2016
{
    public class BackToSchool2016Repository : IBackToSchool2016Repository
    {
        private AppDbContext db;

        public IQueryable<BackToSchool2016BingoQuiz> BackToSchool2016BingoQuiz
        {
            get
            {
                return db.BackToSchool2016BingoQuiz;
            }
        }

        public IQueryable<BackToSchool2016BingoQuizSns> BackToSchool2016BingoQuizSns
        {
            get
            {
                return db.BackToSchool2016BingoQuizSns;
            }
        }

        public BackToSchool2016Repository(AppDbContext db)
        {
            this.db = db;
        }

        public BackToSchool2016Repository()
        {
            db = AppDbContext.Create();
        }

        public BackToSchool2016BingoQuiz CreateBackToSchool2016BingoQuiz(BackToSchool2016BingoQuiz entry)
        {
            var result = db.BackToSchool2016BingoQuiz.Add(entry);
            db.SaveChanges();

            return result;
        }

        public void CreateBackToSchool2016BingoQuizSns(BackToSchool2016BingoQuizSns entry)
        {
            var result = db.BackToSchool2016BingoQuizSns.Add(entry);
            db.SaveChanges();
        }

        public int GetBackToSchool2016BingoQuizSnsShareCount(string snsType, string mobile, string today)
        {
            var query = db.BackToSchool2016BingoQuizSns.AsQueryable()
                .Join(db.BackToSchool2016BingoQuiz, e => e.BackToSchool2016BingoQuizId, p => p.Id, (e, p) => new { SnsType = e.SnsType, Mobile = p.Mobile, RegistDate = SqlFunctions.DatePart("year", p.CreateDate) + "-" + (SqlFunctions.DatePart("month", p.CreateDate).ToString().Length == 1 ? "0"+ SqlFunctions.DatePart("month", p.CreateDate).ToString() : SqlFunctions.DatePart("month", p.CreateDate).ToString()) + "-" + (SqlFunctions.DatePart("day", p.CreateDate).ToString().Length == 1 ? "0" + SqlFunctions.DatePart("day", p.CreateDate).ToString() : SqlFunctions.DatePart("day", p.CreateDate).ToString()) });
            query = query.Where(e => e.RegistDate.Equals(today) && e.SnsType.Equals(snsType.ToLower()) && e.Mobile.Equals(mobile));
            
            return query.Count();
        }
    }
}
