using ExpressiveAnnotations.Attributes;
using Samsonite.Mall.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.BagtotheFutureModels {
    public class BagtotheFutureModel {
        public BagtotheFutureEntryModel BagtotheFutureEntryModel { get; set; }
        public BagtotheFutureEntryCheckModel BagtotheFutureEntryCheckModel { get; set; }
    }

    public class BagtotheFutureEntryCheckModel {
        [Display(Name = "아이디어명")]
        [Required(ErrorMessage = "아이디어명을 입력해주세요.")]
        [MaxLength(50, ErrorMessage = "아이디어명을 50자 이내로 입력해주세요.")]
        public string IdeaName { get; set; }

        [Display(Name = "아이디어설명")]
        [Required(ErrorMessage = "아이디어 설명을 입력해주세요.")]
        [MaxLength(1000, ErrorMessage = "아이디어 설명을 1000자 이내로 입력해주세요.")]
        public string IdeaDescription { get; set; }

        [Display(Name = "첨부파일")]
        [ValidationFile(Required = false, ContentLength = 31457280, ErrorMessage = "30mb 이내 업로드가 가능합니다!")]
        public HttpPostedFileBase Attachment { get; set; }
    }
    public class BagtotheFutureEntryModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]        
        [MaxLength(50, ErrorMessage = "이름을 50자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }
        
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "연락처를 입력 해주세요.")]
        [MaxLength(11, ErrorMessage = "연락처를 11자 이내로 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        [Display(Name = "이메일")]
        [Required(ErrorMessage = "이메일주소를 입력 해주세요.")]
        [MaxLength(100, ErrorMessage = "이메일주소를 100자 이내로 입력해주세요.")]
        [RegularExpression("^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$", ErrorMessage = "이메일주소를 정확하게 입력해주세요. ex) abc@naver.com")]
        public string Email { get; set; }

        [Display(Name = "개인정보 수집 및 활용 동의")]
        [Required(ErrorMessage = "개인정보 수집 및 활용에 동의 해주세요.")]
        [AssertThat("EntryAgree == true", ErrorMessage = "개인정보 수집 및 활용에 동의 해주세요.")]
        public bool EntryAgree { get; set; }

        [Display(Name = "수상작 선정 쌤소나이트코리아(주)의 내부심사에 의해 결정되는 사항에 동의")]
        [Required(ErrorMessage = "수상작 선정은 쌤소나이트코리아(주)의 내부심사에 의해 결정되는 사항에 동의 해주세요.")]
        [AssertThat("EntryAgree2 == true", ErrorMessage = "수상작 선정은 쌤소나이트코리아(주)의 내부심사에 의해 결정되는 사항에 동의 해주세요.")]
        public bool EntryAgree2 { get; set; }

        public string IdeaName { get; set; }
        public string IdeaDescription { get; set; }
        public string File { get; set; }
    }
}