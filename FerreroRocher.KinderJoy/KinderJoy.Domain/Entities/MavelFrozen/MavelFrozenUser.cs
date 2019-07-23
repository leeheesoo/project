using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.MavelFrozen {
    public class MavelFrozenUser : EventSnsSharingUser {
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
        [Display(Name = "자녀 성별")]
        [Required]
        public GenderType ChildGender { get; set; }
        public virtual string ChildGenderDisplayName {
            get {
                var field = ChildGender.GetType().GetField(ChildGender.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : ChildGender.ToString();
            }
        }
        [Display(Name = "채널")]
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }
        [Display(Name = "IP")]
        [Required]
        public string IpAddress { get; set; }
    }
    public enum GenderType {
        [Display(Name = "남아")]
        Boy = 0,
        [Display(Name = "여아")]
        Girl = 1,
    }
}
