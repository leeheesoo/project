using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.Rebranding {
    public class RebrandingConsultingAgreeEntryRepository : RepositoryBase<AppUser, AppDbContext, RebrandingConsultingAgreeEntry>, IRebrandingConsultingAgreeEntryRepository {
        public RebrandingConsultingAgreeEntryRepository(AppDbContext db, object currentUser) : base(db, currentUser) {
        }
    }
}
