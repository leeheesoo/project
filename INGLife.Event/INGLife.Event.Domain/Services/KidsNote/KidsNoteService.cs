using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.KidsNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.KidsNote {
    public class KidsNoteService : IKidsNoteService {
        private IKidsNoteEntryRepository repo;

        public KidsNoteService(IKidsNoteEntryRepository repo) {
            this.repo = repo;
        }
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public KidsNoteEntry CreateKidsNoteEntry(KidsNoteEntry entry) {
            if (IsKidsNoteEntryByMobile(entry.Mobile)) {
                throw new KidsNoteServiceException("400", "이미 참여하였습니다.", entry);
            }
            var result = repo.Add(entry);
            repo.Save();
            return result;
        }
        /// <summary>
        /// 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public KidsNoteEntry UpdateKidsNoteEntry(KidsNoteEntry entry) {
            if (IsKidsNoteEntryByMobile(entry.Mobile)) {
                throw new KidsNoteServiceException("400", "이미 참여하였습니다.", entry);
            }
            var result = repo.Update(entry);
            repo.Save();
            return result;
        }
        /// <summary>
        /// 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KidsNoteEntry GetKidsNoteEntryById(long id) {
            var result = repo.SingleOrDefault(x => x.Id == id && !x.UpdateDate.HasValue);
            if (result == null) {
                throw new KidsNoteServiceException("400", "다시 시도해주세요.", null);
            }
            return result;
        }

        /// <summary>
        /// 참여이력 가져오기
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        private bool IsKidsNoteEntryByMobile(string mobile) {
          
            return repo.Any(x => x.Mobile == mobile && x.UpdateDate.HasValue);
        }

        /// <summary>
        /// 이벤트타입별 선착순 제어
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public bool IsEntry(KidsNoteEventType eventType, int limit) {
            var entryCount = repo.Filter(x => x.EventType == eventType && x.UpdateDate.HasValue).Count();
            return entryCount >= limit;
        }

        public IQueryable<KidsNoteEntry> GetAdminKidsNoteEntryList() {
            return repo.GetAll().Where(x => x.UpdateDate.HasValue).OrderByDescending(x => x.CreateDate);
        }
    }
    public class KidsNoteServiceException : EventServiceException {
        public KidsNoteServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
