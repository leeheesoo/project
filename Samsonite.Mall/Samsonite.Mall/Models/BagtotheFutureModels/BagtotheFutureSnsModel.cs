using ExpressiveAnnotations.Attributes;
using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.BagtotheFutureModels {
    public class BagtotheFutureSnsModel {
        [Display(Name = "참여자 번호")]
        public long UserId { get; set; }
        
        [Display(Name = "SNS")]
        public SnsType SnsType { get; set; }
                
        [Display(Name = "SNS 아이디")]
        public string SnsId { get; set; }
        
        [Display(Name = "SNS 이름")]
        public string SnsName { get; set; }

        [Display(Name = "SNS 스크랩")]
        public string Post { get; set; }
    }

    public class BagtotheFutureSnsUserModel {
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

        [Display(Name = "개인정보 수집 및 활용 동의")]
        [Required(ErrorMessage = "개인정보 수집 및 활용에 동의 해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 및 활용에 동의 해주세요.")]
        public bool Agree { get; set; }
    }
}