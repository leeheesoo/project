using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Entities.Rebranding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Rebranding {
    public interface IRebrandingEventService {
        /// <summary>
        /// 마케팅동의 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        RebrandingMarketingAgreeEntry CreateRebrandingMarketingAgreeEntry(RebrandingMarketingAgreeEntry entry);
        /// <summary>
        /// 상담동의 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        RebrandingConsultingAgreeEntry CreateRebrandingConsultingAgreeEntry(RebrandingConsultingAgreeEntry entry);
        /// <summary>
        /// 마케팅동의 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        RebrandingMarketingAgreeEntry UpdateRebrandingMarketingAgreeEntry(RebrandingMarketingAgreeEntry entry);
        /// <summary>
        /// 상담동의 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        RebrandingConsultingAgreeEntry UpdateRebrandingConsultingAgreeEntry(RebrandingConsultingAgreeEntry entry);
        /// <summary>
        /// 마케팅동의 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RebrandingMarketingAgreeEntry GetRebrandingMarketingAgreeEntryById(long id);
        /// <summary>
        /// 상담동의 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RebrandingConsultingAgreeEntry GetRebrandingConsultingAgreeEntryById(long id);
        /// <summary>
        /// 마케팅동의 응모 수 제한
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        bool IsRebrandingMarketingEntryLimit(int limit);
        /// <summary>
        /// 상담동의 응모 수 제한
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        bool IsRebrandingConsultingEntryLimit(int limit);

        IQueryable<RebrandingMarketingAgreeEntry> GetAdminRebrandingMarketingEntryList(string month);
        IQueryable<RebrandingConsultingAgreeEntry> GetAdminRebrandingConsultingEntryList(string month);
    }
}
