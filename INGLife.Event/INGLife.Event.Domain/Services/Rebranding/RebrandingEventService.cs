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
using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Domain.Repositories.Rebranding;

namespace INGLife.Event.Domain.Services.Rebranding {
    public class RebrandingEventService : IRebrandingEventService {
        private IRebrandingConsultingAgreeEntryRepository consultingRepo;
        private IRebrandingMarketingAgreeEntryRepository marketingRepo;

        public RebrandingEventService(IRebrandingConsultingAgreeEntryRepository consultingRepo, IRebrandingMarketingAgreeEntryRepository marketingRepo) {
            this.consultingRepo = consultingRepo;
            this.marketingRepo = marketingRepo;
        }

        /// <summary>
        /// 상담동의 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public RebrandingConsultingAgreeEntry CreateRebrandingConsultingAgreeEntry(RebrandingConsultingAgreeEntry entry) {
            if (this.IsEntryByMobile(entry.Mobile)) {
                throw new RebrandingEventServiceException("400", "이미 참여하였습니다.", entry.Mobile);
            }
            var result = consultingRepo.Add(entry);
            consultingRepo.Save();
            return result;
        }
        /// <summary>
        /// 마케팅동의 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public RebrandingMarketingAgreeEntry CreateRebrandingMarketingAgreeEntry(RebrandingMarketingAgreeEntry entry) {
            if (this.IsEntryByMobile(entry.Mobile)) {
                throw new RebrandingEventServiceException("400", "이미 참여하였습니다.", entry.Mobile);
            }
            var result = marketingRepo.Add(entry);
            marketingRepo.Save();
            return result;
        }
        /// <summary>
        /// 상담동의 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RebrandingConsultingAgreeEntry GetRebrandingConsultingAgreeEntryById(long id) {  
            var result = consultingRepo.SingleOrDefault(x => x.Id == id && !x.UpdateDate.HasValue);
            if (result == null) {
                throw new RebrandingEventServiceException("400", "응모정보가 존재하지 않습니다. 다시 시도해주세요.", null);
            }
            return result;
        }
        /// <summary>
        /// 마케팅동의 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RebrandingMarketingAgreeEntry GetRebrandingMarketingAgreeEntryById(long id) {
            var result = marketingRepo.SingleOrDefault(x => x.Id == id && !x.UpdateDate.HasValue);
            if (result == null) {
                throw new RebrandingEventServiceException("400", "응모정보가 존재하지 않습니다. 다시 시도해주세요.", null);
            }
            return result;
        }
        /// <summary>
        /// 상담동의 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public RebrandingConsultingAgreeEntry UpdateRebrandingConsultingAgreeEntry(RebrandingConsultingAgreeEntry entry) {
            var result = consultingRepo.Update(entry);
            consultingRepo.Save();
            return result;
        }
        /// <summary>
        /// 마케팅동의 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public RebrandingMarketingAgreeEntry UpdateRebrandingMarketingAgreeEntry(RebrandingMarketingAgreeEntry entry) {
            var result = marketingRepo.Update(entry);
            marketingRepo.Save();
            return result;
        }
        /// <summary>
        /// 마케팅동의 응모 수 제한
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public bool IsRebrandingMarketingEntryLimit(int limit) {
            var entryCount = marketingRepo.Filter(x => x.UpdateDate.HasValue).Count();
            return entryCount >= limit;
        }
        /// <summary>
        /// 상담동의 응모 수 제한
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public bool IsRebrandingConsultingEntryLimit(int limit) {
            var entryCount = consultingRepo.Filter(x => x.UpdateDate.HasValue).Count();
            return entryCount >= limit;
        }

        /// <summary>
        /// 이벤트 응모 여부
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        private bool IsEntryByMobile(string mobile) {
            return marketingRepo.Any(x => x.Mobile == mobile && x.UpdateDate.HasValue) || consultingRepo.Any(x => x.Mobile == mobile && x.UpdateDate.HasValue);
        }

        public IQueryable<RebrandingMarketingAgreeEntry> GetAdminRebrandingMarketingEntryList(string month) {
            if (month == "october") {
                return marketingRepo.Filter(x => x.UpdateDate.HasValue && x.UpdateDate.Value >= new DateTime(2018, 10, 15)).OrderByDescending(x => x.CreateDate);
            }
            return marketingRepo.Filter(x => x.UpdateDate.HasValue && x.UpdateDate.Value < new DateTime(2018,10,15)).OrderByDescending(x => x.CreateDate);
        }

        public IQueryable<RebrandingConsultingAgreeEntry> GetAdminRebrandingConsultingEntryList(string month) {
            if (month == "october") {
                return consultingRepo.Filter(x => x.UpdateDate.HasValue && x.UpdateDate.Value >= new DateTime(2018, 10, 15)).OrderByDescending(x => x.CreateDate);
            }
            return consultingRepo.Filter(x => x.UpdateDate.HasValue && x.UpdateDate.Value < new DateTime(2018, 10, 15)).OrderByDescending(x => x.CreateDate);
        }
    }
    public class RebrandingEventServiceException : EventServiceException {
        public RebrandingEventServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
