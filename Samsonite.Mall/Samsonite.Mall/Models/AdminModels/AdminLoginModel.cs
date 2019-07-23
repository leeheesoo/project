using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.AdminModels {
    public class AdminLoginModel {
        [Display(Name = "아이디")]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string ID { get; set; }
        [Display(Name = "패스워드")]
        [Required(ErrorMessage = "패스워드를 입력해주세요.")]
        public string Password { get; set; }
    }
}