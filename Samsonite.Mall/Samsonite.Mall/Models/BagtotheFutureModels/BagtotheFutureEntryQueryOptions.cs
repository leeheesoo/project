﻿using LinqKit;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Samsonite.Mall.Models.BagtotheFutureModels {
    public class BagtotheFutureEntryQueryOptions: PageQueryOptions {

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Channel { get; set; }
        public string IpAddress { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }
        public string IdeaName { get; set; }
        public string IdeaDescription { get; set; }

        public Expression<Func<BagtotheFutureEntry, bool>> BuildPredicate() {
            Expression<Func<BagtotheFutureEntry, bool>> predicate = x => true;

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

            if (!string.IsNullOrEmpty(IdeaName)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(IdeaName) && x.IdeaName.Contains(IdeaName));
            }
            if (!string.IsNullOrEmpty(IdeaDescription)) {
                predicate = predicate.And(x => !string.IsNullOrEmpty(IdeaDescription) && x.IdeaDescription.Contains(IdeaDescription));
            }

            return predicate;
        }
    }
}