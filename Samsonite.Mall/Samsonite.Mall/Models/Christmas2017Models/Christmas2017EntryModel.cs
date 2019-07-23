using ExpressiveAnnotations.Attributes;
using Samsonite.Mall.Domain.Entities.Christmas2017;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.Christmas2017Models {
    public class Christmas2017EntryModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]
        [MaxLength(20, ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }
        [Display(Name = "공식몰ID")]
        [Required(ErrorMessage = "공식몰ID를 입력 해주세요.")]
        [MaxLength(50, ErrorMessage = "공식몰ID를 50자 이내로 입력해주세요.")]
        [RegularExpression(@"^[0-9a-zA-z]{1,50}$", ErrorMessage = "공식몰ID를 영/숫자 50자 이내로 입력 해주세요.")]
        public string SamsoniteMallId { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "연락처를 입력 해주세요.")]
        [MaxLength(11, ErrorMessage = "연락처를 11자 이내로 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }
        [Display(Name = "크리스마스가방")]
        [Required(ErrorMessage = "크리스마스 가방을 다시 선택해주세요.")]
        public ChristmasBagType ChristmasBagType { get; set; }
        [Display(Name = "개인정보 수집 및 취급 위탁 동의")]
        [Required(ErrorMessage = "개인정보 수집 및 취급 위탁에 동의 해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 및 취급 위탁에 동의 해주세요.")]
        public bool Agree { get; set; }
    }
}