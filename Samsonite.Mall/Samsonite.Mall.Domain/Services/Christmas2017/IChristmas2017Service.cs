using Samsonite.Mall.Domain.Entities.Christmas2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Services.Christmas2017 {
    public interface IChristmas2017Service {
        Christmas2017Entry CreateChristmas2017Entry(Christmas2017Entry entry);
        int GetChristmasTielonnCount { get; }
        int GetChristmasSupremeLiteCount { get; }
        int GetChristmasPlumeBasicCount { get; }
        int GetChristmasMarlonCount { get; }
        IQueryable<Christmas2017Entry> GetAdminChristmasEntryList();
    }
}
