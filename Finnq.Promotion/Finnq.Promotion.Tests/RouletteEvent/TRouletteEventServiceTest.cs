using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Infrastructures;
using Finnq.Promotion.Domain.Repositories.RouletteEvent;
using Finnq.Promotion.Domain.Services.RouletteEvent;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Finnq.Promotion.Tests.RouletteEvent {
    public class TRouletteEventServiceTest {
        private Mock<ITRouletteEventEntryRepository> entryRepo;
        private Mock<ITRouletteEventPrizeSettingRepository> settingRepo;
        private Mock<ITimeProvider> time;
        private TRouletteEventService service;

        public TRouletteEventServiceTest() {
            entryRepo = new Mock<ITRouletteEventEntryRepository>();
            settingRepo = new Mock<ITRouletteEventPrizeSettingRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2017, 10, 25, 15, 0, 0));
            service = new TRouletteEventService(entryRepo.Object,settingRepo.Object,time.Object);
        }

        [Fact(DisplayName = "T-룰렛이벤트-개인정보저장")]
        public void TryCreateTRouletteEventEntry() {
            //arrange
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action            
            var tryTest = service.CreateTRouletteEventEntry(entry);

            //assert
            Assert.NotNull(tryTest);
            Assert.Matches("홍길동", tryTest.Name);

            entryRepo.Verify(x => x.Add(It.IsAny<TRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Any(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "T-룰렛이벤트-개인정보저장실패(응모이력 존재)")]
        public void TryCreateTRouletteEventEntryFailedWon() {
            //arrange
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(true);
            entryRepo.Setup(x => x.Add(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<TRouletteServiceException>(() => {
                var tryTest = service.CreateTRouletteEventEntry(entry);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("해당번호로 중복참여가 불가능합니다.", failedResult.Message);
        }

        [Fact(DisplayName = "T-룰렛이벤트-개인정보저장실패(당첨정보가 존재)")]
        public void TryCreateTRouletteEventEntryFailedHasPrizeType() {
            //arrange
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = TRouletteEventInstantLotteryPrizeType.Cash1000000
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<TRouletteServiceException>(() => {
                var tryTest = service.CreateTRouletteEventEntry(entry);
            });
            
            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("정상적인 경로로 이벤트에 참여해주세요.", failedResult.Message);
        }

        [Fact(DisplayName = "T-룰렛이벤트-경품 Wincount == TotalCount 꽝처리(천원)")]
        public void TryUpdateTRouletteEventEntryFailedEndWincount() {
            //arrange
            var prizes = new List<TRouletteEventInstantLotteryPrizeSetting>() {
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14 }
            };
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TRouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "T-룰렛이벤트-경품 당첨시작시간 꽝처리(천원)")]
        public void TryUpdateRouletteEventEntryFailedEndStartTime() {
            //arrange
            var prizes = new List<TRouletteEventInstantLotteryPrizeSetting>() {
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16 }
            };
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TRouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "T-룰렛이벤트-경품당첨(100만원)")]
        public void TryUpdateTRouletteEventEntry() {
            //arrange
            var prizes = new List<TRouletteEventInstantLotteryPrizeSetting>() {
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 14 }
            };
            var entry = new TRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes[3]);
            var list = new List<TRouletteEventEntry>();
            entryRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>())).Returns(list.AsQueryable());
            entryRepo.Setup(x => x.Update(It.IsAny<TRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TRouletteEventInstantLotteryPrizeType.Cash1000000);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<TRouletteEventInstantLotteryPrizeSetting>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}
