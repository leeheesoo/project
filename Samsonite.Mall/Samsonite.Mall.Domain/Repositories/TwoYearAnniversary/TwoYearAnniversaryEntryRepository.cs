using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;

namespace Samsonite.Mall.Domain.Repositories.TwoYearAnniversary {
    public class TwoYearAnniversaryEntryRepository : RepositoryBase<AppUser, AppDbContext, TwoYearAnniversaryEntry>, ITwoYearAnniversaryEntryRepository {
        public TwoYearAnniversaryEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
