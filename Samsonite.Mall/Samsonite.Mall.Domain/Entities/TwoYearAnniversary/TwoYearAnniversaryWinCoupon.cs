using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Samsonite.Mall.Domain.Entities.TwoYearAnniversary
{
    public class TwoYearAnniversaryWinCoupon
    {
        [Column(Order = 0)]
        [Display(Name = "쿠폰유형")]
        public CouponType CouponType { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "쿠폰코드")]
        public string CouponCode { get; set; }        

        [ForeignKey("TwoYearAnniversaryEntry")]
        [Display(Name = "참여자 번호")]
        public long? TwoYearAnniversaryEntryId { get; set; }
        public virtual TwoYearAnniversaryEntry TwoYearAnniversaryEntry { get; set; }

        [Display(Name = "당첨날짜")]
        public DateTime? WinnerDate { get; set; }        
    }

    public enum CouponType
    {
        [Display(Name = "1만원 쿠폰")]
        Loser = 0,
        [Display(Name = "20만원 쿠폰")]
        Coupon_20 = 4,
    }
}