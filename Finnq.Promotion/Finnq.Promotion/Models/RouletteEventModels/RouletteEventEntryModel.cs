using ExpressiveAnnotations.Attributes;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finnq.Promotion.Models.RouletteEventModels {
    public class RouletteEventEntryModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]
        [MaxLength(50, ErrorMessage = "이름을 50자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [MinLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)$", ErrorMessage = "휴대폰을 정확히 입력해주세요. ex) 010-1234-5678")]
        public string PhoneA { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [MinLength(3, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        public string PhoneB { get; set; }

        [Required(ErrorMessage = "휴대폰을 입력해주세요.")]
        [MinLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        [MaxLength(4, ErrorMessage = "휴대폰을 정확히 입력해주세요.")]
        public string PhoneC { get; set; }

        [Display(Name = "개인정보 수집 이용 동의 안내")]
        [Required(ErrorMessage = "개인정보 수집 이용 동의 안내에 동의 해주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보 수집 이용 동의 안내에 동의 해주세요.")]
        public bool Agree { get; set; }

        [Display(Name = "개인정보 처리 위탁 동의 안내")]
        public bool Agree2 { get; set; }

        [Display(Name = "나이정보 동의사항")]
        [Required(ErrorMessage = "나이정보 동의사항에 동의 해주세요.")]
        [AssertThat("Agree3 == true", ErrorMessage = "나이정보 동의사항에 동의 해주세요.")]
        public bool Agree3 { get; set; }
    }

    public class UpdateRouletteEventEntryModel {
        public long id { get; set; }
    }

    public class RouletteEventInstantLotteryViewModel{
        public long id { get; set; }
        public string PrizeName { get; set; }
        public RouletteEventInstantLotteryPrizeType PrizeType { get; set; }
    }

}