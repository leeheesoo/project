﻿using KinderJoy.Domain.Entities.MavelFrozen;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.MavelFrozen {
    public class MavelFrozenSnsRepository:RepositoryBase<AppUser, AppDbContext, MavelFrozenSNS>, IMavelFrozenSnsRepository {
        public MavelFrozenSnsRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name) { }
    }
}
