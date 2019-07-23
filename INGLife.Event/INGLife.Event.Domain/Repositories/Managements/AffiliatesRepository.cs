using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.Managements {
    public class AffiliatesRepository : RepositoryBase<AppUser, AppDbContext, Affiliates>, IAffiliatesRepository {
        public AffiliatesRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
