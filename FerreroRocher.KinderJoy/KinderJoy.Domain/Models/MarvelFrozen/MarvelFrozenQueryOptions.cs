using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.MavelFrozen;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.MarvelFrozen {
    public class AdminMarvelFrozenQueryOptions : PageQueryOptions {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Channel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public GenderType? ChildGender { get; set; }

        public Expression<Func<MavelFrozenUser, bool>> BuildPredicate() {
            Expression<Func<MavelFrozenUser, bool>> predicate = x => true;
            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (!string.IsNullOrWhiteSpace(Channel)) {
                predicate = predicate.And(x => x.Channel.Contains(Channel));
            }
            if (!string.IsNullOrWhiteSpace(Name)) {
                predicate = predicate.And(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile)) {
                predicate = predicate.And(x => x.Mobile.Contains(Mobile));
            }
            if (ChildGender.HasValue) {
                predicate = predicate.And(x => x.ChildGender == ChildGender.Value);
            }

            return predicate;
        }
    }

    public class AdminMarvelFrozenSNSQueryOptions : PageQueryOptions {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Channel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public SnsType? SnsType { get; set; }

        public Expression<Func<MavelFrozenSNS, bool>> BuildPredicate() {
            Expression<Func<MavelFrozenSNS, bool>> predicate = x => true;
            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (!string.IsNullOrWhiteSpace(Channel)) {
                predicate = predicate.And(x => x.User.Channel.Contains(Channel));
            }
            if (!string.IsNullOrWhiteSpace(Name)) {
                predicate = predicate.And(x => x.User.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile)) {
                predicate = predicate.And(x => x.User.Mobile.Contains(Mobile));
            }
            if (SnsType.HasValue) {
                predicate = predicate.And(x => x.SnsType == SnsType.Value);
            }

            return predicate;
        }
    }
}
