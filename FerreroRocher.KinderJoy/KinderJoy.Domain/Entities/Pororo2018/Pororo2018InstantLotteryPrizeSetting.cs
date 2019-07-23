using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.Pororo2018 {
    public enum Pororo2018InstantLotteryPrizeType {
        [Display(Name = "꽝")]
        Loser = 0,
        [Display(Name = "기프티콘")]
        JoyGifticon = 1,
        [Display(Name = "킨더조이 목걸이 만들기 DIY 팩")]
        DIYPack = 2,
        [Display(Name = "뽀로로 사인펜")]
        MarkerPen = 3,
        [Display(Name = "뽀로로 스케치북")]
        Sketchbook = 4
    }

    public class Pororo2018InstantLotteryPrizeSetting : EventNaverSearchingSettings<Pororo2018InstantLotteryPrizeType> {
    }
}
