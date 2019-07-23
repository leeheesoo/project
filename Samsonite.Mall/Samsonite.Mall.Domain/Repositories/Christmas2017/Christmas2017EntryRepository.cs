using Samsonite.Mall.Domain.Entities.Christmas2017;
using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Repositories.Christmas2017 {
    public class Christmas2017EntryRepository : RepositoryBase<AppUser, AppDbContext, Christmas2017Entry>, IChristmas2017EntryRepository {
        public Christmas2017EntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
