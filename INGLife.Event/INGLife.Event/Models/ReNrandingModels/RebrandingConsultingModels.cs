using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.ReNrandingModels {
    public class RebrandingConsultingModels {
        public RebrandingConsultingModel RebrandingConsultingModel { get; set; }
        public RequestKMCModel RequestConsultingKMCModel { get; set; }
    }

    public class RebrandingConsultingModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "휴대폰 인증을 받아주세요.")]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Required(ErrorMessage = "관심분야를 선택해주세요.")]
        [Display(Name = "관심분야")]
        public RebrandingAttentionType AttetionTypoe { get; set; }
        [Display(Name = "우편번호")]
        [Required(ErrorMessage = "주소를 입력해주세요.")]
        public string ZipCode { get; set; }
        [Display(Name = "기본주소")]
        [Required(ErrorMessage = "주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "주소를 200자 이내로 입력해 주세요.")]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [Required(ErrorMessage = "나머지 주소를 입력해주세요.")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요.")]
        public string AddressDetail { get; set; }

        [Display(Name = "개인(신용)정보 수집·이용")]
        [Required(ErrorMessage = "개인(신용)정보 수집·이용에 동의 시에만 이벤트 참여가 가능합니다.")]
        [AssertThat("Agree == 1", ErrorMessage = "개인(신용)정보 수집·이용에 동의 시에만 이벤트 참여가 가능합니다.")]
        public int Agree { get; set; }
        [Display(Name = "개인(신용)정보 제공")]
        [Required(ErrorMessage = "개인(신용)정보 제공에 동의 시에만 이벤트 참여가 가능합니다.")]
        [AssertThat("Agree2 == 1", ErrorMessage = "개인(신용)정보 제공에 동의 시에만 이벤트 참여가 가능합니다.")]
        public int Agree2 { get; set; }
    }
}