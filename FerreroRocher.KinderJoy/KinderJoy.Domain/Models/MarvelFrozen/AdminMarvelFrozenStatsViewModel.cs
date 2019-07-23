using KinderJoy.Domain.Entities.MavelFrozen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.MarvelFrozen {
    public class AdminMarvelFrozenStatsViewModel {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public GenderType ChildGender { get; set; }
        public virtual string ChildGenderDisplayName {
            get {
                var field = ChildGender.GetType().GetField(ChildGender.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : ChildGender.ToString();
            }
        }
        public int TotalCount { get; set; }
        public int FacebookCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaotalkCount { get; set; }
    }
}
