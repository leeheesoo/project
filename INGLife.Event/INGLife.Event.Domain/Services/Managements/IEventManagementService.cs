using INGLife.Event.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Managements {
    public interface IEventManagementService {

        /// <summary>
        /// 이벤트 추가하기
        /// </summary>
        /// <param name="entry"></param>
        void CreateEventManagement(EventManagement entry);

        /// <summary>
        /// 이벤트목록 가져오기
        /// </summary>
        IList<EventManagement> EventManagements { get; }
    }
}
