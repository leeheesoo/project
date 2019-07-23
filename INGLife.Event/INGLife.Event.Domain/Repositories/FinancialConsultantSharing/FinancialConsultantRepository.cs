using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.FinancialConsultantSharing
{
    public class FinancialConsultantRepository : RepositoryBase<AppUser, AppDbContext, FinancialConsultantEntry>, IFinancialConsultantRepository
    {
        public FinancialConsultantRepository(AppDbContext db, object currentUser) : base(db, currentUser)
        {
        }
    }
}
