using System;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Samsonite.Mall.Domain.Repositories.TwoYearAnniversary;
using Samsonite.Mall.Domain.Services.TwoYearAnniversary;
using Samsonite.Mall.Domain.Infrastructures;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;

namespace Samsonite.Mall.Test
{

    public class ITwoYearAnniversaryServiceTest
    {
        private Mock<ITwoYearAnniversaryEntryRepository> entryRepo;
        private Mock<ITwoYearAnniversaryPrizeSettingRepository> settingRepo;
        private Mock<ITwoYearAnniversaryWinCouponRepository> couponRepo;
        private Mock<ITimeProvider> time;

        private ITwoYearAnniversaryService service;

        public ITwoYearAnniversaryServiceTest()
        {
            entryRepo = new Mock<ITwoYearAnniversaryEntryRepository>();
            settingRepo = new Mock<ITwoYearAnniversaryPrizeSettingRepository>();
            couponRepo = new Mock<ITwoYearAnniversaryWinCouponRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2018, 3, 29, 17, 15, 0));

            service = new TwoYearAnniversaryService(entryRepo.Object, settingRepo.Object, couponRepo.Object, time.Object);
        }

        [Fact(DisplayName = "2주년 룰렛이벤트 즉석당첨 당첨")]
        public void CreateTwoYearAnniversaryEntrySuccess()
        {
            var prizes = new List<TwoYearAnniversaryInstantPrizeSetting>() {
                new TwoYearAnniversaryInstantPrizeSetting { Date = new DateTime(2018, 3, 29), PrizeType = TwoYearAnniversarInstantPrizeType.StarBucks, Rate = 0.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 },
                new TwoYearAnniversaryInstantPrizeSetting { Date = new DateTime(2018, 3, 29), PrizeType = TwoYearAnniversarInstantPrizeType.AtKidsBagPack, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 },
                new TwoYearAnniversaryInstantPrizeSetting { Date = new DateTime(2018, 3, 29), PrizeType = TwoYearAnniversarInstantPrizeType.OriginalBag, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 },
                new TwoYearAnniversaryInstantPrizeSetting { Date = new DateTime(2018, 3, 29), PrizeType = TwoYearAnniversarInstantPrizeType.Coupon_20, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 },
                new TwoYearAnniversaryInstantPrizeSetting { Date = new DateTime(2018, 3, 29), PrizeType = TwoYearAnniversarInstantPrizeType.LineFriendsCarrier, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 }
            };
            var user = new TwoYearAnniversaryEntry { };
            user = null;
            var entry = new TwoYearAnniversaryEntry
            {
                Id = 1,
                CreateDate = new DateTime(2018, 3, 29),
                IpAddress = "127.0.0.1",
                SamsoniteId = "pentacleId123",
                PrizeType = TwoYearAnniversarInstantPrizeType.StarBucks,
                Channel = Domain.Entities.Abstract.ChannelType.PC
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>())).Returns(user);
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TwoYearAnniversaryInstantPrizeSetting, bool>>>())).Returns(prizes.AsQueryable());
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryInstantPrizeSetting, bool>>>())).Returns(prizes[1]);
            var list = new List<TwoYearAnniversaryEntry>();
            entryRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>())).Returns(list.AsQueryable());
            entryRepo.Setup(x => x.Add(It.IsAny<TwoYearAnniversaryEntry>())).Returns(entry);

            //action
            var result = service.CreateTwoYearAnniversaryEntry(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TwoYearAnniversarInstantPrizeType.AtKidsBagPack);
            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Any(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TwoYearAnniversaryInstantPrizeSetting, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryInstantPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<TwoYearAnniversaryInstantPrizeSetting>()), Times.Once);
            entryRepo.Verify(x => x.Add(It.IsAny<TwoYearAnniversaryEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "2주년 룰렛이벤트 쿠폰 당첨")]
        public void UpdateTwoYearAnniversaryWinCouponSuccess()
        {
            
            // 아이디 준비
            var entry = new TwoYearAnniversaryEntry
            {
                Id = 1,
                CreateDate = new DateTime(2018, 3, 29),
                IpAddress = "127.0.0.1",
                SamsoniteId = "pentacleId123",
                PrizeType = TwoYearAnniversarInstantPrizeType.StarBucks,
                Channel = Domain.Entities.Abstract.ChannelType.PC
            };
            entryRepo.Setup(x => x.Add(It.IsAny<TwoYearAnniversaryEntry>())).Returns(entry);
            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>())).Returns(entry);


            var coupon = new TwoYearAnniversaryWinCoupon
            {
                CouponType = 0,
                CouponCode = "1",    
                //TwoYearAnniversaryEntryId = 1,
                //WinnerDate = new DateTime(2018, 3, 29),                
            };            
            couponRepo.Setup(x => x.Add(It.IsAny<TwoYearAnniversaryWinCoupon>())).Returns(coupon);
            couponRepo.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryWinCoupon, bool>>>())).Returns(coupon);


            var resultCoupon = new TwoYearAnniversaryWinCoupon
            {
                CouponType = 0,
                CouponCode = "1",
                TwoYearAnniversaryEntryId = 1,
                WinnerDate = new DateTime(2018, 3, 29),
            };
            couponRepo.Setup(x => x.Update(It.IsAny<TwoYearAnniversaryWinCoupon>())).Returns(resultCoupon);


            long entryId = 1;
            var result = service.UpdateTwoYearAnniversaryWinCoupon(entryId);

            Assert.NotNull(resultCoupon);

            //null check
            //var user = new TwoYearAnniversaryEntry { };
            //user = null;
            //entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TwoYearAnniversaryEntry, bool>>>())).Returns(user);

            //long entryId = 1;

            //var failedResult = Assert.Throws<TwoYearAnniversaryServiceException>(() => {
            //    var result = service.UpdateTwoYearAnniversaryWinCoupon(entryId);
            //});

            //Assert.Matches("400", failedResult.Code);
            //Assert.Matches("당첨 데이터가 존재하지 않습니다.", failedResult.Message);
        }
    }
}
