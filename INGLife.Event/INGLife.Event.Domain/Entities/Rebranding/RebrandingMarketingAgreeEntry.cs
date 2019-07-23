using INGLife.Event.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.Rebranding {
    public class RebrandingMarketingAgreeEntry : SnsSharingUser {
        [Required]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }

        [Display(Name = "연락방식-전화")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자메세지")]
        public bool MessageCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool EmailCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        public bool PostCheck { get; set; }
        [Display(Name = "보유기간")]
        [Required]
        public RebrandingRetentionPeriodType RetentionPeriodType { get; set; }
        public virtual string RetentionPeriodTypeDisplayName {
            get {
                var field = RetentionPeriodType.GetType().GetField(RetentionPeriodType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : RetentionPeriodType.ToString();

            }
        }
        [Display(Name = "이메일")]
        public string Email { get; set; }
        [MaxLength(5)]
        [Display(Name = "우편번호")]
        public string ZipCode { get; set; }
        [Display(Name = "주소")]
        [MaxLength(255)]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [MaxLength(255)]
        public string AddressDetail { get; set; }
        [Display(Name = "추가정보 등록일")]
        public DateTime? UpdateDate { get; set; }  
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }

    public enum RebrandingRetentionPeriodType {
        [Display(Name = "1년")]
        OneYear = 1,
        [Display(Name = "2년")]
        TwoYears = 2,
        [Display(Name = "3년")]
        ThreeYears = 3,
        [Display(Name = "4년")]
        FourYears = 4,
        [Display(Name = "5년")]
        FiveYears = 5
    }
}
