using ExpressiveAnnotations.Attributes;
using Samsonite.Mall.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samsonite.Mall.Models.AdminModels {
    public class AdminUserEditModel {
        public long AdminId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 비밀번호 변경 여부
        /// </summary>
        public bool WillChangePassword { get; set; }
        [RequiredIf("WillChangePassword == true", ErrorMessage = "변경할 비밀번호를 입력해주세요.")]
        public string Password { get; set; }

        public ICollection<AppRole> Roles { get; set; }
    }
    public class AdminUserCreateModel {
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "패스워드를 입력해주세요.")]
        public string Password { get; set; }
        [HiddenInput]
        public ICollection<AppRole> Roles { get; set; }

        [AssertThat("SelectRoles != null", ErrorMessage = "역할을 1개 이상 선택해주세요.")]
        public List<string> SelectRoles { get; set; }
    }
}