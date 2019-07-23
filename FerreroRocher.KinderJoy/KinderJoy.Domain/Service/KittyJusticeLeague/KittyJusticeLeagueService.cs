using KinderJoy.Domain.Abstract;
using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Repository.KittyJusticeLeague;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KinderJoy.Domain.Service.KittyJusticeLeague {
    public class KittyJusticeLeagueService : IKittyJusticeLeagueService {
        private IKittyJusticeLeagueInstantLotteryRepository repo;
        private IKittyJusticeLeagueInstantLotteryPrizeSettingRepository settingRepo;
        private ITimeProvider time;

        private EventNaverPrizeSelector<KittyJusticeLeagueInstantLotteryPrizeSetting, KittyJusticeLeagueInstantLotteryPrizeType> prizeSelector;

        public KittyJusticeLeagueService(IKittyJusticeLeagueInstantLotteryRepository repo, IKittyJusticeLeagueInstantLotteryPrizeSettingRepository settingRepo,ITimeProvider time) {
            this.repo = repo;
            this.settingRepo = settingRepo;
            this.time = time;

            this.prizeSelector = new EventNaverPrizeSelector<KittyJusticeLeagueInstantLotteryPrizeSetting, KittyJusticeLeagueInstantLotteryPrizeType>();
        }

        public KittyJusticeLeagueInstantLottery CreateInstantLottery(KittyJusticeLeagueInstantLottery entry, bool isChecked = false) {
            using (var tx = new TransactionScope()) {
                var now = time.Now;

                var isWin = repo.Any(x => x.PrizeType != KittyJusticeLeagueInstantLotteryPrizeType.Loser && x.IpAddress == entry.IpAddress);

                entry.PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser;
                if (isChecked && !isWin) {
                    var prizes = settingRepo.Filter(x => x.Date == now.Date && x.StartTime <= now.Hour && x.TotalCount > x.WinnerCount && x.Rate > 0.0f).ToList();
                    entry.PrizeType = prizeSelector.SelectPrize(prizes, KittyJusticeLeagueInstantLotteryPrizeType.Loser, now);

                    if (entry.PrizeType != KittyJusticeLeagueInstantLotteryPrizeType.Loser) {
                        var prize = settingRepo.SingleOrDefault(x => x.Date == now.Date && x.PrizeType == entry.PrizeType);
                        if (prize == null) {
                            entry.PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser;
                        } else {
                            prize.WinnerCount = repo.Filter(x => x.CreateDate >= now.Date && x.PrizeType == entry.PrizeType).Count() + 1;
                            settingRepo.Update(prize);
                        }
                    }
                }

                entry.CreateDate = now;
                var user = repo.Add(entry);
                repo.Save();

                tx.Complete();

                return user;
            }
        }
        public KittyJusticeLeagueInstantLottery UpdateInstantLotteryWinner(KittyJusticeLeagueInstantLottery entry) {
            var isWin = repo.Any(x => x.PrizeType != KittyJusticeLeagueInstantLotteryPrizeType.Loser && x.Mobile == entry.Mobile);
            if (isWin) {
                throw new KittyJusticeLeagueServiceException("400", "이미 당첨된 내역이 존재합니다.당첨된 분에게는 추가 경품이 지급되지 않습니다.", entry.Mobile);
            }
           
            var result = repo.Update(entry);
            repo.Save();

            return result;
        }
        public KittyJusticeLeagueInstantLottery GetInstantLotteryWinner(long id) {
            var entry = repo.SingleOrDefault(x => x.Id == id && x.PrizeType != KittyJusticeLeagueInstantLotteryPrizeType.Loser && x.Mobile == null && x.Name == null);
            if (entry == null) {
                throw new KittyJusticeLeagueServiceException("400", "이미 개인정보가 등록되었거나 당첨 내역을 찾을 수 없습니다.", null);
            }
            return entry;
        }
        public KittyJusticeLeagueInstantLotteryPrizeSetting CreateInstantLotteryPrizeSetting(KittyJusticeLeagueInstantLotteryPrizeSetting entry) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var isOverlapPrize = settingRepo.Filter(x => x.Date == entry.Date && x.PrizeType == entry.PrizeType).SingleOrDefault();
            if (isOverlapPrize != null) {
                throw new KittyJusticeLeagueServiceException("400", "이미 등록된 데이터가 있습니다.", null);
            }
            var result = settingRepo.Add(entry);
            settingRepo.Save();

            return result;
        }
        public IQueryable<KittyJusticeLeagueInstantLotteryPrizeSetting> GetAdminInstatLotteryPrizeSettingList() {
            return settingRepo.GetAll().OrderBy(x => x.Date);
        }
        public KittyJusticeLeagueInstantLotteryPrizeSetting UpdateInstantLotteryPrizeSetting(DateTime date, KittyJusticeLeagueInstantLotteryPrizeType prizeType, int totalCount, int startTime, float rate) {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var updateEntry = settingRepo.SingleOrDefault(x => x.Date == date && x.PrizeType == prizeType);
            if (updateEntry == null) {
                throw new KittyJusticeLeagueServiceException("400", "수정할 데이터가 없습니다.", null);
            }
            if (updateEntry.WinnerCount > totalCount) {
                throw new KittyJusticeLeagueServiceException("400", "경품수량은 당첨수량보다 적게 세팅할 수 없습니다.", totalCount);
            }
            if (totalCount <= 0) {
                throw new KittyJusticeLeagueServiceException("400", "경품수량은 0이하로 세팅할 수 없습니다.", totalCount);
            }
            if (startTime < 0 || startTime >= 24) {
                throw new KittyJusticeLeagueServiceException("400", "당첨 시작시간은 0~23 까지 가능합니다.", startTime);
            }
            if (rate <= 0) {
                throw new KittyJusticeLeagueServiceException("400", "당첨확률은 0이하로 세팅할 수 없습니다.", rate);
            }
            updateEntry.TotalCount = totalCount;
            updateEntry.StartTime = startTime;
            updateEntry.Rate = rate;

            var result = settingRepo.Update(updateEntry);
            settingRepo.Save();

            return result;
        }

        public IQueryable<KittyJusticeLeagueInstantLottery> GetAdminKittyJusticeLeagueEntryList(KittyJusticeLeagueQueryOptions option) {
            return repo.GetAll().AsExpandable().Where(option.BuildPredicate()).OrderByDescending(x => x.CreateDate);
        }
    }
    public class KittyJusticeLeagueServiceException : EventServiceException {
        public KittyJusticeLeagueServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
