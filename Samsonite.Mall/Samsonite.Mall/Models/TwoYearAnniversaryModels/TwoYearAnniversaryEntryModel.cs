using LinqKit;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.TwoYearAnniversaryModels
{
    public class TwoYearAnniversaryEntryModel {

        public string Id { get; set; }

        public string Channel { get; set; }

        public string IpAddress { get; set; }                

        public string SamsoniteId { get; set; }

        public string PrizeName { get; set; }

        public TwoYearAnniversarInstantPrizeType PrizeType { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

    }
}