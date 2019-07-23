using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.FinancialConcertMarketingAgree {
    public interface IFinancialConcertMarketingAgreeService {
        long Create (FinancialConcertMarketingAgreeEntry entities);
        void Update (FinancialConcertMarketingAgreeEntry updateEntities);
        /// <summary>
        /// 본인인증한 데이터와 검증
        /// </summary>
        /// <param name="entryId">본인인증한 사용자 아이디</param>
        /// <param name="name">이름</param>
        /// <param name="mobile">휴대폰</param>
        /// <param name="gender">성별</param>
        /// <param name="birthDay">생년월일</param>
        /// <returns></returns>
        FinancialConcertMarketingAgreeEntry CheckCertEntry (long entryId, string name, string mobile, string gender, string birthDay);
        IList<FinancialConcertMarketingAgreeEntry> GetAll ();

    }
}
