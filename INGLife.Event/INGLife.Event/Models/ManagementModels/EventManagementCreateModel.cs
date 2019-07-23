using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.ManagementModels {
    public class EventManagementCreateModel {

        [Required(ErrorMessage = "제휴사를 선택해주세요.")]
        public long AffiliatesId { get; set; }
        [Required(ErrorMessage = "이벤트명을 입력해주세요.")]
        [MaxLength(50, ErrorMessage = "이벤트명을 50자 이내로 입력해주세요.")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Page Path를 입력해주세요.")]        
        [MaxLength(100, ErrorMessage = "Page Path를 100자 이내로 입력해주세요.")]
        //[RegularExpression("(/+[a-zA-Z0-9-])+|/$", ErrorMessage = "Page Path는 /로 시작하는 영문 및 숫자로만 입력 가능합니다.")]
        public string PagePath { get; set; }
    }
}