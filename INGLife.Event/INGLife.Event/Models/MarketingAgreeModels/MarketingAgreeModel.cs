using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.MarketingAgreeModels {

    public class MarketingAgreeModels {
        public MarketingAgreeModel MarketingAgreeModel { get; set; }
        public RequestKMCModel RequestKMCModel { get; set; }
    }

    public class MarketingAgreeModel {
        [Display(Name = "이름")]
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }

        [Display(Name = "연락방식-전체")]
        public bool AllCheck { get; set; }
        [Display(Name = "연락방식-전화")]
        public bool PhoneCheck { get; set; }
        [Display(Name = "연락방식-문자메세지")]
        public bool MessageCheck { get; set; }

        [Display(Name = "이용기간")]
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        public RetentionPeriodType RetentionPeriodType { get; set; }


        [Display(Name = "개인(신용)정보 수집·이용")]
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        [AssertThat("Agree == 1", ErrorMessage = "개인(신용)정보 수집·이용·제공에 동의하지 않으시면 이벤트 참여가 불가합니다.")]
        public int Agree { get; set; }
        [Display(Name = "개인(신용)정보 제공")]
        [Required(ErrorMessage = "이벤트가 종료되었습니다.")]
        [AssertThat("Agree2 == 1", ErrorMessage = "개인(신용)정보 제공에 동의하지 않으시면 이벤트 참여가 불가합니다.")]
        public int Agree2 { get; set; }
    }
}