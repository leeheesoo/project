using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Identity {
    public class AppUser : IdentityUser<long, AppUserLogin, AppUserRole, AppUserClaim>, IUser<long> {
    }
}
