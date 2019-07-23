using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.MamboEvent
{
    public class NilririmanboEntry
    {
        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }
        [Required]
        [Display(Name = "이름")]
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "휴대폰번호")]
        public string Mobile { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "인스타그램ID")]
        public string InstagramId { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "FC code")]
        public string FcCode { get; set; }
        [Required]
        [Display(Name = "IP")]
        public string IpAddress { get; set; }
        [Required]
        [Display(Name = "참여일")]
        public DateTime CreateDate { get; set; }
    }
}
