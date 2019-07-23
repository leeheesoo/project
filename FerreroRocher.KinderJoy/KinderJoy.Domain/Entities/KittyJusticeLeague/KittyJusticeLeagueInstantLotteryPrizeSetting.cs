using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.KittyJusticeLeague {
    public enum KittyJusticeLeagueInstantLotteryPrizeType {
        [Display(Name = "꽝")]
        Loser = 0,
        [Display(Name = "기프티콘")]
        JoyGifticon = 1,
        [Display(Name = "헬로키티 나노블럭")]
        KittyNanoBlock = 2,
        [Display(Name = "저스티스리그 나노블럭")]
        JusticeLeagueNanoBlock = 3,
        [Display(Name = "미니 크리스마스 트리")]
        ChirstmasTree = 4,
        [Display(Name = "크리스마스 카드")]
        ChristmasCard = 5
    }

    public class KittyJusticeLeagueInstantLotteryPrizeSetting : EventNaverSearchingSettings<KittyJusticeLeagueInstantLotteryPrizeType> {
    }
}
