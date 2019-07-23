using INGLife.Event.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Managements {
    public interface IAffiliatesService {
        IList<Affiliates> GetAffiliates { get; }
    }
}
