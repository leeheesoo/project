using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.FindingDory2017
{
    public class FindingDory2017UserRepository : RepositoryBase<AppUser, AppDbContext, FindingDory2017User>, IFindingDory2017UserRepository
    {
        public FindingDory2017UserRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name)
        {
        }
    }
}
