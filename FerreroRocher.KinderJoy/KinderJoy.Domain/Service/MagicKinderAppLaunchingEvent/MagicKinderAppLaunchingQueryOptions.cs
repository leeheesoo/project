using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent
{
    public class MagicKinderAppLaunchingQueryOptions : PageQueryOptions
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Channel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public MagicKinderScreenShotTypes? ScreenShotType { get; set; }
        public bool? IsShow { get; set; }

        public Expression<Func<MagicKinderAppLaunching, bool>> BuildPredicate()
        {
            Expression<Func<MagicKinderAppLaunching, bool>> predicate = x => true;
            if (FromDate.HasValue)
            {
                predicate = predicate.And(x => x.CreateDate >= FromDate);
            }
            if (ToDate.HasValue)
            {
                var toDate = ToDate.Value.AddDays(1);
                predicate = predicate.And(x => x.CreateDate < toDate);
            }
            if (!string.IsNullOrWhiteSpace(Channel))
            {
                predicate = predicate.And(x => x.Channel.Contains(Channel));
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                predicate = predicate.And(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                predicate = predicate.And(x => x.Mobile.Contains(Mobile));
            }
            if (ScreenShotType.HasValue)
            {
                predicate = predicate.And(x => x.ScreenShotType == ScreenShotType.Value);
            }
            if (IsShow.HasValue)
            {
                predicate = predicate.And(x => x.IsShow == IsShow);
            }

            return predicate;
        }
    }

    public class MagicKinderAppLaunchingIsShowChangeQueryOptions
    {
        public long Id { get; set; }
    }
}
