using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.OverFortyFiveDb
{
    public class OverFortyFiveDbEntry : SnsSharingUser
    {
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
            get
            {
                if (RetentionPeriodType.HasValue)
                {
                    var field = RetentionPeriodType.Value.GetType().GetField(RetentionPeriodType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : RetentionPeriodType.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "이모티콘")]
        public EmoticonType? EmoticonType { get; set; }
        public virtual string EmoticonTypeDisplayName
        {
            get
            {
                if (EmoticonType.HasValue)
                {
                    var field = EmoticonType.Value.GetType().GetField(EmoticonType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : EmoticonType.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "추가정보 등록일")]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "tnk 호출 결과")]
        public string TnkResult { get; set; }

        [Display(Name = "tnk adKey")]
        public string TnkAdKey { get; set; }

        [Display(Name = "tnk 호출 등록일")]
        public DateTime? TnkUpdateDate { get; set; }

        [Display(Name = "문자발송여부")]
        public bool? IsMessage { get; set; }
    }
}

public enum EmoticonType
{
    [Display(Name = "엄니맘을 알어?")]
    Emoticon1 = 1,
    [Display(Name = "깨방정 요하")]
    Emoticon2 = 2,
    [Display(Name = "이런 덕담 처음이지?")]
    Emoticon3 = 3
}
