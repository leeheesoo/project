using Finnq.Promotion.Domain.Entities.TmapEvent;
using Finnq.Promotion.Domain.Identity;
using Finnq.Promotion.Domain.Infrastructures;

namespace Finnq.Promotion.Domain.Repositories.TmapEvent {
    public class TmapEventEntryRepository : RepositoryBase<AppUser, AppDbContext, TmapEventEntry>, ITmapEventEntryRepository {
        public TmapEventEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
