using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Repositories.MarketingAgree {
    public interface ITumblerEventRepository : IRepository<TumblerEventEntry> {
    }
}
