using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Identity;

namespace KinderJoy.Site.Models.Admin {
        /// <summary>
        /// 관리자 계정 정보 수정 모델
        /// </summary>
    public class AdminUserEditModel {
            public long AdminId { get; set; }
            public string UserName { get; set; }
            [Required(ErrorMessage = "이메일주소를 입력해주세요.")]
            [EmailAddress(ErrorMessage = "이메일주소를 정확히 입력해주세요.")]
            public string Email { get; set; }
            /// <summary>
            /// 비밀번호 변경 여부
            /// </summary>
            public bool WillChangePassword { get; set; }
            [RequiredIf("WillChangePassword == true", ErrorMessage = "변경할 비밀번호를 입력해주세요.")]
            public string Password { get; set; }
            [Required(ErrorMessage = "전화번호를 정확하게 입력해주세요.")]
            [RegularExpression(@"^[0-9]{8,13}$", ErrorMessage = "전화번호를 숫자로만 8자 이상 13이내로 입력해주세요.")]
            public string PhoneNumber { get; set; }

            public ICollection<AppRole> Roles { get; set; }
    }
}