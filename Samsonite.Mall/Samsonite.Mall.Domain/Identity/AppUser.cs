using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Samsonite.Mall.Domain.Identity {
    public class AppUser : IdentityUser<long, AppUserLogin, AppUserRole, AppUserClaim>, IUser<long> {
    }
}