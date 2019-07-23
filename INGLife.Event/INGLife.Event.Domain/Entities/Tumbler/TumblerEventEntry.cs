using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.TumblerEntry
{
    /// <summary>
    /// 텀블러이벤트
    /// </summary>
    public class TumblerEventEntry : SnsSharingUser {
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
        [Display(Name = "관심분야 - 건강관리")]
        public bool? HealthCheck { get; set; }
        /// <summary>
        /// 관심분야 - 월급관리
        /// </summary>
        [Display(Name = "관심분야 - 월급관리")]
        public bool? SalaryCheck { get; set; }
        /// <summary>
        /// 관심분야 - 저축연금
        /// </summary>
        [Display(Name = "관심분야 - 저축연금")]
        public bool? SavingCheck { get; set; }
        /// <summary>
        /// 관심분야 - 예비자금준비
        /// </summary>
        [Display(Name = "관심분야 - 예비자금준비")]
        public bool? FundCheck { get; set; }
        /// <summary>
        /// 관심분야 - 기타
        /// </summary>
        [Display(Name = "관심분야 - 기타")]
        public bool? EtcCheck { get; set; }
        [Display(Name = "이벤트 타입")]
        public string EventType { get; set; }
        /// <summary>
        /// 문자발송여부
        /// </summary>
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }

        [Display(Name = "응모완료일시")]
        public DateTime? CompleteDate { get; set; }
    }
}
