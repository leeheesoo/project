using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Repositories.BagtotheFuture {
    public class BagtotheFutureSnsRepository : RepositoryBase<AppUser, AppDbContext, BagtotheFutureSns>, IBagtotheFutureSnsRepository {
        public BagtotheFutureSnsRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
