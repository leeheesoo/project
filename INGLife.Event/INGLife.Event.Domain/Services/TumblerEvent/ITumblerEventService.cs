using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.TumblerEvent
{
    public interface ITumblerEventService
    {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        TumblerEventEntry Create(TumblerEventEntry entry);

        TumblerEventEntry CheckEntry(string name, string mobile, string gender, string birthDay);

        int getEventTypeCount(string eventType);

        //int getEntryCount();

        ///// <summary>
        ///// 본인인증 정보를 제외한 정보 update
        ///// </summary>
        ///// <param name="entry"></param>
        ///// <returns></returns>
        //OverFortyFiveDbEntry UpdateOverFortyFiveEntry(OverFortyFiveDbEntry entry);
        ///// <summary>
        ///// 특정 응모정보 가져오기
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //OverFortyFiveDbEntry GetOverFortyFiveEntryById(long id);

        //int GetEmoticonTypeCount(EmoticonType emoticonType);

        IQueryable<TumblerEventEntry> GetAdminTumblerEventEntryList(string eventType);
    }
}
