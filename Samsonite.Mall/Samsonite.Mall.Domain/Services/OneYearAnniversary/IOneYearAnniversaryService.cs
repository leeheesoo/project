using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using System.Collections.Generic;

namespace Samsonite.Mall.Domain.Services.OneYearAnniversary {
    public interface IOneYearAnniversaryService {
        #region [c]reate
        /// <summary>
        /// 댓글등록이벤트 entity 생성
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        OneYearAnniversaryEntry CreateOneYearAnniversaryEntry(OneYearAnniversaryEntry entry);
        #endregion
        #region [r]ead
        /// <summary>
        /// 댓글등록이벤트 전체data 가져오기
        /// </summary>
        /// <returns></returns>
        IList<OneYearAnniversaryEntry> GetOneYearAnniversaryEntryAll();
        /// <summary>
        /// 댓글등록이벤트 특정data 가져오기
        /// </summary>
        /// <param name="id">가져올 data id</param>
        /// <returns></returns>
        OneYearAnniversaryEntry GetOneYearAnniversaryEntry(long id);
        #endregion
        #region [u]pdate
        /// <summary>
        /// 특정 댓글등록이벤트 변경하기
        /// </summary>
        /// <param name="entry">변경할 댓글등록이벤트 entry</param>
        /// <returns></returns>
        OneYearAnniversaryEntry UpdateOneYearAnniversaryEntry(OneYearAnniversaryEntry entry);
        #endregion
        #region [d]elete
        /// <summary>
        /// 특정 댓글등록이벤트 삭제하기(DB에서 노출여부만 false로)
        /// </summary>
        /// <param name="id">삭제할 data id</param>
        void DeleteOneYearAnniversaryEntry(long id);
        #endregion
    }
}
