using Finnq.Promotion.Domain.Entities.RouletteEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Services.RouletteEvent {
    public interface IRouletteEventService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        RouletteEventEntry CreateRouletteEventEntry(RouletteEventEntry entry);
        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RouletteEventEntry UpdateRouletteEventEntry(long id);
        /// <summary>
        /// 관리자 즉석당첨 경품 등록
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        RouletteEventInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(RouletteEventInstantLotteryPrizeSetting data);
        /// <summary>
        /// 관리자 즉석당첨 경품 수정
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        RouletteEventInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, RouletteEventInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate);
        IQueryable<RouletteEventInstantLotteryPrizeSetting> GetAdminRouletteEventInstantLotteryPrizeSettingList();
        IQueryable<RouletteEventEntry> GetAdminRouletteEventEntryList(RouletteEventQueryOptions options);
    }
}
