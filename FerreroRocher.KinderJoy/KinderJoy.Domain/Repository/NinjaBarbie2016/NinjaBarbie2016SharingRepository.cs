using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using Microsoft.Owin.Security;

namespace KinderJoy.Domain.Repository.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 - SNS공유 Repository
    /// </summary>
    public class NinjaBarbie2016SharingRepository : RepositoryBase<AppUser, AppDbContext, NinjaBarbie2016Sharing>, INinjaBarbie2016SharingRepository {
        public NinjaBarbie2016SharingRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name) {
        }
    }
}