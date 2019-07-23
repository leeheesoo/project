using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Models.FinancialConsultantSharingModels.New
{
    public class FinancialConsultantSharingNewCreateModel
    {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "휴대폰 인증을 진행해주세요.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "휴대폰 인증을 진행해주세요.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 진행해주세요.")]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 진행해주세요.")]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "이용기간")]
        [Required(ErrorMessage = "개인정보 보유·이용기간을 선택해주세요.")]
        public RetentionPeriodOriginType? RetentionPeriodNewType { get; set; }
        //[AssertThat("ContactCheck == true", ErrorMessage = "전화와 문자메시지 수신 동의와 주소 입력을 완료 하셔야 이벤트 참여가 가능합니다.")]
        //[Display(Name = "연락방식")]
        //public virtual bool ContactCheck
        //{
        //    get
        //    {
        //        return this.PhoneCheck || this.MessageCheck || this.PostCheck;
        //    }
        //}
        [Display(Name = "연락방식-전체")]
        public bool AllCheck { get; set; }
        [Display(Name = "연락방식-전화")]
        [AssertThat("PhoneCheck == true", ErrorMessage = "전화와 문자, 우편 수신동의를 하셔야 이벤트 참여가 가능합니다.")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자메세지")]
        [AssertThat("MessageCheck == true", ErrorMessage = "전화와 문자, 우편 수신동의를 하셔야 이벤트 참여가 가능합니다.")]
        public bool MessageCheck { get; set; }
        [Display(Name = "연락방식-우편")]
        [AssertThat("PostCheck == true", ErrorMessage = "전화와 문자, 우편 수신동의를 하셔야 이벤트 참여가 가능합니다.")]
        public bool PostCheck { get; set; }
        [Display(Name = "연락방식-이메일")]
        public bool EmailCheck { get; set; }
        [Display(Name = "이메일1")]
        [RequiredIf("EmailCheck == true", ErrorMessage = "이메일을 입력해 주세요.")]
        [RegularExpression("^[0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*$", ErrorMessage = "이메일을 정확히 입력해 주세요.")]
        [MaxLength(25)]
        public string Email1 { get; set; }
        [Display(Name = "이메일2")]
        [RequiredIf("EmailCheck == true", ErrorMessage = "이메일을 입력해 주세요.")]
        [RegularExpression(@"^[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*\.[a-zA-Z]{2,4}$", ErrorMessage = "이메일을 정확히 입력해 주세요.")]
        [MaxLength(25)]
        public string Email2 { get; set; }
        [Display(Name = "이메일requied체크")]
        [AssertThat("EmailRequiredCheck == true", ErrorMessage = "이메일을 입력해주세요.")]
        public virtual bool EmailRequiredCheck
        {
            get
            {
                return !(this.EmailCheck && (string.IsNullOrEmpty(this.Email1) || string.IsNullOrEmpty(this.Email2)));
            }
        }
        [Display(Name = "우편번호")]
        [RequiredIf("PostCheck == true", ErrorMessage = "주소를 입력해주세요.")]
        public string ZipCode { get; set; }
        [Display(Name = "기본주소")]
        [RequiredIf("PostCheck == true", ErrorMessage = "주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "주소를 200자 이내로 입력해 주세요.")]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [RequiredIf("PostCheck == true", ErrorMessage = "나머지주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요.")]
        public string AddressDetail { get; set; }
        [Display(Name = "개인(신용)정보 수집·이용")]
        [Required(ErrorMessage = "개인(신용)정보 수집·이용에 동의 시에만 이벤트 참여가 가능합니다.")]
        [AssertThat("Agree == 1", ErrorMessage = "개인(신용)정보 수집·이용에 동의 시에만 이벤트 참여가 가능합니다.")]
        public int Agree { get; set; }
        [Display(Name = "개인(신용)정보 제공")]
        [Required(ErrorMessage = "개인(신용)정보 제공에 동의 시에만 이벤트 참여가 가능합니다.")]
        [AssertThat("Agree2 == 1", ErrorMessage = "개인(신용)정보 제공에 동의 시에만 이벤트 참여가 가능합니다.")]
        public int Agree2 { get; set; }
        [Display(Name = "재무상담사 코드")]
        [Required(ErrorMessage = "카카오톡 공유 메세지로 접속해주세요.[0]")]
        public string FcCode { get; set; }
        [Display(Name = "추천인 이름")]
        [Required(ErrorMessage = "카카오톡 공유 메세지로 접속해주세요.[1]")]
        public string ProposerName { get; set; }
    }
}