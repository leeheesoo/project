using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree {
    /// <summary>
    /// 재무콘서트 마케팅동의
    /// </summary>
    public class FinancialConcertMarketingAgreeEntry: SnsSharingUser {
        /// <summary>
        /// 성별
        /// </summary>
        [Required]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        /// <summary>
        /// 생년월일
        /// </summary>
        [Required]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        /// <summary>
        /// 연락방식-전화
        /// </summary>
        [Display(Name = "연락방식-전화")]
        public bool? PhoneCheck { get; set; }
        /// <summary>
        /// 연락방식-문자메세지
        /// </summary>
        [Display(Name = "연락방식-문자메세지")]
        public bool? MessageCheck { get; set; }
        /// <summary>
        /// 연락방식-우편
        /// </summary>
        [Display(Name = "연락방식-우편")]
        public bool? PostCheck { get; set; }
        /// <summary>
        /// 연락방식-이메일
        /// </summary>
        [Display(Name = "연락방식-이메일")]
        public bool? EmailCheck { get; set; }
        /// <summary>
        /// 보유.이용기간
        /// </summary>
        [Display(Name = "보유.이용기간")]
        public int RetentionPeriodType { get; set; }
        public virtual string RetentionPeriodTypeDisplayName {
            get {
                return string.Format("{0}년", this.RetentionPeriodType);
            }
        }
        /// <summary>
        /// 이메일
        /// </summary>
        [Display(Name = "이메일")]
        public string Email { get; set; }
        /// <summary>
        /// 우편번호
        /// </summary>
        [MaxLength(5)]
        [Display(Name = "우편번호")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 주소
        /// </summary>
        [Display(Name = "주소")]
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 상세주소
        /// </summary>
        [Display(Name = "상세주소")]
        [MaxLength(255)]
        public string AddressDetail { get; set; }
        public virtual string AddressDisplayName {
            get {
                return (string.IsNullOrEmpty(ZipCode) ? "" : string.Format("({0}) {1} {2}", ZipCode, Address, AddressDetail));
            }
        }
        /// <summary>
        /// 신청 회차
        /// </summary>
        [Display(Name = "신청 회차")]
        public FinancialConcertMarketingAgreeApplicationTurnEnum ApplicationTurn { get; set; }
        public virtual string ApplicationTurnDisplayName {
            get {
                if(ApplicationTurn ==0) {
                    return "";
                }
                var field = ApplicationTurn.GetType().GetField(ApplicationTurn.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : ApplicationTurn.ToString();
            }
        }
        /// <summary>
        /// 문자발송여부
        /// </summary>
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }

        [Display(Name = "응모완료일시")]
        public DateTime? CompleteDate { get; set; }
    }
}
