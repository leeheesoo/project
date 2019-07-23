using Finnq.Promotion.Domain.Entities.TmapEvent;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.TmapEvent {
    public class TmapEventEntryQueryOptions: PageQueryOptions {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsMobileDevice { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Expression<Func<TmapEventEntry, bool>> BuildPredicate() {
            Expression<Func<TmapEventEntry, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (IsMobileDevice.HasValue) {
                predicate = predicate.And(x => x.IsMobileDevice.Equals(IsMobileDevice));
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Phone)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(Phone));
            }

            return predicate;
        }
    }
}
