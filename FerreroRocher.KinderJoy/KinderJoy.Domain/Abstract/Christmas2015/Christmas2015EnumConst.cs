using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract.Christmas2015 {
    /// <summary>
    /// 2015년 크리스마스 이벤트 enum Construction
    /// </summary>
    public class Christmas2015EnumConst {
        /// <summary>
        /// 킨더조이 장난감 종류
        /// </summary>
        public enum Christmas2015ToyType {
            [Display(Name = "장난감1")]
            Toy1 = 1,
            [Display(Name = "장난감2")]
            Toy2 = 2,
            [Display(Name = "장난감3")]
            Toy3 = 3,
            [Display(Name = "장난감4")]
            Toy4 = 4,
            [Display(Name = "장난감5")]
            Toy5 = 5,
            [Display(Name = "장난감6")]
            Toy6 = 6,
            [Display(Name = "장난감7")]
            Toy7 = 7,
            [Display(Name = "장난감8")]
            Toy8 = 8,
            [Display(Name = "장난감9")]
            Toy9 = 9,
            [Display(Name = "장난감10")]
            Toy10 = 10,
            [Display(Name = "장난감11")]
            Toy11 = 11,
            [Display(Name = "장난감12")]
            Toy12 = 12,
            [Display(Name = "장난감13")]
            Toy13 = 13,
            [Display(Name = "장난감14")]
            Toy14 = 14,
            [Display(Name = "장난감15")]
            Toy15 = 15,
            [Display(Name = "장난감16")]
            Toy16 = 16,
            [Display(Name = "장난감17")]
            Toy17 = 17,
            [Display(Name = "장난감18")]
            Toy18 = 18,
            [Display(Name = "장난감19")]
            Toy19 = 19,
            [Display(Name = "장난감20")]
            Toy20 = 20,
            [Display(Name = "장난감21")]
            Toy21 = 21,
            [Display(Name = "장난감22")]
            Toy22 = 22,
            [Display(Name = "장난감23")]
            Toy23 = 23,
            [Display(Name = "장난감24")]
            Toy24 = 24,
            [Display(Name = "장난감25")]
            Toy25 = 25,
            [Display(Name = "장난감26")]
            Toy26 = 26,
            [Display(Name = "장난감27")]
            Toy27 = 27,
            [Display(Name = "장난감28")]
            Toy28 = 28,
            [Display(Name = "장난감29")]
            Toy29 = 29,
            [Display(Name = "장난감30")]
            Toy30 = 30,
            [Display(Name = "장난감31")]
            Toy31 = 31,
            [Display(Name = "장난감32")]
            Toy32 = 32,
            [Display(Name = "장난감33")]
            Toy33 = 33,
            [Display(Name = "장난감34")]
            Toy34 = 34,
            [Display(Name = "장난감35")]
            Toy35 = 35,
        }
        /// <summary>
        /// 즉석당첨 이벤트 경품
        /// </summary>
        public enum Christmas2015WinPrize {
            [Display(Name = "꽝")]
            Loser = 0,
            [Display(Name = "망토")]
            Cloak = 1,
            [Display(Name = "킨더조이 1개입(남아용/여아용) 기프티콘")]
            Kinder = 2
        }
        public class Christmas2015WinPrizeSetting : EventNaverSearchingSettings<Christmas2015WinPrize> {
            public string PrizeName {
                get {
                    var field = PrizeType.GetType().GetField(PrizeType.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : PrizeType.ToString();
                }
            }
        }
    }
}
