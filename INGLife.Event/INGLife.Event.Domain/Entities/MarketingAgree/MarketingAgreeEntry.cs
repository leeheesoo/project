using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.MarketingAgree {
    public class MarketingAgreeEntry : SnsSharingUser {
        [Required]
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }

        [Display(Name = "보유기간")]
        public RetentionPeriodType? RetentionPeriodType { get; set; }
        public virtual string RetentionPeriodTypeDisplayName
        {
            get {
                if (RetentionPeriodType.HasValue) {
                    var field = RetentionPeriodType.Value.GetType().GetField(RetentionPeriodType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : RetentionPeriodType.Value.ToString();
                } else {
                    return null;
                }
            }
        }

        [Display(Name = "추가정보 등록일")]
        public DateTime? UpdateDate { get; set; }
       
        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }
}
