using INGLife.Event.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.KidsNote {
    public class KidsNoteEntry : SnsSharingUser {
        [Required]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "아이나이")]
        public int ChildAge { get; set; }
        [Display(Name = "아이이름")]
        public string ChildName { get; set; }
        [Display(Name = "연락방식-전화")]
        public bool? PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자메세지")]
        public bool? MessageCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool? EmailCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        public bool? PostCheck { get; set; }
        [Display(Name = "보유기간")]
        public RetentionPeriodType? RetentionPeriodType { get; set; }
        public virtual string RetentionPeriodTypeDisplayName {
            get {
                if (RetentionPeriodType.HasValue) {
                    var field = RetentionPeriodType.Value.GetType().GetField(RetentionPeriodType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : RetentionPeriodType.Value.ToString();
                } else {
                    return null;
                }
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
        [Required]
        [Display(Name = "이벤트타입")]
        public KidsNoteEventType EventType { get; set; }
        public virtual string EventTypeDisplayName {
            get {
                var field = EventType.GetType().GetField(EventType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : EventType.ToString();
            }
        }
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }
   
    public enum RetentionPeriodType {
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
    public enum KidsNoteEventType {
        [Display(Name = "1차이벤트")]
        FirstEvent = 1,
        [Display(Name = "2차이벤트")]
        SecondEvent = 2,
        [Display(Name = "3차이벤트")]
        ThirdEvent = 3,
        [Display(Name = "4차이벤트")]
        FourthEvent = 4,
    }
}
