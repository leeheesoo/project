using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Repositories.BagtotheFuture {
    public class BagtotheFutureSnsUserRepository : RepositoryBase<AppUser, AppDbContext, BagtotheFutureSnsUser>, IBagtotheFutureSnsUserRepository {
        public BagtotheFutureSnsUserRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
