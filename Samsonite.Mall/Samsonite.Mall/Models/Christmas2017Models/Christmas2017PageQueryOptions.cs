using LinqKit;
using Samsonite.Mall.Domain.Entities.Christmas2017;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.Christmas2017Models {

    public class Christmas2017PageQueryOptions : PageQueryOptions {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Channel { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string SamsoniteMallId { get; set; }
        public ChristmasBagType? ChristmasBagType { get; set; }


        public Expression<Func<Christmas2017Entry, bool>> BuildPredicate() {
            Expression<Func<Christmas2017Entry, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate);
            }
            if (ToDate.HasValue) {
                var todate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate <= todate);
            }

            if (!string.IsNullOrEmpty(Channel)) {
                predicate = predicate.And(x => x.Channel == Channel);
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Mobile) && x.Mobile.Contains(Mobile));
            }
            if (!string.IsNullOrEmpty(SamsoniteMallId)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.SamsoniteMallId) && x.SamsoniteMallId.Contains(SamsoniteMallId));
            }
            if (ChristmasBagType.HasValue) {
                predicate = predicate.And(x => x.ChristmasBagType == ChristmasBagType);
            }
            return predicate;
        }
    }
}