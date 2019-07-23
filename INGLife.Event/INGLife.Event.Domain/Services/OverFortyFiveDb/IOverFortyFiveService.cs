using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.OverFortyFiveDb
{
    public interface IOverFortyFiveService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        OverFortyFiveDbEntry CreateOverFortyFiveEntry(OverFortyFiveDbEntry entry);

        int getEntryCount();

        /// <summary>
        /// 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        OverFortyFiveDbEntry UpdateOverFortyFiveEntry(OverFortyFiveDbEntry entry);
        /// <summary>
        /// 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OverFortyFiveDbEntry GetOverFortyFiveEntryById(long id);

        int GetEmoticonTypeCount(EmoticonType emoticonType);
        
        IQueryable<OverFortyFiveDbEntry> GetAdminOverFortyFiveEntryList();
    }
}
