using Finnq.Promotion.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Entities.TWorldRouletteEvent {
    public enum TWorldRouletteEventInstantLotteryPrizeType {
        [Display(Name = "꽝")]
        Loser = 0,
        [Display(Name = "5천원")]
        Cash5000 = 1,
        [Display(Name = "만원")]
        Cash10000 = 2,
        [Display(Name = "5만원")]
        Cash50000 = 3,
        [Display(Name = "10만원")]
        Cash100000 = 4,
        [Display(Name = "50만원")]
        Cash500000 = 5,
        [Display(Name = "100만원")]
        Cash1000000 = 6
    }
    public class TWorldRouletteEventInstantLotteryPrizeSetting : InstantLotteryPrizeSetting<TWorldRouletteEventInstantLotteryPrizeType> {
    }
}
