using KinderJoy.Domain.Entities.ChildrensDay;
using KinderJoy.Domain.Entities.MainStream;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Models.MainStream;

namespace KinderJoy.Domain.Service.MainStream {
    public interface IMainStreamService {

        long CreateEntry(MainStreamSurprise entry);

        long CreateEntrySns(MainStreamSurpriseSNS sns);

        IQueryable<MainStreamSurprise> MainStreamSurprises { get; }
        IQueryable<MainStreamSurpriseSNS> MainStreamSurpriseSNSs { get; }

        IQueryable<AdminMainStreamSnsStatsViewModel> GetMainStreamSurpriseSnsStats();
    }
}
