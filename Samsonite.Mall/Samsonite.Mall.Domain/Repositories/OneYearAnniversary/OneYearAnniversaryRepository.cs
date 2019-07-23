using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;

namespace Samsonite.Mall.Domain.Repositories.OneYearAnniversary {
    public class OneYearAnniversaryRepository : RepositoryBase<AppUser, AppDbContext, OneYearAnniversaryEntry>, IOneYearAnniversaryRepository {
        public OneYearAnniversaryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
