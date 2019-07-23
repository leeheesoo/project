using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.MainStream;
using LinqKit;

namespace KinderJoy.Domain.Service.MainStream {
    public class MainStreamQueryOptions : PageQueryOptions {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Device { get; set; }
        public string IpAddress { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

        public MainStreamSurpriseType? MainStreamSurpriseType { get; set; }

        public virtual Expression<Func<MainStreamSurprise, bool>> BuildPredicate() {

            Expression<Func<MainStreamSurprise, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }

            if (!string.IsNullOrEmpty(Device)) {
                predicate = predicate.And(x => x.Channel.Equals(Device));
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.IpAddress.Contains(IpAddress));
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Age)) {
                predicate = predicate.And(x => x.Age.ToString().Contains(Age));
            }
            if (!string.IsNullOrEmpty(Gender)) {
                predicate = predicate.And(x => x.Gender.Equals(Gender));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => x.Mobile.Contains(Mobile));
            }
            if (MainStreamSurpriseType.HasValue) {
                predicate = predicate.And(x => x.Quiz == MainStreamSurpriseType);
            }
            return predicate;
        }
    }
    public class MainStreamSnsQueryOptions : MainStreamQueryOptions {
        public string SnsType { get; set; }
        public string SnsId { get; set; }
        public string SnsName { get; set; }
        public string SnsNickName { get; set; }
        public string ScrapUrl { get; set; }

        public new Expression<Func<MainStreamSurpriseSNS, bool>> BuildPredicate() {

            Expression<Func<MainStreamSurpriseSNS, bool>> predicate = x => true;

            if (FromDate.HasValue) {
                predicate = predicate.And(x => x.MainStreamSurprise.CreateDate >= FromDate.Value);
            }
            if (ToDate.HasValue) {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.MainStreamSurprise.CreateDate < toDate);
            }

            if (!string.IsNullOrEmpty(Device)) {
                predicate = predicate.And(x => x.MainStreamSurprise.Channel.Equals(Device));
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.MainStreamSurprise.IpAddress.Contains(IpAddress));
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => x.MainStreamSurprise.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Age)) {
                predicate = predicate.And(x => x.MainStreamSurprise.Age.ToString().Contains(Age));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => x.MainStreamSurprise.Mobile.Contains(Mobile));
            }
            if (MainStreamSurpriseType.HasValue) {
                predicate = predicate.And(x => x.MainStreamSurprise.Quiz == MainStreamSurpriseType);
            }
            if (!string.IsNullOrEmpty(SnsType)) {
                predicate = predicate.And(x => x.SnsType == SnsType);
            }
            if (!string.IsNullOrEmpty(SnsId)) {
                predicate = predicate.And(x => x.SnsId.Contains(SnsId));
            }
            if (!string.IsNullOrEmpty(SnsName)) {
                predicate = predicate.And(x => x.SnsName.Contains(SnsName));
            }
            if (!string.IsNullOrEmpty(SnsNickName)) {
                predicate = predicate.And(x => x.SnsNickName.Contains(SnsNickName));
            }
            if (!string.IsNullOrEmpty(ScrapUrl)) {
                predicate = predicate.And(x => x.ScrapUrl.Contains(ScrapUrl));
            }
            return predicate;
        }
    }
}
