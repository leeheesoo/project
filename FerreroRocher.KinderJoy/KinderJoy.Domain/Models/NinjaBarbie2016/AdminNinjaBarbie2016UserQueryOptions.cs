using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Service;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.NinjaBarbie2016 {
    public class AdminNinjaBarbie2016UserQueryOptions : PageQueryOptions {
        public long? Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Channel { get; set; }
        public string IpAddress { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int? Age { get; set; }
        public NinjaBarbieSurprizeType? SurprizeType { get; set; }

        public Expression<Func<NinjaBarbie2016User, bool>> BuildPredicate() {

            Expression<Func<NinjaBarbie2016User, bool>> predicate = x => true;
            if (Id.HasValue) {
                predicate = predicate.And(x => x.Id == Id.Value);
            }
            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (!string.IsNullOrEmpty(Channel)) {
                predicate = predicate.And(x => x.Channel == Channel);
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.IpAddress == IpAddress);
            }
            if (!string.IsNullOrWhiteSpace(Name)) {
                predicate = predicate.And(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile)) {
                predicate = predicate.And(x => x.Mobile.Contains(Mobile));
            }
            if (!string.IsNullOrWhiteSpace(Address)) {
                predicate = predicate.And(x => x.Address.Contains(Address) || x.AddressDetail.Contains(Address));
            }
            if (!string.IsNullOrWhiteSpace(ZipCode)) {
                predicate = predicate.And(x => x.ZipCode == ZipCode);
            }
            if (Age.HasValue) {
                predicate = predicate.And(x => x.Age == Age.Value);
            }
            if (SurprizeType.HasValue) {
                predicate = predicate.And(x => x.SurprizeType == SurprizeType.Value);
            }

            return predicate;
        }
    }
}
