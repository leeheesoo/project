using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.MarketingAgree {
    public class OverFortyFiveDbRepository : RepositoryBase<AppUser, AppDbContext, OverFortyFiveDbEntry>, IOverFortyFiveDbEntryRepository
    {
        public OverFortyFiveDbRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
