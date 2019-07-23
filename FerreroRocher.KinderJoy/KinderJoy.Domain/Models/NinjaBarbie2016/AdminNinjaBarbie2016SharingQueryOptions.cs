using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.NinjaBarbie2016 {
    public class AdminNinjaBarbie2016SharingQueryOptions : PageQueryOptions {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Channel { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int? Age { get; set; }
        public NinjaBarbieSurprizeType? SurprizeType { get; set; }
        public SnsType? SnsType { get; set; }
        public string IpAddress { get; set; }
        
        public Expression<Func<NinjaBarbie2016Sharing, bool>> BuildPredicate() {

            Expression<Func<NinjaBarbie2016Sharing, bool>> predicate = x => true;
            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (!string.IsNullOrWhiteSpace(Channel)) {
                predicate = predicate.And(x => x.User.Channel == Channel);
            }
            if (!string.IsNullOrWhiteSpace(Name)) {
                predicate = predicate.And(x => x.User.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile)) {
                predicate = predicate.And(x => x.User.Mobile.Contains(Mobile));
            }
            if (Age.HasValue) {
                predicate = predicate.And(x => x.User.Age == Age.Value);
            }
            if (SurprizeType.HasValue) {
                predicate = predicate.And(x => x.User.SurprizeType == SurprizeType.Value);
            }
            if (SnsType.HasValue) {
                predicate = predicate.And(x => x.SnsType == SnsType.Value);
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.User.IpAddress == IpAddress);
            }

            return predicate;
        }
    }
}
