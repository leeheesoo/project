using KinderJoy.Domain.Entities.DisneyStarWars2018;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.RepositoryDisneyStarWars2018;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.DisneyStarWars2018{
    public class DisneyStarWars2018InstantLotteryPrizeSettingRepository : RepositoryBase<AppUser, AppDbContext, DisneyStarWars2018InstantLotteryPrizeSetting>, IDisneyStarWars2018InstantLotteryPrizeSettingRepository {
        public DisneyStarWars2018InstantLotteryPrizeSettingRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
