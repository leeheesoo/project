using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.KidsNoteModels {
    public class KidsNoteModels {
        public KidsNoteModel KidsNoteModel { get; set; }
        public RequestKMCModel RequestKMCModel { get; set; }
    }
    public class KidsNoteModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        
        [Display(Name = "자녀이름")]
        //[DefaultValue("EMPTHY")]
        //[Required(ErrorMessage = "자녀이름을 입력 해주세요.")]
        [MaxLength(10, ErrorMessage = "이름을 10자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string ChildName { get; set; }

        [Display(Name = "자녀나이")]
        //[DefaultValue(0)]
        //[Required(ErrorMessage = "자녀나이를 입력 해주세요.")]
        [RegularExpression("^[0-9]{1,2}$", ErrorMessage = "자녀나이를 정확히 입력 해주세요.")]
        public int? ChildAge { get; set; }
        [AssertThat("ContactCheck == true", ErrorMessage = "연락방식을 선택해주세요.")]
        [Display(Name = "연락방식")]
        public virtual bool ContactCheck {
            get {
                return this.PhoneCheck || this.MessageCheck || this.PostCheck || this.EmailCheck;
            }
        }
        [Display(Name = "연락방식-전체")]
        public bool AllCheck { get; set; }
        [Display(Name = "연락방식-전화")]
        [AssertThat("PhoneCheck == true", ErrorMessage = "전화 수신 동의를 하셔야\n이벤트 참여가 가능합니다.")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자메세지")]
        //[AssertThat("MessageCheck == true", ErrorMessage = "전화, 문자 수신 동의를 하셔야\n이벤트 참여가 가능합니다.")]
        public bool MessageCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        public bool PostCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool EmailCheck { get; set; }
        [Display(Name = "이용기간")]
        [Required(ErrorMessage = "개인정보 보유·이용기간을 선택해주세요.")]
        public RetentionPeriodType RetentionPeriodType { get; set; }
        [Display(Name = "이메일1")]
        [RegularExpression("^[0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*$", ErrorMessage = "이메일을 정확히 입력해 주세요.")]
        [MaxLength(25)]
        public string Email1 { get; set; }
        [Display(Name = "이메일2")]
        [RegularExpression(@"^[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*\.[a-zA-Z]{2,4}$", ErrorMessage = "이메일을 정확히 입력해 주세요.")]
        [MaxLength(25)]
        public string Email2 { get; set; }
        [Display(Name = "이메일requied체크")]
        [AssertThat("EmailRequiredCheck == true", ErrorMessage = "이메일을 입력해주세요.")]
        public virtual bool EmailRequiredCheck {
            get {
                return !(this.EmailCheck && (string.IsNullOrEmpty(this.Email1) || string.IsNullOrEmpty(this.Email2)));
            }
        }
        [Display(Name = "우편번호")]
        [Required(ErrorMessage ="주소를 입력해주세요.")]
        public string ZipCode { get; set; }
        [Display(Name = "기본주소")]
        [Required(ErrorMessage = "주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "주소를 200자 이내로 입력해 주세요.")]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [Required(ErrorMessage = "주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요.")]
        public string AddressDetail { get; set; }
        [Display(Name = "개인(신용)정보 수집·이용·제공")]
        [Required(ErrorMessage = "개인(신용)정보 수집·이용·제공에 동의하지 않으시면 이벤트 참여가 불가합니다.")]
        [AssertThat("Agree == 1", ErrorMessage = "개인(신용)정보 수집·이용·제공에 동의하지 않으시면 이벤트 참여가 불가합니다.")]
        public int Agree { get; set; }
        //[Required(ErrorMessage = "개인정보의 제공에 동의 해주세요.")]
        //[AssertThat("Agree2 == true", ErrorMessage = "개인정보의 제공에 동의 해주세요.")]
        public bool Agree2 { get; set; }
    }
}