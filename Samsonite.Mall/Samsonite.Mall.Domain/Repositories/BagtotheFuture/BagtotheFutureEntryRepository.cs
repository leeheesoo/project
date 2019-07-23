using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;

namespace Samsonite.Mall.Domain.Repositories.BagtotheFuture {
    public class BagtotheFutureEntryRepository : RepositoryBase<AppUser, AppDbContext, BagtotheFutureEntry>, IBagtotheFutureEntryRepository {
        public BagtotheFutureEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
