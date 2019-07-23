using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Entities.KittyJusticeLeague;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.KittyJusticeleage {
    public class KittyJusticeLeaguePrizeSettingModel {
        [Display(Name = "날짜")]
        [Required(ErrorMessage = "날짜를 입력해주세요")]
        public DateTime Date { get; set; }

        [Display(Name = "경품타입")]
        [Required(ErrorMessage = "경품타입을 입력해주세요")]
        public KittyJusticeLeagueInstantLotteryPrizeType PrizeType { get; set; }

        [Display(Name = "당첨확률(%)")]
        [Required(ErrorMessage = "당첨확률을 입력해주세요")]
        [AssertThat("Rate > 0", ErrorMessage = "당첨확률을 정확히 입력해주세요.")]
        public float Rate { get; set; }

        [Display(Name = "총 경품수")]
        [Required(ErrorMessage = "총 경품수를 입력해주세요")]
        [AssertThat("TotalCount > 0", ErrorMessage = "총 경품수를 정확히 입력해주세요.")]
        public int TotalCount { get; set; }

        [Display(Name = "당첨된 경품수")]
        public int WinnerCount { get; set; }

        [Display(Name = "시작시간")]
        [Required(ErrorMessage = "당첨 시작시간을 입력해주세요")]
        [AssertThat("StartTime >= 0 && StartTime < 24", ErrorMessage = "당첨 시작시간은 0~23 까지 가능합니다.")]
        public int StartTime { get; set; }
    }
}