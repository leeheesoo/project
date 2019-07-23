using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Identity;
using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Repositories.TWorldRouletteEvent {
    public class TWorldRouletteEventPrizeSettingRepository : RepositoryBase<AppUser, AppDbContext, TWorldRouletteEventInstantLotteryPrizeSetting>, ITWorldRouletteEventPrizeSettingRepository {
        public TWorldRouletteEventPrizeSettingRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
