using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Models.ChildrensDay {
    public class ChildrensDayHiddenPictureModel {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        /// <summary>
        /// 장난감 성별
        /// </summary>
        [HiddenInput(DisplayValue =false)]
        [MaxLength(3)]
        public string Gender { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        [Required(ErrorMessage = "이름을 20자 이내로 입력해 주세요.")]
        [MaxLength(20)]
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
}