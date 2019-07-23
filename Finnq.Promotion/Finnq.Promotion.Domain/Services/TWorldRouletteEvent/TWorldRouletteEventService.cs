﻿using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Exceptions;
using Finnq.Promotion.Domain.Infrastructures;
using Finnq.Promotion.Domain.Repositories.RouletteEvent;
using Finnq.Promotion.Domain.Repositories.TWorldRouletteEvent;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Finnq.Promotion.Domain.Services.TWorldRouletteEvent {
    public class TWorldRouletteEventService : ITWorldRouletteEventService {
        private ITWorldRouletteEventEntryRepository entryRepo;
        private ITWorldRouletteEventPrizeSettingRepository settingRepo;
        private ITimeProvider time;

        private InstantLotteryPrizeSelector<TWorldRouletteEventInstantLotteryPrizeSetting, TWorldRouletteEventInstantLotteryPrizeType> prizeSelector;

        public TWorldRouletteEventService(ITWorldRouletteEventEntryRepository entryRepo, ITWorldRouletteEventPrizeSettingRepository settingRepo, ITimeProvider time) {
            this.entryRepo = entryRepo;
            this.settingRepo = settingRepo;
            this.time = time;

            this.prizeSelector = new InstantLotteryPrizeSelector<TWorldRouletteEventInstantLotteryPrizeSetting, TWorldRouletteEventInstantLotteryPrizeType>();
        }
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public TWorldRouletteEventEntry CreateTWorldRouletteEventEntry(TWorldRouletteEventEntry entry) {
            if (entry.PrizeType.HasValue) {
                throw new TWorldRouletteServiceException("400", "정상적인 경로로 이벤트에 참여해주세요.", null);
            }

            var isEntryChance = entryRepo.Any(x => x.Mobile == entry.Mobile && x.PrizeType.HasValue);

            if (isEntryChance) {
                throw new TWorldRouletteServiceException("400", "해당번호로 중복참여가 불가능합니다.", entry.Mobile);
            }
            var result = entryRepo.Add(entry);
            entryRepo.Save();
            return result;
        }

        /// <summary>
        /// 즉석당첨 응모
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TWorldRouletteEventEntry UpdateTWorldRouletteEventEntry(long id) {
            var entry = entryRepo.SingleOrDefault(x => x.Id == id && !x.PrizeType.HasValue);
            if (entry == null) {
                throw new TWorldRouletteServiceException("400", "정상적인 경로로 이벤트를 참여해주세요.", null);
            }

            using (var tx = new TransactionScope()) {
                var now = time.Now;
                entry.PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Loser;

                var prizes = settingRepo.Filter(x => x.Date == now.Date && x.StartTime <= now.Hour && x.TotalCount > x.WinnerCount && x.Rate > 0.0f).ToList();
                entry.PrizeType = prizeSelector.SelectPrize(prizes, TWorldRouletteEventInstantLotteryPrizeType.Loser, now);
                if (entry.PrizeType != TWorldRouletteEventInstantLotteryPrizeType.Loser) {
                    var prize = settingRepo.SingleOrDefault(x => x.Date == now.Date && x.PrizeType == entry.PrizeType);
                    if (prize == null) {
                        entry.PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Loser;
                    } else {
                        prize.WinnerCount = entryRepo.Filter(x => x.CreateDate >= now.Date && x.PrizeType == entry.PrizeType).Count() + 1;
                        settingRepo.Update(prize);
                    }
                }

                entry.UpdateDate = now;
                var user = entryRepo.Update(entry);
                entryRepo.Save();
                tx.Complete();

                return user;
            }

        }

        /// <summary>
        /// 관리자 즉석당첨 경품 등록
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TWorldRouletteEventInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(TWorldRouletteEventInstantLotteryPrizeSetting data) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var isOverlapPrize = settingRepo.Filter(x => x.Date == data.Date && x.PrizeType == data.PrizeType).SingleOrDefault();
            if (isOverlapPrize != null) {
                throw new TWorldRouletteServiceException("400","이미 등록된 데이터가 있습니다.",null);
            }
            var result = settingRepo.Add(data);
            settingRepo.Save();

            return result;
        }
        /// <summary>
        /// 관리자 즉석당첨 경품 수정
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TWorldRouletteEventInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, TWorldRouletteEventInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var updateEntry = settingRepo.SingleOrDefault(x => x.Date == date && x.PrizeType == prizeType);
            if (updateEntry == null) {
                throw new TWorldRouletteServiceException("400","수정할 데이터가 없습니다.",null);
            }
            if (updateEntry.WinnerCount > totalCount) {
                throw new TWorldRouletteServiceException("400", "경품수량은 당첨수량보다 적게 세팅할 수 없습니다.", totalCount);
            }
            if (totalCount <= 0) {
                throw new TWorldRouletteServiceException("400", "경품수량은 0이하로 세팅할 수 없습니다.", totalCount);
            }
            if(startTime < 0 || startTime >=24) {
                throw new TWorldRouletteServiceException("400", "당첨 시작시간은 0~23 까지 가능합니다.", startTime);
            }
            if(rate <=0) {
                throw new TWorldRouletteServiceException("400", "당첨확률은 0이하로 세팅할 수 없습니다.", rate);
            }
            updateEntry.TotalCount = totalCount;
            updateEntry.StartTime = startTime;
            updateEntry.Rate = rate;

            var result = settingRepo.Update(updateEntry);
            settingRepo.Save();

            return result;
        }
        public IQueryable<TWorldRouletteEventInstantLotteryPrizeSetting> GetAdminTWorldRouletteEventInstantLotteryPrizeSettingList() {
            return settingRepo.GetAll().OrderBy(x => x.Date);
        }

        public IQueryable<TWorldRouletteEventEntry> GetAdminTWorldRouletteEventEntryList(TWorldRouletteEventQueryOptions option) {
            return entryRepo.GetAll().AsExpandable().Where(option.BuildPredicate()).OrderByDescending(x => x.CreateDate);
        }
    }

    public class TWorldRouletteServiceException : EventServiceException {
        public TWorldRouletteServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
