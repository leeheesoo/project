using INGLife.Event.Domain.Entities.KidsNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.KidsNote {
    public interface IKidsNoteService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        KidsNoteEntry CreateKidsNoteEntry(KidsNoteEntry entry);
        /// <summary>
        /// 본인인증 정보를 제외한 정보 update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        KidsNoteEntry UpdateKidsNoteEntry(KidsNoteEntry entry);
        /// <summary>
        /// 특정 응모정보 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        KidsNoteEntry GetKidsNoteEntryById(long id);
        /// <summary>
        /// 이벤트타입별 선착순 제어
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        bool IsEntry(KidsNoteEventType eventType, int limit);
        IQueryable<KidsNoteEntry> GetAdminKidsNoteEntryList();
    }
}
