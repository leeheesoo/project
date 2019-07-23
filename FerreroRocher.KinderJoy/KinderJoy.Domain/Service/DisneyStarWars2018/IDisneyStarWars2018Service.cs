using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Entities.DisneyStarWars2018;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.DisneyStarWars2018 {
    public interface IDisneyStarWars2018Service {
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        DisneyStarWars2018InstantLottery CreateInstantLottery(DisneyStarWars2018InstantLottery entry, bool isChecked = false);
        /// <summary>
        /// 경품 당첨 후 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        DisneyStarWars2018InstantLottery UpdateInstantLotteryWinner(DisneyStarWars2018InstantLottery entry);
        /// <summary>
        /// 경품 당첨 유저 탐색
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DisneyStarWars2018InstantLottery GetInstantLotteryWinner(long id);
        /// <summary>
        /// 관리자 경품 세팅
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        DisneyStarWars2018InstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(DisneyStarWars2018InstantLotteryPrizeSetting entry);
        /// <summary>
        /// 관리자 경품 수정
        /// </summary>
        /// <param name="date"></param>
        /// <param name="prizeType"></param>
        /// <param name="totalCount"></param>
        /// <param name="startTime"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        DisneyStarWars2018InstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, DisneyStarWars2018InstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<DisneyStarWars2018InstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList();
        IQueryable<DisneyStarWars2018InstantLottery> GetAdminDisneyStarWars2018InstatLotteryList(DisneyStarWars2018QueryOptions option);
    }
}
