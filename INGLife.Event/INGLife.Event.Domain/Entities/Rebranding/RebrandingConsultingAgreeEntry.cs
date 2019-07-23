using INGLife.Event.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.Rebranding
{
    public class RebrandingConsultingAgreeEntry : SnsSharingUser {
        [Required]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "관심분야")]
        public RebrandingAttentionType AttetionTypoe { get; set; }
        public virtual string AttetionTypoeDisplayName {
            get {
                var field = AttetionTypoe.GetType().GetField(AttetionTypoe.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : AttetionTypoe.ToString();
            }
        }
        [MaxLength(5)]
        [Display(Name = "우편번호")]
        public string ZipCode { get; set; }
        [Display(Name = "주소")]
        [MaxLength(255)]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [MaxLength(255)]
        public string AddressDetail { get; set; }
        [Display(Name = "추가정보 등록일")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }
    public enum RebrandingAttentionType {
        [Display(Name = "건강관리")]
        HealthCare = 1,
        [Display(Name = "월급관리")]
        SalaryManagement = 2,
        [Display(Name = "저축/연금")]
        Saving = 3,
        [Display(Name = "예비자금준비")]
        ReserveFund = 4,
        [Display(Name = "기타")]
        Etc = 5
    }
}
