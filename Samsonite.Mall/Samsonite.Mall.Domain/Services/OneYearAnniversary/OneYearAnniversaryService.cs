using Samsonite.Mall.Domain.Repositories.OneYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Exceptions;

namespace Samsonite.Mall.Domain.Services.OneYearAnniversary {
    public class OneYearAnniversaryService : IOneYearAnniversaryService {
        private IOneYearAnniversaryRepository repository;
        public OneYearAnniversaryService(IOneYearAnniversaryRepository repository) {
            this.repository = repository;
        }

        #region [c]reate
        /// <summary>
        /// 댓글등록이벤트 entity 생성
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public OneYearAnniversaryEntry CreateOneYearAnniversaryEntry(OneYearAnniversaryEntry entry) {
            repository.Add(entry);
            repository.Save();
            return entry;
        }
        #endregion

        #region [r]ead
        /// <summary>
        /// 댓글등록이벤트 전체data 가져오기
        /// </summary>
        /// <returns></returns>
        public IList<OneYearAnniversaryEntry> GetOneYearAnniversaryEntryAll() {
            return repository.GetAll().ToList();
        }
        /// <summary>
        /// 댓글등록이벤트 특정data 가져오기
        /// </summary>
        /// <param name="id">가져올 data id</param>
        /// <returns></returns>
        public OneYearAnniversaryEntry GetOneYearAnniversaryEntry(long id) {
            return repository.SingleOrDefault(x => x.Id == id);
        }
        #endregion

        #region [u]pdate
        /// <summary>
        /// 특정 댓글등록이벤트 변경하기
        /// </summary>
        /// <param name="entry">변경할 댓글등록이벤트 entry</param>
        /// <returns></returns>
        public OneYearAnniversaryEntry UpdateOneYearAnniversaryEntry(OneYearAnniversaryEntry entry) {
            var checkEntry = repository.SingleOrDefault(x => x.Id == entry.Id);
            if(checkEntry == null) {
                throw new OneYearAnniversaryServiceException("변경하려는 이벤트 정보가 없습니다.", entry);
            }
            repository.Update(entry);
            repository.Save();
            return entry;
        }
        #endregion

        #region [d]elete
        /// <summary>
        /// 특정 댓글등록이벤트 삭제하기(DB에서 노출여부만 false로)
        /// </summary>
        /// <param name="id">삭제할 data id</param>
        public void DeleteOneYearAnniversaryEntry(long id) {
            var entry = repository.SingleOrDefault(x => x.Id == id);
            if (entry == null) {
                //throw new OneYearAnniversaryServiceException("삭제하려는 이벤트 정보가 없습니다.", id);
                throw new OneYearAnniversaryServiceException("노출여부를 변경할 이벤트 정보가 없습니다.", id);
            }
            //repository.Remove(entry);
            entry.IsShow = !entry.IsShow;
            repository.Update(entry);
            repository.Save();
        }
        #endregion
    }
    public class OneYearAnniversaryServiceException : EventServiceException {
        public OneYearAnniversaryServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
        public OneYearAnniversaryServiceException(string message, object data)
            : base(message, data) {

        }
    }
}
