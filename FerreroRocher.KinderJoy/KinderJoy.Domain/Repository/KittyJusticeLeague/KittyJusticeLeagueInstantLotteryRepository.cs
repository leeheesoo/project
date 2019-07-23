using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.KittyJusticeLeague {
    public class KittyJusticeLeagueInstantLotteryRepository : RepositoryBase<AppUser, AppDbContext, KittyJusticeLeagueInstantLottery>, IKittyJusticeLeagueInstantLotteryRepository {
        public KittyJusticeLeagueInstantLotteryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
