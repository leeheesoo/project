using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Samsonite.Mall.Models.OneYearAnniversaryModels {
    public class OneYearAnniversaryModel {
        public OneYearAnniversaryCreateModel OneYearAnniversaryCreateModel { get; set; }
        public OneYearAnniversaryUpdateModel OneYearAnniversaryUpdateModel { get; set; }

    }
    /// <summary>
    /// 오행시 댓글등록 이벤트 생성모델
    /// </summary>
    public class OneYearAnniversaryCreateModel {

        [Display(Name = "쌤소나이트 오행시: 쌤")]
        [Required(ErrorMessage = "쌤소나이트 오행시를 등록해주세요.")]
        [MaxLength(30, ErrorMessage = "쌤소나이트 오행시를 각 30자 이내로 입력해주세요.")]
        public string AcrosticPoemFirst { get; set; }
        [Display(Name = "쌤소나이트 오행시: 소")]
        [Required(ErrorMessage = "쌤소나이트 오행시를 등록해주세요.")]
        [MaxLength(30, ErrorMessage = "쌤소나이트 오행시를 각 30자 이내로 입력해주세요.")]
        public string AcrosticPoemSecond { get; set; }
        [Display(Name = "쌤소나이트 오행시: 나")]
        [Required(ErrorMessage = "쌤소나이트 오행시를 등록해주세요.")]
        [MaxLength(30, ErrorMessage = "쌤소나이트 오행시를 각 30자 이내로 입력해주세요.")]
        public string AcrosticPoemThird { get; set; }
        [Display(Name = "쌤소나이트 오행시: 이")]
        [Required(ErrorMessage = "쌤소나이트 오행시를 등록해주세요.")]
        [MaxLength(30, ErrorMessage = "쌤소나이트 오행시를 각 30자 이내로 입력해주세요.")]
        public string AcrosticPoemFourth { get; set; }
        [Display(Name = "쌤소나이트 오행시: 트")]
        [Required(ErrorMessage = "쌤소나이트 오행시를 등록해주세요.")]
        [MaxLength(30, ErrorMessage = "쌤소나이트 오행시를 각 30자 이내로 입력해주세요.")]
        public string AcrosticPoemFifth { get; set; }
        [Display(Name = "축하메시지")]
        [Required(ErrorMessage = "축하메시지를 등록해주세요.")]
        [MaxLength(100, ErrorMessage = "축하메시지를 100자 이내로 입력해주세요.")]
        public string CongratulationMessage { get; set; }
    }

    /// <summary>
    /// 오행시 댓글등록 이벤트 변경모델
    /// </summary>
    public class OneYearAnniversaryUpdateModel {
        [Display(Name = "아이디")]
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }


        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]
        [MaxLength(50, ErrorMessage = "이름을 50자 이내로 입력 해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }

        [Display(Name = "쌤소나이트 아이디")]
        [Required(ErrorMessage = "쌤소나이트 아이디를 입력 해주세요.")]
        [MaxLength(15, ErrorMessage = "쌤소나이트 아이디를 15자 이내로 입력 해주세요.")]
        [RegularExpression(@"^[0-9a-zA-z]{6,20}$", ErrorMessage = "아이디를 영/숫자 6~15자로 입력 해주세요.")]
        public string SamsoniteId { get; set; }

        [Display(Name = "연락처")]
        [Required(ErrorMessage = "연락처를 입력 해주세요.")]
        [MaxLength(11, ErrorMessage = "연락처를 11자 이내로 입력해 주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }
        [Display(Name = "개인정보 수집 및 이용 동의")]
        [Required(ErrorMessage = "개인정보 수집 및 이용 동의 해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 및 이용에 동의 해주세요.")]
        public bool Agree { get; set; }
    }
}