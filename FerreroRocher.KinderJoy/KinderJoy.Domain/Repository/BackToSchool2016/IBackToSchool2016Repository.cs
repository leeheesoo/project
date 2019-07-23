using KinderJoy.Domain.Entities.BackToSchool2016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.BackToSchool2016
{
    public interface IBackToSchool2016Repository
    {
        IQueryable<BackToSchool2016BingoQuiz> BackToSchool2016BingoQuiz { get; }
        IQueryable<BackToSchool2016BingoQuizSns> BackToSchool2016BingoQuizSns { get; }

        /// <summary>
        /// 개인정보저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        BackToSchool2016BingoQuiz CreateBackToSchool2016BingoQuiz(BackToSchool2016BingoQuiz entry);

        /// <summary>
        /// Sns 공유저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        void CreateBackToSchool2016BingoQuizSns(BackToSchool2016BingoQuizSns entry);

        int GetBackToSchool2016BingoQuizSnsShareCount(string snsType, string mobile, string today);
    }
}
