using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.FindingDory2017
{
    public class FindingDory2017User : EventSnsSharingUser
    {
        [Display(Name = "나이")]
        [Required]
        public int Age { get; set; }
        [Display(Name = "우편번호")]
        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }

        [Display(Name = "기본주소")]
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [Display(Name = "상세주소")]
        [Required]
        [MaxLength(300)]
        public string AddressDetail { get; set; }

        [Display(Name = "채널")]
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }

        [Display(Name = "IP")]
        [Required]
        public string IpAddress { get; set; }


    }
}
