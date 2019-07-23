using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using System.ComponentModel.DataAnnotations;

namespace KinderJoy.Site.Models.NinjaBarbie2016 {
    public class NinjaBarbie2016UserViewModel {
        [Display(Name = "서프라이즈")]
        [Required(ErrorMessage = "새로고침 후 다시 참여해주세요.")]
        public NinjaBarbieSurprizeType SurprizeType { get; set; }

        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "휴대폰 번호")]
        [Required(ErrorMessage = "휴대폰 번호를 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "휴대폰 번호를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        [Display(Name = "우편번호")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string ZipCode { get; set; }

        [Display(Name = "주소")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string Address { get; set; }

        [Display(Name = "상세주소")]
        [Required(ErrorMessage = "상세주소를 입력해 주세요!")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요!")]
        public string AddressDetail { get; set; }

        [Display(Name = "나이")]
        [Required(ErrorMessage = "나이를 입력해 주세요!")]
        public int Age { get; set; }

        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
    }

    public class NinjaBarbie2016CreateCanvasImagingModel {
        public string Image { get; set; }
    }

    public class NinjaBarbie2016SurprizeViewModel {
        [Display(Name = "번호")]
        public long UserId { get; set; }
        [Display(Name = "서프라이즈")]
        public NinjaBarbieSurprizeType SurprizeType { get; set; }

        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Accessory { get; set; }
        [Display(Name = "페이스북 합성이미지 Url")]
        public string FacebookImage { get; set; }
        [Display(Name = "카카오톡 합성이미지 Url")]
        public string KakaotalkImage { get; set; }
        [Display(Name = "카카오스토리 합성이미지 Url")]
        public string KakaostoryImage { get; set; }
    }

    public class NinjaBarbie2016SharingViewModel {
        [Display(Name = "번호")]
        public long UserId { get; set; }
        [Display(Name = "페이스북 합성이미지 Url")]
        public string FacebookImage { get; set; }
        [Display(Name = "카카오톡 합성이미지 Url")]
        public string KakaotalkImage { get; set; }
        [Display(Name = "카카오스토리 합성이미지 Url")]
        public string KakaostoryImage { get; set; }

        public SnsType SnsType { get; set; }
        public string SnsId { get; set; }
        public string SnsName { get; set; }
        public string Post { get; set; }
    }
}