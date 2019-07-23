using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Entities.Pororo2018;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.Pororo2018 {
    public interface IPororo2018Service {
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        Pororo2018InstantLottery CreateInstantLottery(Pororo2018InstantLottery entry, bool isChecked = false);
        /// <summary>
        /// 경품 당첨 후 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        Pororo2018InstantLottery UpdateInstantLotteryWinner(Pororo2018InstantLottery entry);
        /// <summary>
        /// 경품 당첨 유저 탐색
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Pororo2018InstantLottery GetInstantLotteryWinner(long id);
        /// <summary>
        /// 관리자 경품 세팅
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Pororo2018InstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(Pororo2018InstantLotteryPrizeSetting entry);
        /// <summary>
        /// 관리자 경품 수정
        /// </summary>
        /// <param name="date"></param>
        /// <param name="prizeType"></param>
        /// <param name="totalCount"></param>
        /// <param name="startTime"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        Pororo2018InstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, Pororo2018InstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<Pororo2018InstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList();
        IQueryable<Pororo2018InstantLottery> GetAdminPororo2018InstatLotteryList(Pororo2018QueryOptions option);
    }
}
