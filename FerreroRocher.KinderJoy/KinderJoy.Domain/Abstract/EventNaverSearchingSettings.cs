using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract {
    public abstract class EventNaverSearchingSettings<TType> {
        [Key, Column(Order = 0)]
        [Display(Name = "날짜")]
        public DateTime Date { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "경품타입")]
        public TType PrizeType { get; set; }
        public virtual string PrizeName {
            get {
                var field = PrizeType.GetType().GetField(PrizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : PrizeType.ToString();
            }
        }
        [Display(Name = "당첨확률(%)")]
        public float Rate { get; set; }
        [Display(Name = "총 경품수")]
        public int TotalCount { get; set; }
        [Display(Name = "당첨된 경품수")]
        public int WinnerCount { get; set; }
        [Display(Name = "시작시간")]
        public int StartTime { get; set; }
    }
}
