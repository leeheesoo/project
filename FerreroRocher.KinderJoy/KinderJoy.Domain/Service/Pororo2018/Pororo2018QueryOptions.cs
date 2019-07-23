﻿using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.Pororo2018;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.Pororo2018 {
    public class Pororo2018QueryOptions : PageQueryOptions
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public ChannelType? Channel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? UpdateFromDate { get; set; }
        public DateTime? UpdateToDate { get; set; }
        public Pororo2018InstantLotteryPrizeType? PrizeType { get; set; }

        public Expression<Func<Pororo2018InstantLottery, bool>> BuildPredicate()
        {
            Expression<Func<Pororo2018InstantLottery, bool>> predicate = x => true;
            if (FromDate.HasValue)
            {
                predicate = predicate.And(x => x.CreateDate >= FromDate);
            }
            if (ToDate.HasValue)
            {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (Channel.HasValue)
            {
                predicate = predicate.And(x => x.Channel == Channel.Value);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                predicate = predicate.And(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                predicate = predicate.And(x => x.Mobile.Contains(Mobile));
            }
            if (!string.IsNullOrWhiteSpace(Address)) {
                predicate = predicate.And(x => x.Address.Contains(Address));
            }
            if (PrizeType.HasValue)
            {
                predicate = predicate.And(x => x.PrizeType == PrizeType.Value);
            }
            if (UpdateFromDate.HasValue) {
                predicate = predicate.And(x => x.UpdateDate >= UpdateFromDate);
            }
            if (UpdateToDate.HasValue) {
                var toDate = UpdateToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.UpdateDate < toDate);
            }

            return predicate;
        }
    }
}
