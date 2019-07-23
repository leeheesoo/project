using Microsoft.AspNet.Identity.EntityFramework;

namespace INGLife.Event.Domain.Identity {
    public class AppRole : IdentityRole<long, AppUserRole> {
        public AppRole() : base() { }
        public AppRole(string name) : this() { Name = name; }
    }
}

