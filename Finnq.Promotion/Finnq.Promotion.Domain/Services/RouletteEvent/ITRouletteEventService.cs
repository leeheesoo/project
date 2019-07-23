using Finnq.Promotion.Domain.Entities.RouletteEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.RouletteEvent {
    public interface ITRouletteEventService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        TRouletteEventEntry CreateTRouletteEventEntry(TRouletteEventEntry entry);
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TRouletteEventEntry UpdateTRouletteEventEntry(long id);
        /// <summary>
        /// 관리자 즉석당첨 경품 등록
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TRouletteEventInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(TRouletteEventInstantLotteryPrizeSetting data);
        /// <summary>
        /// 관리자 즉석당첨 경품 수정
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TRouletteEventInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, TRouletteEventInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<TRouletteEventInstantLotteryPrizeSetting> GetAdminTRouletteEventInstantLotteryPrizeSettingList();
        IQueryable<TRouletteEventEntry> GetAdminTRouletteEventEntryList(TRouletteEventQueryOptions options);
    }
}
