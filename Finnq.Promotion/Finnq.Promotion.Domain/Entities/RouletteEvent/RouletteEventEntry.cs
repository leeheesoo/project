using Finnq.Promotion.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Entities.RouletteEvent {
    public class RouletteEventEntry {
        [Key]
        [Required]
        [Display(Name = "번호")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "채널")]
        public ChannelType Channel { get; set; }
        public virtual string ChannelName {
            get {
                var field = Channel.GetType().GetField(Channel.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : Channel.ToString();
            }
        }

        [Required]
        [MaxLength(10)]
        [Display(Name = "이름")]
        public string Name { get; set; }

        [Required]
        [MaxLength(13)]
        [Display(Name = "연락처")]
        public string Mobile { get; set; }

        [Display(Name = "경품타입")]
        public RouletteEventInstantLotteryPrizeType? PrizeType { get; set; }
        public virtual string PrizeName {
            get {
                if(!PrizeType.HasValue) {
                    return null;
                }
                var field = PrizeType.Value.GetType().GetField(PrizeType.Value.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : PrizeType.Value.ToString();
            }
        }

        [Required]
        [MaxLength(15)]
        [Display(Name = "IP")]
        public string IpAddress { get; set; }

        [Required]
        [Display(Name = "개인정보 등록일")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "즉석당첨 참여일")]
        public DateTime? UpdateDate { get; set; }
    }
}
