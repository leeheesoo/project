using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.MarketingAgree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.MarketingAgree {
    public interface IMarketingAgreeService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MarketingAgreeEntry CreateMarketingAgreeEntry(MarketingAgreeEntry entry);
        /// <summary>
        /// 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MarketingAgreeEntry UpdateMarketingAgreeEntry(MarketingAgreeEntry entry);
        /// <summary>
        /// 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MarketingAgreeEntry GetMarketingAgreeEntryById(long id);
        
        IQueryable<MarketingAgreeEntry> GetAdminMarketingAgreeEntryList();
    }
}
