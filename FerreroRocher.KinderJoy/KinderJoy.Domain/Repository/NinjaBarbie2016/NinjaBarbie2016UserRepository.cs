using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Identity;
using Microsoft.Owin.Security;

namespace KinderJoy.Domain.Repository.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 - SNS공유 참여자 Repository
    /// </summary>
    public class NinjaBarbie2016UserRepository : RepositoryBase<AppUser, AppDbContext, NinjaBarbie2016User>, INinjaBarbie2016UserRepository {
        public NinjaBarbie2016UserRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name) {
        }
    }
}
