using Samsonite.Mall.Domain.Repositories.TwoYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Exceptions;
using Samsonite.Mall.Domain.Infrastructures;
using System.Transactions;

namespace Samsonite.Mall.Domain.Services.TwoYearAnniversary {
    public class TwoYearAnniversaryService : ITwoYearAnniversaryService {
        private ITwoYearAnniversaryEntryRepository twoYearRepository;
        private ITwoYearAnniversaryPrizeSettingRepository prizeRepository;
        private ITwoYearAnniversaryWinCouponRepository couponRepository;
        private ITimeProvider time;

        private InstantLotteryPrizeSelector<TwoYearAnniversaryInstantPrizeSetting, TwoYearAnniversarInstantPrizeType> prizeSelector;

        public TwoYearAnniversaryService(ITwoYearAnniversaryEntryRepository twoYearRepository, ITwoYearAnniversaryPrizeSettingRepository prizeRepository, ITwoYearAnniversaryWinCouponRepository couponRepository, ITimeProvider time) {
            this.twoYearRepository = twoYearRepository;
            this.prizeRepository = prizeRepository;
            this.couponRepository = couponRepository;
            this.time = time;

            this.prizeSelector = new InstantLotteryPrizeSelector<TwoYearAnniversaryInstantPrizeSetting, TwoYearAnniversarInstantPrizeType>();
        }

        /// <summary>
        /// 즉석당첨 정보저장
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TwoYearAnniversaryEntry CreateTwoYearAnniversaryEntry(TwoYearAnniversaryEntry entity)
        {
            var today = time.Now.Date;
            var tommorrow = today.AddDays(1);
                        
            var isEntryCount = twoYearRepository.SingleOrDefault(x => x.CreateDate >= today && x.CreateDate <= tommorrow && x.SamsoniteId == entity.SamsoniteId);

            if (isEntryCount != null)
            { // 1일 응모횟수 1회 제한
                throw new TwoYearAnniversaryServiceException("400", "하루에 한 번 응모가 가능합니다. 내일 다시 참여해주세요!", null);
            }

            using (var tx = new TransactionScope())
            {
                var now = time.Now;

                // 기당첨 check ( 연락처 )
                var isWin = twoYearRepository.Any(x => x.PrizeType != TwoYearAnniversarInstantPrizeType.Loser && x.SamsoniteId == entity.SamsoniteId);

                // 당첨되었을대 세팅 
                entity.PrizeType = TwoYearAnniversarInstantPrizeType.Loser;
                // 당첨되지 않았다면.
                if (!isWin)
                {                   
                    //조건체크를 한번하고. 
                    var prizes = prizeRepository.Filter(x => x.Date == now.Date && x.StartTime <= now.Hour && x.TotalCount > x.WinnerCount && x.Rate > 0.0f);                     
                    entity.PrizeType = prizeSelector.SelectPrize(prizes.ToList(), TwoYearAnniversarInstantPrizeType.Loser, now);

                    if (entity.PrizeType != TwoYearAnniversarInstantPrizeType.Loser)
                    {
                        var prize = prizeRepository.SingleOrDefault(x => x.Date == now.Date && x.PrizeType == entity.PrizeType);                        
                        if (prize == null)
                        {
                            //상품세팅이 아무것도 안되어 있다면
                            entity.PrizeType = TwoYearAnniversarInstantPrizeType.Loser;
                        }
                        else
                        {
                            //전체 entry 에서 데이터를 카운팅 후 1건 당첨된것을 상품세팅 테이블에 카운팅
                            prize.WinnerCount = twoYearRepository.Filter(x => x.CreateDate >= now.Date && x.PrizeType == entity.PrizeType).Count() + 1;
                            prizeRepository.Update(prize);
                        }
                    }
                }

                var user = twoYearRepository.Add(entity);
                twoYearRepository.Save();

                tx.Complete();
                return user;
            }
        }
        /// <summary>
        /// 당첨 쿠폰 발급
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public TwoYearAnniversaryWinCoupon UpdateTwoYearAnniversaryWinCoupon(long entryId)
        {
            var user = twoYearRepository.SingleOrDefault(x => x.Id == entryId);
            if(user == null)
            {
                throw new TwoYearAnniversaryServiceException("400", "당첨 데이터가 존재하지 않습니다.", null);
            }
            if(user.PrizeType == TwoYearAnniversarInstantPrizeType.Coupon_20 || user.PrizeType == TwoYearAnniversarInstantPrizeType.Loser)
            {
                var coupon = couponRepository.FirstOrDefault(x => (int)x.CouponType == (int)user.PrizeType && !x.WinnerDate.HasValue);
                if (coupon != null)
                {
                    coupon.TwoYearAnniversaryEntryId = user.Id;
                    coupon.WinnerDate = time.Now;

                    var winCoupon = couponRepository.Update(coupon);
                    couponRepository.Save();

                    return winCoupon;
                }
            }
            return null;
        }

        /// <summary>
        /// 룰렛경품저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public TwoYearAnniversaryInstantPrizeSetting CreateTwoYearAnniversaryPrize(TwoYearAnniversaryInstantPrizeSetting entry)
        {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var updateEntry = prizeRepository.SingleOrDefault(x => x.Date == entry.Date && x.PrizeType == entry.PrizeType);
            if (updateEntry != null)
            {
                throw new TwoYearAnniversaryServiceException("400", "같은날, 같은 경품번호가 이미 등록되어 있습니다.", null);
            }
            if (TwoYearAnniversarInstantPrizeType.Loser == entry.PrizeType)
            {
                throw new TwoYearAnniversaryServiceException("400", "1만원 쿠폰은 입력할 수 없습니다.", null);
            }            
            prizeRepository.Add(entry);
            prizeRepository.Save();
            return entry;
        }
        
        /// <summary>
        /// 룰렛 경품목록 가져오기 
        /// </summary>
        /// <param name="id">가져올 data id</param>
        /// <returns></returns>
        public IQueryable<TwoYearAnniversaryInstantPrizeSetting> GetTwoYearAnniversaryPrizeList()
        {
            return prizeRepository.GetAll().OrderBy(x => x.Date);
        }
        
        /// <summary>
        /// 룰렛 경품 수정하기 
        /// </summary>
        /// <param name="entry">TwoYearAnniversarInstantPrizeSetting 전체 데이터 </param>
        /// <returns></returns>
        public TwoYearAnniversaryInstantPrizeSetting UpdateTwoYearAnniversaryPrize(TwoYearAnniversaryInstantPrizeSetting entry)
        {
            // 날짜, 경품이 같은 데이터가 있는지 확인
            var updateEntry = prizeRepository.SingleOrDefault(x => x.Date == entry.Date && x.PrizeType == entry.PrizeType);
            if (updateEntry == null)
            {
                throw new TwoYearAnniversaryServiceException("400", "수정할 데이터가 없습니다.", null);
            }
            if (updateEntry.WinnerCount > entry.TotalCount)
            {
                throw new TwoYearAnniversaryServiceException("400", "경품수량은 당첨수량보다 적게 세팅할 수 없습니다.", updateEntry.WinnerCount);
            }
            if (entry.TotalCount <= 0)
            {
                throw new TwoYearAnniversaryServiceException("400", "경품수량은 0이하로 세팅할 수 없습니다.", entry.TotalCount);
            }
            if (entry.StartTime < 0 || entry.StartTime >= 24)
            {
                throw new TwoYearAnniversaryServiceException("400", "당첨 시작시간은 0~23 까지 가능합니다.", entry.StartTime);
            }
            if (entry.Rate <= 0)
            {
                throw new TwoYearAnniversaryServiceException("400", "당첨확률은 0이하로 세팅할 수 없습니다.", entry.Rate);
            }
            updateEntry.TotalCount = entry.TotalCount;
            updateEntry.StartTime = entry.StartTime;
            updateEntry.Rate = entry.Rate;

            var result = prizeRepository.Update(updateEntry);
            prizeRepository.Save();

            return result;
        }

        /// <summary>
        /// 경품참여자 리스트 조회
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public IQueryable<TwoYearAnniversaryEntry> SelectTwoYearAnniversaryEnty()
        {            
            return twoYearRepository.GetAll().OrderByDescending(x => x.CreateDate);
        }

        public IList<TwoYearAnniversaryEntry> GetOneYearAnniversaryEntryAll()
        {
            return twoYearRepository.GetAll().ToList();
        }
    }
    public class TwoYearAnniversaryServiceException : EventServiceException {
        public TwoYearAnniversaryServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
        public TwoYearAnniversaryServiceException(string message, object data)
            : base(message, data) {

        }
    }
}
