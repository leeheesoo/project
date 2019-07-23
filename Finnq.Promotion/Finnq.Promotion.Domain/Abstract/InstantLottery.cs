using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Abstract {
    public abstract class InstantLottery<TType> {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "채널")]
        public ChannelType Channel { get; set; }
        public string ChannelName {
            get {
                var field = Channel.GetType().GetField(Channel.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : Channel.ToString();
            }
        }

        [Required]
        [Display(Name = "경품타입")]
        public TType PrizeType { get; set; }
        public string PrizeName {
            get {
                var field = PrizeType.GetType().GetField(PrizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : PrizeType.ToString();
            }
        }

        [MaxLength(15)]
        [Required]
        [Display(Name = "IP")]
        public string IpAddress { get; set; }

        [Required]
        [Display(Name = "참여일")]
        public DateTime CreateDate { get; set; }

        [MaxLength(10)]
        [Display(Name = "이름")]
        public string Name { get; set; }

        [MaxLength(15)]
        [Display(Name = "휴대폰번호")]
        public string Mobile { get; set; }

        [Display(Name = "개인정보 등록일")]
        public DateTime? UpdateDate { get; set; }
    }
}
