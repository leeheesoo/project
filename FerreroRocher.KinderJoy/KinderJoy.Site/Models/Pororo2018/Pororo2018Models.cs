using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Entities.Pororo2018;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Site.Models.Pororo2018 {
    public class Pororo2018Model {
        [Required(ErrorMessage ="경품에 당첨되지 않았습니다.")]
        public long Id { get; set; }
        [Required(ErrorMessage = "경품에 당첨되지 않았습니다.")]
        public Pororo2018InstantLotteryPrizeType PrizeType { get; set; }
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 입력 해주세요.")]
        [MaxLength(10, ErrorMessage = "이름을 10자 이내로 입력해주세요.")]
        [RegularExpression("[a-zA-Z가-힣]+$", ErrorMessage = "이름은 한글 또는 영문으로만 입력 가능합니다.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "연락처를 입력 해주세요.")]
        [MaxLength(11, ErrorMessage = "연락처를 11자 이내로 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }
        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
        [Display(Name = "우편번호")]
        [RequiredIf("PrizeType != 1", ErrorMessage = "주소를 입력해주세요")]
        public string ZipCode { get; set; }
        [Display(Name = "기본주소")]
        [RequiredIf("PrizeType != 1", ErrorMessage = "주소를 입력해주세요")]
        [StringLength(200, ErrorMessage = "주소를 200자 이내로 입력해 주세요.")]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [RequiredIf("PrizeType != 1", ErrorMessage = "주소를 입력해주세요")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요.")]
        public string AddressDetail { get; set; }
    }

    public class Pororo2018InstantLotteryViewModel {
        public long Id { get; set; }
        public Pororo2018InstantLotteryPrizeType PrizeType { get; set; }
        public string PrizeName { get; set; }
    }
}
