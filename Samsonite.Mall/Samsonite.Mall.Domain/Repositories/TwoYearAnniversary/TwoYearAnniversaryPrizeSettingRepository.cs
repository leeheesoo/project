using System;
using System.Linq;
using System.Linq.Expressions;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;

namespace Samsonite.Mall.Domain.Repositories.TwoYearAnniversary {
    public class TwoYearAnniversaryPrizeSettingRepository : RepositoryBase<AppUser, AppDbContext, TwoYearAnniversaryInstantPrizeSetting>, ITwoYearAnniversaryPrizeSettingRepository
    {
        public TwoYearAnniversaryPrizeSettingRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }

    }
}
