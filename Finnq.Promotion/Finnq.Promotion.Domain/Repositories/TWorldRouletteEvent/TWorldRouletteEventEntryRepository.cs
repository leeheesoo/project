using Finnq.Promotion.Domain.Identity;
using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using System.Linq.Expressions;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;

namespace Finnq.Promotion.Domain.Repositories.TWorldRouletteEvent {
    public class TWorldRouletteEventEntryRepository : RepositoryBase<AppUser, AppDbContext, TWorldRouletteEventEntry>, ITWorldRouletteEventEntryRepository {
        public TWorldRouletteEventEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
