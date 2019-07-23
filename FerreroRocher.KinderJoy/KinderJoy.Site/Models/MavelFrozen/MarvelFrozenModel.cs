using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.MavelFrozen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.MavelFrozen {
    public class MarvelFrozenUserModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "나이")]
        [Required(ErrorMessage = "나이를 입력해 주세요!")]
        public int Age { get; set; }

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

        [Display(Name="자녀성별")]
        public GenderType ChildGender { get; set; }

        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
    }

    public class MarvelFrozenSnsModel {
        public long UserId { get; set; }
        public SnsType SnsType { get; set; }
        public string SnsId { get; set; }
        public string SnsName { get; set; }
        public string Post { get; set; }
    }
}