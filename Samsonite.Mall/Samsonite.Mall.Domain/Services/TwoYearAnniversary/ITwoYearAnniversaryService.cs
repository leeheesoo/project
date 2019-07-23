using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Domain.Services.TwoYearAnniversary {
    public interface ITwoYearAnniversaryService {
        TwoYearAnniversaryEntry CreateTwoYearAnniversaryEntry(TwoYearAnniversaryEntry entity);
        TwoYearAnniversaryWinCoupon UpdateTwoYearAnniversaryWinCoupon(long entryId);
        TwoYearAnniversaryInstantPrizeSetting CreateTwoYearAnniversaryPrize(TwoYearAnniversaryInstantPrizeSetting entry);
        IQueryable<TwoYearAnniversaryInstantPrizeSetting> GetTwoYearAnniversaryPrizeList();
        TwoYearAnniversaryInstantPrizeSetting UpdateTwoYearAnniversaryPrize(TwoYearAnniversaryInstantPrizeSetting entry);
        IQueryable<TwoYearAnniversaryEntry> SelectTwoYearAnniversaryEnty();
        IList<TwoYearAnniversaryEntry> GetOneYearAnniversaryEntryAll();

    }
}
