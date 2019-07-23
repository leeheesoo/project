using KinderJoy.Domain.Entities.Pororo2018;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.Pororo2018 {
    public class Pororo2018InstantLotteryRepository : RepositoryBase<AppUser, AppDbContext, Pororo2018InstantLottery>, IPororo2018InstantLotteryRepository {
        public Pororo2018InstantLotteryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
