using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.OneYearAnniversary {
    /// <summary>
    /// 쌤소나이트 1주년
    /// </summary>
    public class OneYearAnniversaryEntry {
        public OneYearAnniversaryEntry() {
            this.IsShow = true;
        }
        [Display(Name = "아이디")]
        [Key]
        public long Id { get; set; }

        [Display(Name = "쌤소나이트 오행시: 쌤")]
        [Required]
        [MaxLength(20)]
        public string AcrosticPoemFirst { get; set; }
        [Display(Name = "쌤소나이트 오행시: 소")]
        [Required]
        [MaxLength(20)]
        public string AcrosticPoemSecond { get; set; }
        [Display(Name = "쌤소나이트 오행시: 나")]
        [Required]
        [MaxLength(20)]
        public string AcrosticPoemThird { get; set; }
        [Display(Name = "쌤소나이트 오행시: 이")]
        [Required]
        [MaxLength(20)]
        public string AcrosticPoemFourth { get; set; }
        [Display(Name = "쌤소나이트 오행시: 트")]
        [Required]
        [MaxLength(20)]
        public string AcrosticPoemFifth { get; set; }

        public virtual string AcrosticPoem {
            get {
                return string.Format("{0}\n{1}\n{2}\n{3}\n{4}"
                    ,AcrosticPoemFirst, AcrosticPoemSecond, AcrosticPoemThird, AcrosticPoemFourth, AcrosticPoemFifth);
            }
        }

        [Display(Name = "축하메시지")]
        [Required]
        [MaxLength(100)]
        public string CongratulationMessage { get; set; }
       

        [Display(Name = "이름")]
        //[Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "아이디")]
        //[Required]
        [MaxLength(50)]
        public string SamsoniteId { get; set; }

        public virtual string MaskingSamsoniteId {
            get {
                if (!string.IsNullOrEmpty(SamsoniteId)) {
                    var subStrIdx = (SamsoniteId.Length / 2) - 1;
                    if (SamsoniteId.Length > 5) {
                        return string.Format("{0}***{1}", SamsoniteId.Substring(0, subStrIdx), SamsoniteId.Substring(subStrIdx + 3, SamsoniteId.Length - (subStrIdx + 3)));
                    }
                }
                return SamsoniteId;
            }
        }

        [Display(Name = "연락처")]
        //[Required]
        [MaxLength(11)]
        public string Mobile { get; set; }
        [Display(Name = "채널")]
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }

        [Display(Name = "아이피주소")]
        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }

        [Display(Name = "참여일시")]
        [Required]
        public DateTime CreateDate { get; set; }

        [Display(Name = "노출여부")]
        [Required]
        public bool IsShow { get; set; }
    }
}
