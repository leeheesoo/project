using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.KidsNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Repositories.MarketingAgree;

namespace INGLife.Event.Domain.Services.MarketingAgree {
    public class MarketingAgreeService : IMarketingAgreeService {
        private IMarketingAgreeEntryRepository repo;

        public MarketingAgreeService(IMarketingAgreeEntryRepository repo) {
            this.repo = repo;
        }
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MarketingAgreeEntry CreateMarketingAgreeEntry(MarketingAgreeEntry entry) {
            if (IsMarketingAgreeEntryByMobile(entry.Mobile)) {
                throw new MarketingAgreeServiceException("400", "이미 참여하였습니다.", entry);
            }
            var result = repo.Add(entry);
            repo.Save();
            return result;
        }

        public IQueryable<MarketingAgreeEntry> GetAdminMarketingAgreeEntryList() {
            return repo.GetAll().Where(x => x.UpdateDate.HasValue).OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MarketingAgreeEntry GetMarketingAgreeEntryById(long id) {
            var result = repo.SingleOrDefault(x => x.Id == id && !x.UpdateDate.HasValue);
            if (result == null) {
                throw new MarketingAgreeServiceException("400", "다시 시도해주세요.", null);
            }
            return result;
        }
        /// <summary>
        /// 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MarketingAgreeEntry UpdateMarketingAgreeEntry(MarketingAgreeEntry entry) {
            if (IsMarketingAgreeEntryByMobile(entry.Mobile)) {
                throw new MarketingAgreeServiceException("400", "이미 참여하였습니다.", entry);
            }
            var result = repo.Update(entry);
            repo.Save();
            return result;
        }

        /// <summary>
        /// 참여이력 가져오기
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        private bool IsMarketingAgreeEntryByMobile(string mobile) {

            return repo.Any(x => x.Mobile == mobile && x.UpdateDate.HasValue);
        }
    }
    public class MarketingAgreeServiceException : EventServiceException {
        public MarketingAgreeServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
