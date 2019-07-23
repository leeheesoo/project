using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.BagtotheFuture {
    public class BagtotheFutureEntry: EventSnsSharingUser {
        [Display(Name = "이메일" )]
        [MaxLength(100, ErrorMessage = "이메일주소를 100자내로 입력해주세요.")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "아이디어명")]
        [MaxLength(50, ErrorMessage = "아이디어명을 50자내로 입력해주세요.")]
        [Required]
        public string IdeaName { get; set; }

        [Display(Name = "아이디어에 대한 설명")]
        [MaxLength(1000, ErrorMessage = "아이디어에 대한 설명을 1000자내로 입력해주세요.")]
        [Required]
        public string IdeaDescription { get; set; }

        [Display(Name = "첨부파일")]
        [MaxLength(300)]
        public string File { get; set; }
    }
}
