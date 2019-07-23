using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Identity;
using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Repositories.RouletteEvent {
    public class RouletteEventPrizeSettingRepository : RepositoryBase<AppUser, AppDbContext, RouletteEventInstantLotteryPrizeSetting>, IRouletteEventPrizeSettingRepository {
        public RouletteEventPrizeSettingRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
