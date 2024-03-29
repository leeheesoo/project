﻿using System;
using KinderJoy.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace KinderJoy.Domain.Infrastructure {
    public class AppRoleManager : RoleManager<AppRole, long>, IDisposable {
        public AppRoleManager(RoleStore<AppRole, long, AppUserRole> store)
            : base(store) {
        }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context) {
            return new AppRoleManager(new RoleStore<AppRole, long, AppUserRole>(context.Get<AppDbContext>()));
        }
    }
}
