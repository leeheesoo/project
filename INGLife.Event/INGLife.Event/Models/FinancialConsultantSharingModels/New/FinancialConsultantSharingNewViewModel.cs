using System;
using System.ComponentModel.DataAnnotations;

namespace INGLife.Event.Models.FinancialConsultantSharingModels.New
{
    public class FinancialConsultantSharingNewViewModel
    {
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
        [Display(Name = "연락방식-전화")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자")]
        public bool MessageCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool EmailCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        public bool PostCheck { get; set; }
        [Display(Name = "보유기간")]
        public string RetentionPeriodNewTypeDisplayName { get; set; }
        [Display(Name = "이메일")]
        public string Email { get; set; }
        [Display(Name = "주소")]
        public string AddressDisplayName { get; set; }
        [Display(Name = "재무상담사코드")]
        public string FcCode { get; set; }
        [Display(Name = "추천인이름")]
        public string ProposerName { get; set; }
        [Display(Name = "추천인고유번호")]
        public string OriginCustomerRandomNum { get; set; }
        [Display(Name = "문자발송여부")]
        public bool IsMessage { get; set; }
    }
}