using KinderJoy.Domain.Abstract.Christmas2015;
using KinderJoy.Domain.Entities;
using KinderJoy.Domain.Entities.Christmas2015;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Repository.Christmas2015 {
    public interface IChristmas2015Repository {
        #region [C]reate
        /// <summary>
        /// [즉석당첨 이벤트] 생성
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isOK"></param>
        /// <returns></returns>
        Christmas2015Win CreateChristmas2015Win(Christmas2015Win entry, bool isOK);
        /// <summary>
        /// [즉석당첨 이벤트] 경품 셋팅
        /// </summary>
        /// <param name="saveDatas"></param>
        void CreateChristmas2015WinSettings(List<Dictionary<string, object>> saveDatas);
        /// <summary>
        /// [트리만들기 이벤트] 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Christmas2015MakeTree CreateChristmas2015MakeTree(Christmas2015MakeTree entry);
        /// <summary>
        /// [트리만들기 이벤트] SNS공유 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        void CreateChristmas2015MakeTreeSNSShare(Christmas2015MakeTreeSNSShare entry);
        #endregion

        #region [R]ead
        /// <summary>
        /// [즉석당첨 이벤트] 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        IList<Christmas2015Win> GetChristmas2015Win();
        /// <summary>
        /// [즉석당첨 이벤트] 경품,확률 셋팅정보 가져오기
        /// </summary>
        /// <returns></returns>
        IList<Christmas2015WinPrizeSetting> GetChristmas2015WinPrizeSettings();
        /// <summary>
        /// [트리만들기 이벤트] SNS공유 가능 횟수 가져오기
        /// </summary>
        /// <param name="snsType"></param>
        /// <param name="mobile"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        int GetSNSShareCountByMobile(string snsType, string mobile, string today);
        /// <summary>
        /// 현재날짜
        /// </summary>
        DateTime Now { get; }

        IQueryable<Christmas2015WinPrizeSetting> Christmas2015WinPrizeSettings { get; }
        IQueryable<Christmas2015Win> Christmas2015Wins { get; }

        IQueryable<Christmas2015MakeTree> Christmas2015MakeTree { get; }
        IQueryable<Christmas2015MakeTreeSNSShare> Christmas2015MakeTreeSNSShares { get; }
        #endregion

        #region [U]date
        /// <summary>
        /// [즉석당첨 이벤트] 경품 당첨인 경우 개인정보 Update
        /// </summary>
        /// <param name="entry"></param>
        void UpdateChristmas2015WinById(Christmas2015Win entry);
        /// <summary>
        /// [즉석당첨 이벤트] 총수량 Update
        /// </summary>
        /// <param name="entry"></param>
        void UpdateChristmas2015WinTotalCount(Christmas2015WinPrizeSetting entry);
        /// <summary>
        /// [즉석당첨 이벤트] 시작시간 Update
        /// </summary>
        /// <param name="entry"></param>
        void UpdateChristmas2015WinStartTime(Christmas2015WinPrizeSetting entry);
        /// <summary>
        /// [즉석당첨 이벤트] 확률 Update
        /// </summary>
        /// <param name="entry"></param>
        void UpdateChristmas2015WinRate(Christmas2015WinPrizeSetting entry);
        /// <summary>
        /// [트리만들기 이벤트] 장난감 정보,합성이미지 Update
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Christmas2015MakeTree UpdateChristmas2015MakeTreeById(Christmas2015MakeTree entry);
        #endregion

        #region [D]elete
        #endregion
    }
}
