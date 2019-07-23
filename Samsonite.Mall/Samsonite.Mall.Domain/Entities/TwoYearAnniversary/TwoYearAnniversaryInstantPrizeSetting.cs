using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.TwoYearAnniversary {
    /// <summary>
    /// 쌤소나이트 2주년
    /// </summary>
    public enum TwoYearAnniversarInstantPrizeType
    {        
        [Display(Name = "1만원 쿠폰")]
        Loser = 0,        
        [Display(Name = "스타벅스 기프티콘")]        
        StarBucks = 1,
        [Display(Name = "키즈백팩")]
        AtKidsBagPack = 2,
        [Display(Name = "쌤소나이트 정품가방")]
        OriginalBag = 3,        
        [Display(Name = "20만원 쿠폰")]
        Coupon_20 = 4,        
        [Display(Name = "라인프렌즈 콜라보 캐리어")]
        LineFriendsCarrier = 5
    }
    public class TwoYearAnniversaryInstantPrizeSetting : InstantLotteryPrizeSetting<TwoYearAnniversarInstantPrizeType>
    {        
    }   
}
