using Finnq.Promotion.Domain.Abstract;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.TWorldRouletteEvent {
    public class TWorldRouletteEventQueryOptions : PageQueryOptions{
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? UpdateFromDate { get; set; }
        public DateTime? UpdateToDate { get; set; }
        public ChannelType? Channel { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public TWorldRouletteEventInstantLotteryPrizeType? PrizeType { get; set; }

        public Expression<Func<TWorldRouletteEventEntry, bool>> BuildPredicate() {
            Expression<Func<TWorldRouletteEventEntry, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (UpdateFromDate.HasValue) {
                predicate = predicate.And(x => x.UpdateDate >= UpdateFromDate.Value);
            }
            if (UpdateToDate.HasValue) {
                var toDate = UpdateToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.UpdateDate < toDate);
            }
            if (Channel.HasValue) {
                predicate = predicate.And(x => x.Channel == Channel);
            }
            if (PrizeType.HasValue) {
                predicate = predicate.And(x => x.PrizeType == PrizeType);
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Mobile) && x.Mobile.Contains(Mobile));
            }

            return predicate;
        }
    }
}
