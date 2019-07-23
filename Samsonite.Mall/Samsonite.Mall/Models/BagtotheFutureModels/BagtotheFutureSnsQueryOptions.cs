using LinqKit;
using Samsonite.Mall.Domain.Entities.Abstract;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.BagtotheFutureModels {
    public class BagtotheFutureSnsQueryOptions : PageQueryOptions {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Channel { get; set; }
        public string IpAddress { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }
        public SnsType? SnsType { get; set; }

        public Expression<Func<BagtotheFutureSns, bool>> BuildPredicate() {
            Expression<Func<BagtotheFutureSns, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                var fromDate = FromDate.Value.Date;
                predicate = predicate.And(x => x.CreateDate.Date >= fromDate);
            }
            if (ToDate.HasValue) {
                var todate = ToDate.Value.Date;
                predicate = predicate.And(x => x.CreateDate.Date <= todate);
            }
            if (!string.IsNullOrEmpty(Channel)) {
                predicate = predicate.And(x => x.User.Channel == Channel);
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.User.IpAddress == IpAddress);
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.User.Name) && x.User.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.User.Mobile) && x.User.Mobile.Contains(Mobile));
            }
            if(SnsType.HasValue) {
                predicate = predicate.And(X => X.SnsType == SnsType.Value);
            }
            return predicate;
        }
    }
}