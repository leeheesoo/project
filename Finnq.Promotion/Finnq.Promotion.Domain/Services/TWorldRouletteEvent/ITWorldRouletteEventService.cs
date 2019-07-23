using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.TWorldRouletteEvent {
    public interface ITWorldRouletteEventService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        TWorldRouletteEventEntry CreateTWorldRouletteEventEntry(TWorldRouletteEventEntry entry);
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TWorldRouletteEventEntry UpdateTWorldRouletteEventEntry(long id);
        /// <summary>
        /// 관리자 즉석당첨 경품 등록
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TWorldRouletteEventInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(TWorldRouletteEventInstantLotteryPrizeSetting data);
        /// <summary>
        /// 관리자 즉석당첨 경품 수정
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TWorldRouletteEventInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, TWorldRouletteEventInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<TWorldRouletteEventInstantLotteryPrizeSetting> GetAdminTWorldRouletteEventInstantLotteryPrizeSettingList();
        IQueryable<TWorldRouletteEventEntry> GetAdminTWorldRouletteEventEntryList(TWorldRouletteEventQueryOptions options);
    }
}
