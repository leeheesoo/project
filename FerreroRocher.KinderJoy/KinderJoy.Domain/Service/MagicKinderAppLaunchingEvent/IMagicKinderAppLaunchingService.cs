using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Models.MagicKinderAppLaunchingEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent
{
    public interface IMagicKinderAppLaunchingService
    {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MagicKinderAppLaunching CreateMagicKinderAppLaunching(MagicKinderAppLaunching entry);
        /// <summary>
        /// 노출여부 수정
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MagicKinderAppLaunching UpdateIsShowMagicKinderAppLaunching(long id);
        /// <summary>
        /// 이벤트 참여 현황
        /// </summary>
        /// <returns></returns>
        IQueryable<MagicKinderAppLaunching> GetMagicKinderAppLaunching();
        /// <summary>
        /// 관리자 리스트
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IQueryable<MagicKinderAppLaunching> GetAdminMagicKinderAppLaunching(MagicKinderAppLaunchingQueryOptions options);
        /// <summary>
        /// 관리자 통계 리스트
        /// </summary>
        /// <returns></returns>
        IQueryable<AdminMagicKinderStatsViewModel> GetStatistics();
    }
}
