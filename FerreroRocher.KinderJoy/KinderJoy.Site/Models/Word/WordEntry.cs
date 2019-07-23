using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Word
{
    public class WordEntry
    {
        [Required(ErrorMessage = "이름을 입력하세요")]
        [MaxLength(20, ErrorMessage = "길이가 초과 되었습니다.")]
        [RegularExpression("[a-zA-Zㄱ-ㅎㅏ-ㅣ가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "성별을 선택 하세요")]
        [AssertThat("Genger == 'M' || Genger == 'F'", ErrorMessage = "성별을 선택 하세요")]
        public string Genger { get; set; }

        [Required(ErrorMessage = "나이를 입력 하세요")]
        [Range(1, 100, ErrorMessage = "나이는 1살이상 100살 이하로 입력 하세요")]
        public int Age { get; set; }

        [Required(ErrorMessage = "올바른 휴대폰 번호를 입력해 주세요")]
        [MaxLength(11, ErrorMessage = "올바른 휴대폰 번호를 입력해 주세요")]
        [RegularExpression("^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})", ErrorMessage = "올바른 휴대폰 번호를 입력해 주세요")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 및 이용에 동의해주세요.")]
        public bool Agree { get; set; }

        [Required(ErrorMessage = "경품을 선택 하세요")]
        [AssertThat("GiftType == 'M' || GiftType == 'F'", ErrorMessage = "경품을 선택 하세요")]
        public string GiftType { get; set; }

        [Required]
        public string Channel { get; set; }
    }
}