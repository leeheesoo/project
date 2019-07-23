using KinderJoy.Domain.Entities.DisneyStarWars2018;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.DisneyStarWars2018 {
    public class DisneyStarWars2018InstantLotteryRepository : RepositoryBase<AppUser, AppDbContext, DisneyStarWars2018InstantLottery>, IDisneyStarWars2018InstantLotteryRepository {
        public DisneyStarWars2018InstantLotteryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
