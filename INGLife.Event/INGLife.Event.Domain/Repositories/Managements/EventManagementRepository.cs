using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.Managements {
    public class EventManagementRepository : RepositoryBase<AppUser, AppDbContext, EventManagement>, IEventManagementRepository {
        public EventManagementRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
