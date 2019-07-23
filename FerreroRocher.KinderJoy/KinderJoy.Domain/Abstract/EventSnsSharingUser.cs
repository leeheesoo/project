using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract
{
   public abstract class EventSnsSharingUser {
        [Key]
        [Required]
        [Display(Name = "참여자 번호")]
        public long Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "이름")]
        public string Name { get; set; }
        [Required]
        [MaxLength(13)]
        [Display(Name = "연락처")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "참여일시")]
        public DateTime CreateDate { get; set; }
    }
}