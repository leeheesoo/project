using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract {
    public class EventNaverPrizeSelector<TPrizeSetting, TType> where TPrizeSetting : EventNaverSearchingSettings<TType> {
        public EventNaverPrizeSelector() {
        }

        public TType SelectPrize(List<TPrizeSetting> prizes, TType loser, DateTime now) {

            Random rnd = new Random();
            float percent = (float)rnd.NextDouble();

            // 경품목록 확인
            prizes = prizes.Where(x => x.Date == now.Date && x.StartTime <= now.Hour && x.TotalCount > x.WinnerCount && x.Rate > 0.0f).ToList();

            //경품 내역 가져오기<시작시간, 확률 확인>
            var totalSeed = 0.0f;
            var seed = (float)new Random().NextDouble();
            var winnerPrize = loser;
            foreach (var prize in prizes) {
                totalSeed += prize.Rate;
                if (totalSeed > seed) {
                    // 당첨
                    winnerPrize = prize.PrizeType;
                    break;
                }
            }
            return winnerPrize;
        }
    }
}
