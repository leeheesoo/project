using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.NinjaBarbie2016 {
    public class AdminNinjaBarbie2016StatisticsViewModel {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
        public int TotalCount { get; set; }
        public int FacebookCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaotalkCount { get; set; }
        public NinjaBarbieSurprizeType SurprizeType { get; set; }
        public virtual string SurprizeTypeDisplayName {
            get {
                var field = SurprizeType.GetType().GetField(SurprizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : SurprizeType.ToString();
            }
        }
    }
}
