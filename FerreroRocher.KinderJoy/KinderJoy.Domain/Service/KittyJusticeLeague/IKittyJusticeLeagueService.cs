using KinderJoy.Domain.Entities.KittyJusticeLeague;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.KittyJusticeLeague {
    public interface IKittyJusticeLeagueService {
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        KittyJusticeLeagueInstantLottery CreateInstantLottery(KittyJusticeLeagueInstantLottery entry, bool isChecked = false);
        /// <summary>
        /// 경품 당첨 후 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        KittyJusticeLeagueInstantLottery UpdateInstantLotteryWinner(KittyJusticeLeagueInstantLottery entry);
        /// <summary>
        /// 경품 당첨 유저 탐색
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        KittyJusticeLeagueInstantLottery GetInstantLotteryWinner(long id);
        /// <summary>
        /// 관리자 경품 세팅 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        KittyJusticeLeagueInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(KittyJusticeLeagueInstantLotteryPrizeSetting entry);
        /// <summary>
        /// 관리자 경품 수정
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        KittyJusticeLeagueInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, KittyJusticeLeagueInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<KittyJusticeLeagueInstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList();
        IQueryable<KittyJusticeLeagueInstantLottery> GetAdminKittyJusticeLeagueEntryList(KittyJusticeLeagueQueryOptions option);
    }
}
