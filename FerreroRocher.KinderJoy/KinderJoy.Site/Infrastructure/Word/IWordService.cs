using KinderJoy.Domain.Entities.Word;
using KinderJoy.Site.Models.Word;
using System;
using System.Collections.Generic;
namespace KinderJoy.Site.Infrastructure.Word
{
    public interface IWordService
    {
        /// <summary>
        /// 응모 데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        WordEvent EntryWord(WordEntry entry, string ip);
        
        /// <summary>
        /// 공유내역 저장
        /// </summary>
        /// <param name="entry"></param>
        void ShareWord(WordShare entry);

        /// <summary>
        /// 이벤트 응모 회차 계산
        /// </summary>
        /// <returns></returns>
        int CalcSt();

        /// <summary>
        /// 회차별 당첨 데이터가 있는지 확인
        /// </summary>
        /// <param name="St"></param>
        /// <returns></returns>
        bool IsWinnerResultSt(int St);

        /// <summary>
        /// 회차별 당첨자 추출
        /// </summary>
        /// <param name="St"></param>
        /// <param name="GiftType"></param>
        /// <returns></returns>
        List<WordWinner> GetWordWinners(int St, string GiftType);

        /// <summary>
        /// 당첨차 정보 최종 회차
        /// </summary>
        /// <returns></returns>
        int CalcWinnerLastSt();
    }
}
