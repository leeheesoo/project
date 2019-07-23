using Samsonite.Mall.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Entities.Christmas2017 {
    public class Christmas2017Entry : EventSnsSharingUser {
        [Required]
        [Display(Name = "샘소나이트몰아이디")]
        [MaxLength(50)]
        public string SamsoniteMallId { get; set; }
        [Required]
        [Display(Name = "크리스마스가방")]
        public ChristmasBagType ChristmasBagType { get; set; }
        public virtual string ChristmasBagName {
            get {
                var field = ChristmasBagType.GetType().GetField(ChristmasBagType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : ChristmasBagType.ToString();
            }
        }
    }

    public enum ChristmasBagType {
        [Display(Name = "TIELONN")]
        Tielonn = 0,
        [Display(Name = "MARLON")]
        Marlon = 1,
        [Display(Name = "SUPREME-LITE")]
        SupremeLite = 2,
        [Display(Name = "PLUME BASIC")]
        PlumeBasic = 3
    }
}
