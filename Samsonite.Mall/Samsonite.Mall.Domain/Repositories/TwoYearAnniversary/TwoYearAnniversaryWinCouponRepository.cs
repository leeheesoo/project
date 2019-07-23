using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;

namespace Samsonite.Mall.Domain.Repositories.TwoYearAnniversary {
    public class TwoYearAnniversaryWinCouponRepository : RepositoryBase<AppUser, AppDbContext, TwoYearAnniversaryWinCoupon>, ITwoYearAnniversaryWinCouponRepository
    {
        public TwoYearAnniversaryWinCouponRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
