using KinderJoy.Domain.Entities.MainStream;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.MainStream {
    public interface IMainStreamRepository {

        IQueryable<MainStreamSurprise> MainStreamSurprises { get; }
        IQueryable<MainStreamSurpriseSNS> MainStreamSurpriseSNSs { get; }

        /// <summary>
        /// 응모저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        long CreateEntry(MainStreamSurprise entry);

        /// <summary>
        /// sns공유 저장
        /// </summary>
        /// <param name="sns"></param>
        /// <returns></returns>
        long CreateEntrySns(MainStreamSurpriseSNS sns);

    }
}
