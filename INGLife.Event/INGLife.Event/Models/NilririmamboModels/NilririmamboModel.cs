using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.NilririmamboModel {
    public class NilririmamboModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "연락처를 입력해주세요")]
        [MaxLength(11, ErrorMessage = "연락처를 11자 이내로 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "인스타그램 계정을 입력해주세요.")]
        [MaxLength(30, ErrorMessage = "인스타그램 계정을 30자 이내로 입력해주세요.")]
        [Display(Name = "인스타그램ID")]
        public string InstagramId { get; set; }
        [Required(ErrorMessage = "FC 코드를 입력해주세요.")]
        [MinLength(5, ErrorMessage = "FC 코드를 확인해주세요. ")]
        [Display(Name = "fc 코드")]
        public string FcCode { get; set; }
        [Display(Name = "개인(신용)정보 수집·이용·제공")]
        [Required(ErrorMessage = "개인정보 수집 및 이용에 대한 동의사항을 확인해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 및 이용에 대한 동의사항을 확인해주세요.")]
        public bool Agree { get; set; }
    }
}