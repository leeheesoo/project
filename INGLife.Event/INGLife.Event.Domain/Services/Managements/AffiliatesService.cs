using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Repositories.Managements;

namespace INGLife.Event.Domain.Services.Managements {
    public class AffiliatesService : IAffiliatesService {
        private IAffiliatesRepository affiliatesRepository;
        public AffiliatesService(IAffiliatesRepository affiliatesRepository) {
            this.affiliatesRepository = affiliatesRepository;
        }
        public IList<Affiliates> GetAffiliates {
            get {
                return affiliatesRepository.GetAll().OrderByDescending(x => x.Name).ToList();
            }
        }
    }
}
