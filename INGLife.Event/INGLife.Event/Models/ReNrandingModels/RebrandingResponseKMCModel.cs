using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.ReNrandingModels {
    public class RebrandingResponseKMCModel {
        public ResponseKMCModel ResponseKMCModel { get; set; }
        [Required(ErrorMessage ="이벤트 타입을 선택해주세요.")]
        public RebrandingEventType RebrandingEventType { get; set; }
    }

    public enum RebrandingEventType {
        [Display(Name = "마케팅동의")]
        MarketingAgree = 0,
        [Display(Name = "상담동의")]
        ConsultingAgree = 1
    }
}