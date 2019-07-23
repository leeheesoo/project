using AutoMapper;
using Samsonite.Mall.Domain.Identity;
using NLog;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Samsonite.Mall.Domain.Entities.Abstract;
using System.Collections.Generic;

namespace Samsonite.Mall.Domain.Infrastructures {
    public class InstantLotteryPrizeSelector<TPrizeSetting, TType> where TPrizeSetting : InstantLotteryPrizeSetting<TType>
    {
        public TType SelectPrize(List<TPrizeSetting> prizes, TType loser, DateTime now)
        {
            //경품 내역 가져오기<시작시간, 확률 확인>
            var totalSeed = 0.0f;
            var seed = (float)new Random().NextDouble();
            var winnerPrize = loser;
            foreach (var prize in prizes)
            {
                totalSeed += prize.Rate;
                if (totalSeed > seed)
                {
                    // 당첨
                    winnerPrize = prize.PrizeType;
                    break;
                }
            }
            return winnerPrize;
        }
    }
}