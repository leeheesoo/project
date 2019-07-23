using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.TwoYearAnniversaryModels
{
    public class TwoYearAnniversaryViewModel
    {
        public TwoYearAnniversarInstantPrizeType PrizeType { get; set; }
        public int RouletteType { get; set; }
        public string PrizeName { get; set; }
    }
    
    public class TwoYearCustNoModel
    {
        public string TwoYearCustNo { get; set; }
    }
}