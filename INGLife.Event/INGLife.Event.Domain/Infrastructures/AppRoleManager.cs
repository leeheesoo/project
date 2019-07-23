using INGLife.Event.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Infrastructures {
    public class AppRoleManager : RoleManager<AppRole, long> {
        public AppRoleManager(IRoleStore<AppRole, long> store) : base(store) {
        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context) {
            return new AppRoleManager(new RoleStore<AppRole, long, AppUserRole>(context.Get<AppDbContext>()));
        }
    }
}
