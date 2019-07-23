using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Repositories.TWorldRouletteEvent {
    public interface ITWorldRouletteEventPrizeSettingRepository : IRepository<TWorldRouletteEventInstantLotteryPrizeSetting> {
    }
}
