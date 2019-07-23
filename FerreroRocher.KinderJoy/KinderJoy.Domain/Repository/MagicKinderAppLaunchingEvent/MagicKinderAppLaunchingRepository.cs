using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.MagicKinderAppLaunchingEvent
{
    public class MagicKinderAppLaunchingRepository : RepositoryBase<AppUser, AppDbContext, MagicKinderAppLaunching>, IMagicKinderAppLaunchingRepository
    {
        public MagicKinderAppLaunchingRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name)
        {
        }
    }
}
