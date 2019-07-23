using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.Abstract {
    public abstract class EventSnsSharing<TType> {
        [Key]
        [Required]
        [Display(Name = "번호")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Required]
        [Display(Name = "참여자 번호")]
        public long UserId { get; set; }
        public virtual TType User { get; set; }

        [Required]
        [Display(Name = "SNS")]
        public SnsType SnsType { get; set; }
        public virtual string SnsTypeDisplayName {
            get {
                var field = SnsType.GetType().GetField(SnsType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : SnsType.ToString();
            }
        }

        [MaxLength(100)]
        [Display(Name = "SNS 아이디")]
        public string SnsId { get; set; }

        [MaxLength(100)]
        [Display(Name = "SNS 이름")]
        public string SnsName { get; set; }

        [MaxLength(300)]
        [Display(Name = "SNS 스크랩")]
        public string Post { get; set; }

        [Required]
        [Display(Name = "공유일자")]
        public DateTime CreateDate { get; set; }
    }
    
    public enum SnsType {
        [Display(Name = "페이스북")]
        Facebook = 1,
        [Display(Name = "트위터")]
        Twitter = 2,
        [Display(Name = "블로그")]
        Blog = 3,
        [Display(Name = "카카오스토리")]
        KakaoStory = 4,
        [Display(Name = "카카오톡")]
        Kakaotalk = 5
    }

    public enum ChannelType
    {
        [Display(Name = "PC")]
        PC = 1,
        [Display(Name = "Mobile")]
        Mobile = 2
    }
}
