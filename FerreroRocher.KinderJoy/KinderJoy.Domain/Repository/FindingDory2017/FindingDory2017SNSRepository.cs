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
    public class FindingDory2017SNSRepository : RepositoryBase<AppUser, AppDbContext, FindingDory2017SNS>, IFindingDory2017SNSRepository
    {
        public FindingDory2017SNSRepository(AppDbContext db, IAuthenticationManager authManager) : base(db, authManager?.User?.Identity?.Name)
        {
        }
    }
}
