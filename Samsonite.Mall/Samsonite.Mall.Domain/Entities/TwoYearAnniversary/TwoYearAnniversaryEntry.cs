using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.TwoYearAnniversary {
    /// <summary>
    /// 쌤소나이트 1주년
    /// </summary>
    public class TwoYearAnniversaryEntry {
        [Display(Name = "순번")]
        [Key]
        [Required]
        public long Id { get; set; }

        [Display(Name = "샘소나이트 아이디")]
        [Required]
        [MaxLength(250)]
        public string SamsoniteId { get; set; }

        [Display(Name = "샘소나이트 암호화")]
        [Required]
        [MaxLength(250)]
        public string SamsoniteEncryptionId { get; set; }

        [Display(Name = "룰렛 경품")]
        [Required]        
        public TwoYearAnniversarInstantPrizeType PrizeType { get; set; }
        public virtual string PrizeName
        {
            get
            {
                var field = PrizeType.GetType().GetField(PrizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : PrizeType.ToString();
            }
        }

        [Display(Name = "채널")]
        [Required]
        public ChannelType Channel { get; set; }
        public virtual string ChannelName {
            get {
                var field = Channel.GetType().GetField(Channel.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : Channel.ToString();
            }
        }
        [Display(Name = "아이피주소")]
        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }

        [Display(Name = "참여일시")]
        [Required]
        public DateTime CreateDate { get; set; }       
    }
}
