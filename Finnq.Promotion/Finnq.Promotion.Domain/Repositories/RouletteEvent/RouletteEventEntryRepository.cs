using Finnq.Promotion.Domain.Identity;
using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using System.Linq.Expressions;

namespace Finnq.Promotion.Domain.Repositories.RouletteEvent {
    public class RouletteEventEntryRepository : RepositoryBase<AppUser, AppDbContext, RouletteEventEntry>, IRouletteEventEntryRepository {
        public RouletteEventEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
