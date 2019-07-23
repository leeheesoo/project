using LinqKit;
using Samsonite.Mall.Domain.Entities.Abstract;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.TwoYearAnniversaryModels
{

    public class TwoYearAnniversaryPageQueryOptions : PageQueryOptions {

        public ChannelType? Channel { get; set; }
        public string IpAddress { get; set; }                
        public string SamsoniteId { get; set; }
        public TwoYearAnniversarInstantPrizeType? PrizeType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// 노출여부
        /// </summary>
        public bool? IsShow { get; set; }

        public Expression<Func<TwoYearAnniversaryEntry, bool>> BuildPredicate() {
            Expression<Func<TwoYearAnniversaryEntry, bool>> predicate = x => true;


            if (FromDate.HasValue)
            {
                var fromDate = FromDate.Value;
                predicate = predicate.And(x => x.CreateDate >= fromDate);
            }
            if (ToDate.HasValue)
            {
                var todate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate <= todate);
            }

            if (!PrizeType.Equals(null))
            {
                predicate = predicate.And(x => x.PrizeType == PrizeType);
            }

            if (!Channel.Equals(null)) {
                predicate = predicate.And(x => x.Channel == Channel);
            }

            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.IpAddress.Contains(IpAddress));
            }           
           
            if (!string.IsNullOrEmpty(SamsoniteId)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.SamsoniteId) && x.SamsoniteId.Contains(SamsoniteId));
            }

            if (PrizeType.HasValue)
            {
                predicate = predicate.And(x => x.PrizeType == PrizeType);
            }
            
            return predicate;
        }
    }
}