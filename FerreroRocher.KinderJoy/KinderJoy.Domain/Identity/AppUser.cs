﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Identity
{
    public class AppUser : IdentityUser<long, AppUserLogin, AppUserRole, AppUserClaim>, IUser<long>
    {
    }
}
