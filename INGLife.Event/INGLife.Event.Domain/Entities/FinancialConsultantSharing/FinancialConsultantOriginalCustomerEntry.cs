using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.FinancialConsultantSharing
{
    /// <summary>
    /// FC 공유 이벤트 - 기존고객 Entry
    /// </summary>
    public class FinancialConsultantOriginalCustomerEntry : SnsSharingUser {
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
        public RetentionPeriodOriginType? RetentionPeriodOriginType { get; set; }
        public virtual string RetentionPeriodOriginTypeDisplayName
        {
            get
            {
                if (RetentionPeriodOriginType.HasValue)
                {
                    var field = RetentionPeriodOriginType.Value.GetType().GetField(RetentionPeriodOriginType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : RetentionPeriodOriginType.Value.ToString();
                }
                else
                {
                    return null;
                }
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
        /// FC 고유코드
        /// </summary>
        [Required]
        [MaxLength(15)]
        [Display(Name = "FC code")]
        public string FcCode { get; set; }
        /// <summary>
        /// 기존고객 식별 고유번호
        /// </summary>
        [Required]
        [MaxLength(15)]
        [Display(Name = "식별번호")]
        public string CustomerRandomNum { get; set; }
        /// <summary>
        /// 문자발송여부
        /// </summary>
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }

    public enum RetentionPeriodOriginType
    {
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
