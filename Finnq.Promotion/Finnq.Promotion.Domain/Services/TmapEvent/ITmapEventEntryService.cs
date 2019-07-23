using Finnq.Promotion.Domain.Entities.TmapEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.TmapEvent {
    public interface ITmapEventEntryService {
        TmapEventEntry CreateTmapEventEntry(TmapEventEntry entry);
        IQueryable<TmapEventEntry> FilterTmapEventEntry(TmapEventEntryQueryOptions options);
    }
}
