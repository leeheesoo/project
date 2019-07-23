using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.FinancialConcertMarketingAgreeModels {
    public class FinancialConcertMarketingAgreeAdminViewModel {
        [Display(Name = "본인인증일시")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "채널")]
        public string ChannelDisplayName { get; set; }
        [Display(Name = "IP")]
        public string IpAddress { get; set; }
        [Display(Name = "이름")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        public string Mobile { get; set; }
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "신청 회차")]
        public string ApplicationTurnDisplayName { get; set; }
        [Display(Name = "연락방식-전화")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자")]
        public bool MessageCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool EmailCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        public bool PostCheck { get; set; }
        [Display(Name = "보유기간")]
        public string RetentionPeriodTypeDisplayName { get; set; }
        [Display(Name = "이메일")]
        public string Email { get; set; }
        [Display(Name = "주소")]
        public string AddressDisplayName { get; set; }
        [Display(Name = "응모완료일시")]
        public DateTime CompleteDate { get; set; }
    }
}