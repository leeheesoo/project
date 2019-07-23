using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.MamboEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Nilririmambo {
    public interface INilririmamboService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        NilririmanboEntry CreateNilririmamboEntry(NilririmanboEntry entry);

        IQueryable<NilririmanboEntry> GetAdminNilririmamboEntryList();
    }
}
