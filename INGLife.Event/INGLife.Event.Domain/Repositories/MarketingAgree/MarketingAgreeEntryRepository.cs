using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.MarketingAgree {
    public class MarketingAgreeEntryRepository : RepositoryBase<AppUser, AppDbContext, MarketingAgreeEntry>, IMarketingAgreeEntryRepository {
        public MarketingAgreeEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
