using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.Abstract {
    public abstract class SnsSharingUser {
        [Key]
        [Required]
        [Display(Name = "번호")]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "이름")]
        public string Name { get; set; }
        [Required]
        [MaxLength(13)]
        [Display(Name = "연락처")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "참여일시")]
        public DateTime CreateDate { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "아이피주소")]
        public string IpAddress { get; set; }

        [Required]
        [Display(Name = "채널")]
        public ChannelType Channel { get; set; }
        public virtual string ChannelDisplayName {
            get {
                var field = Channel.GetType().GetField(Channel.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : Channel.ToString();
            }
        }
    }
}
