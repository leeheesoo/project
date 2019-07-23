using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.DisneyStarWars2018
{
    public enum DisneyStarWars2018InstantLotteryPrizeType{
        [Display(Name = "꽝")]
        Loser = 0,
        [Display(Name = "킨더조이 키프티콘(랜덤발송)")]
        KinderJoyGifty = 1,
        [Display(Name = "디즈니 공주 거울")]
        DisneyQuenMirror = 2,
        [Display(Name = "스타워즈 물컵(3개 세트)")]
        StarWarsCupSet = 3,
    }

    public class DisneyStarWars2018InstantLotteryPrizeSetting : EventNaverSearchingSettings<DisneyStarWars2018InstantLotteryPrizeType> {
    }
}
