using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KinderJoy.Domain.Entities.MainStream;

namespace KinderJoy.Site.Models.MainStream {
    public class MainStreamModel {
        
        /// <summary>
        /// 이름
        /// </summary>
        [Required(ErrorMessage = "이름을 10자 이내로 입력해 주세요.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "이름을 2-10자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }

        /// <summary>
        /// 나이
        /// </summary>
        [Required(ErrorMessage = "나이를 입력해 주세요.")]
        [RegularExpression("^[0-9]{2}", ErrorMessage = "나이를 정확히 입력해 주세요.(최대2자리)")]
        public int Age { get; set; }

        [Required(ErrorMessage = "연락처를 입력해 주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        /// <summary>
        /// 개인정보수집 동의
        /// </summary>
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
    }

    public class MainStreamEntryModel {
        /// <summary>
        /// 이름
        /// </summary>
        [Required(ErrorMessage = "이름을 10자 이내로 입력해 주세요.")]
        [MaxLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 나이
        /// </summary>
        [Required(ErrorMessage = "나이를 입력해 주세요.")]
        [RegularExpression("^[0-9]{2}", ErrorMessage = "나이를 정확히 입력해 주세요.(최대2자리)")]
        public int Age { get; set; }

        [Required(ErrorMessage = "연락처를 입력해 주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "아이의 성별을 골라주세요.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "우리 아이에게 필요한 서프라이즈를 선택해주세요")]
        public MainStreamSurpriseType Quiz { get; set; }
        
    }

}