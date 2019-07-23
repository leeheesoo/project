using System;
using INGLife.Event.Domain.Entities.Abstract;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace INGLife.Event.Domain.Services.FinancialConsultantSharing
{
    public class OriginalStatusModel : SnsSharingUser
    {

        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public string BirthDay { get; set; }        
        public int Cnt { get; set; }        
        public string CustomerRandomNum { get; set; }
        public string Email { get; set; }
        public bool? EmailCheck { get; set; }
        public string FcCode { get; set; }
        public string Gender { get; set; }        
        public bool? IsMessage { get; set; }
        public bool? MessageCheck { get; set; }        
        public bool? PhoneCheck { get; set; }
        public bool? PostCheck { get; set; }
        public RetentionPeriodOriginType? RetentionPeriodOriginType { get; set; }
        public string ZipCode { get; set; }

        public virtual string RetentionPeriodOriginTypeDisplayName
        {
            get
            {
                if (RetentionPeriodOriginType.HasValue)
                {
                    var field = RetentionPeriodOriginType.Value.GetType().GetField(RetentionPeriodOriginType.Value.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : RetentionPeriodOriginType.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual string AddressDisplayName
        {
            get
            {
                return (string.IsNullOrEmpty(ZipCode) ? "" : string.Format("({0}) {1} {2}", ZipCode, Address, AddressDetail));
            }
        }
    }
}