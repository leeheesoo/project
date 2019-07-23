using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.MarketingAgree {
    public class TumblerEventRepository : RepositoryBase<AppUser, AppDbContext, TumblerEventEntry>, ITumblerEventRepository
    {
        public TumblerEventRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
