using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.KittyJusticeLeague {
    public class KittyJusticeLeagueInstantLotteryPrizeSettingRepository : RepositoryBase<AppUser, AppDbContext, KittyJusticeLeagueInstantLotteryPrizeSetting>, IKittyJusticeLeagueInstantLotteryPrizeSettingRepository {
        public KittyJusticeLeagueInstantLotteryPrizeSettingRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
