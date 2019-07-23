using LinqKit;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.OneYearAnniversaryModels {

    public class OneYearAnniversaryPageQueryOptions : PageQueryOptions {
        /// <summary>
        /// 리스트여부(true:프로모션페이지에 노출될 data 가져오기)
        /// </summary>
        public bool? IsList { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Channel { get; set; }
        public string IpAddress { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string SamsoniteId { get; set; }

        public string AcrosticPoemFirst { get; set; }

        public string AcrosticPoemSecond { get; set; }

        public string AcrosticPoemThird { get; set; }

        public string AcrosticPoemFourth { get; set; }

        public string AcrosticPoemFifth { get; set; }

        public string CongratulationMessage { get; set; }

        /// <summary>
        /// 노출여부
        /// </summary>
        public bool? IsShow { get; set; }

        public Expression<Func<OneYearAnniversaryEntry, bool>> BuildPredicate() {
            Expression<Func<OneYearAnniversaryEntry, bool>> predicate = x => true;

            if (IsList.HasValue && IsList.Value) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.SamsoniteId));
            }

            if (FromDate.HasValue) {
                var fromDate = FromDate.Value.Date;
                predicate = predicate.And(x => x.CreateDate.Date >= fromDate);
            }
            if (ToDate.HasValue) {
                var todate = ToDate.Value.Date;
                predicate = predicate.And(x => x.CreateDate.Date <= todate);
            }

            if (!string.IsNullOrEmpty(Channel)) {
                predicate = predicate.And(x => x.Channel == Channel);
            }
            if (!string.IsNullOrEmpty(IpAddress)) {
                predicate = predicate.And(x => x.IpAddress == IpAddress);
            }
            if (!string.IsNullOrEmpty(Name)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Mobile)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(Mobile) && x.Mobile.Contains(Mobile));
            }
            if (!string.IsNullOrEmpty(SamsoniteId)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.SamsoniteId) && x.SamsoniteId.Contains(SamsoniteId));
            }
            if (!string.IsNullOrEmpty(AcrosticPoemFirst)) {
                predicate = predicate.And(x => x.AcrosticPoemFirst.Contains(AcrosticPoemFirst));
            }
            if (!string.IsNullOrEmpty(AcrosticPoemSecond)) {
                predicate = predicate.And(x => x.AcrosticPoemSecond.Contains(AcrosticPoemSecond));
            }
            if (!string.IsNullOrEmpty(AcrosticPoemThird)) {
                predicate = predicate.And(x => x.AcrosticPoemThird.Contains(AcrosticPoemThird));
            }
            if (!string.IsNullOrEmpty(AcrosticPoemFourth)) {
                predicate = predicate.And(x => x.AcrosticPoemFourth.Contains(AcrosticPoemFourth));
            }
            if (!string.IsNullOrEmpty(AcrosticPoemFifth)) {
                predicate = predicate.And(x => x.AcrosticPoemFifth.Contains(AcrosticPoemFifth));
            }
            if (!string.IsNullOrEmpty(CongratulationMessage)) {
                predicate = predicate.And(x => x.CongratulationMessage.Contains(CongratulationMessage));
            }


            if (IsShow.HasValue) {
                predicate = predicate.And(x => x.IsShow == IsShow);
            }
            return predicate;
        }
    }
}