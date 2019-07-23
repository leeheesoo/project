using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finnq.Promotion.Models.TmapEventModels {
    public class TmapEventEntryModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]
        [MaxLength(50, ErrorMessage = "이름을 50자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요." )]
        [MinLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)$", ErrorMessage = "휴대폰을 정확히 입력해주세요. ex) 010-1234-5678")]
        public string PhoneA { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [MinLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        public string PhoneB { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [MinLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        public string PhoneC { get; set; }

        [Display(Name = "휴대폰")]
        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)-[0-9]{3,4}-[0-9]{4}$", ErrorMessage = "휴대폰을 정확히 입력해주세요. ex) 010-1234-5678")]
        public string Phone {
            get {
                return string.Format("{0}-{1}-{2}", this.PhoneA, this.PhoneB, this.PhoneC);
            }
        }

        [Display(Name = "개인정보 수집 이용 동의 안내")]
        [Required(ErrorMessage = "개인정보 수집 이용 동의 안내에 동의 해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 이용 동의 안내에 동의 해주세요.")]
        public bool Agree { get; set; }

        [Display(Name = "개인정보 취급 위탁 동의 안내")]
        [Required(ErrorMessage = "개인정보 취급 위탁 동의 안내에 동의 해주세요.")]
        [AssertThat("Agree2 == true", ErrorMessage = "개인정보 취급 위탁 동의 안내에 동의 해주세요.")]
        public bool Agree2 { get; set; }
    }
}