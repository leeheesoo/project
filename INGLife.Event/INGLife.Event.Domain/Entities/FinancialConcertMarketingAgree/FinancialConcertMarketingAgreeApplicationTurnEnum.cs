using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree {
    public enum FinancialConcertMarketingAgreeApplicationTurnEnum {
        [Display(Name = "1회차(10월 20일)_박지선편")]
        FIRST = 1,
        [Display(Name = "2회차(10월 27일)_유병재편")]
        SECOND = 2,
        [Display(Name = "3회차(11월 3일)_조승연편")]
        THIRD = 3
    }
}
