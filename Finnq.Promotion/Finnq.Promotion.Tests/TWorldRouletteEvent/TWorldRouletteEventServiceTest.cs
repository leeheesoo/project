using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Infrastructures;
using Finnq.Promotion.Domain.Repositories.RouletteEvent;
using Finnq.Promotion.Domain.Repositories.TWorldRouletteEvent;
using Finnq.Promotion.Domain.Services.RouletteEvent;
using Finnq.Promotion.Domain.Services.TWorldRouletteEvent;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Finnq.Promotion.Tests.TWorldRouletteEvent {
    public class TWorldRouletteEventServiceTest {
        private Mock<ITWorldRouletteEventEntryRepository> entryRepo;
        private Mock<ITWorldRouletteEventPrizeSettingRepository> settingRepo;
        private Mock<ITimeProvider> time;
        private TWorldRouletteEventService service;

        public TWorldRouletteEventServiceTest() {
            entryRepo = new Mock<ITWorldRouletteEventEntryRepository>();
            settingRepo = new Mock<ITWorldRouletteEventPrizeSettingRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2017, 10, 25, 15, 0, 0));
            service = new TWorldRouletteEventService(entryRepo.Object,settingRepo.Object,time.Object);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-개인정보저장")]
        public void TryCreateTWorldRouletteEventEntry() {
            //arrange
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action            
            var tryTest = service.CreateTWorldRouletteEventEntry(entry);

            //assert
            Assert.NotNull(tryTest);
            Assert.Matches("홍길동", tryTest.Name);

            entryRepo.Verify(x => x.Add(It.IsAny<TWorldRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Any(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-개인정보저장실패(응모이력 존재)")]
        public void TryCreateTWorldRouletteEventEntryFailedWon() {
            //arrange
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(true);
            entryRepo.Setup(x => x.Add(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<TWorldRouletteServiceException>(() => {
                var tryTest = service.CreateTWorldRouletteEventEntry(entry);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("해당번호로 중복참여가 불가능합니다.", failedResult.Message);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-개인정보저장실패(당첨정보가 존재)")]
        public void TryCreateTWorldRouletteEventEntryFailedHasPrizeType() {
            //arrange
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash1000000
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<TWorldRouletteServiceException>(() => {
                var tryTest = service.CreateTWorldRouletteEventEntry(entry);
            });
            
            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("정상적인 경로로 이벤트에 참여해주세요.", failedResult.Message);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-경품 Wincount == TotalCount 꽝처리(천원)")]
        public void TryUpdateTWorldRouletteEventEntryFailedEndWincount() {
            //arrange
            var prizes = new List<TWorldRouletteEventInstantLotteryPrizeSetting>() {
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14 }
            };
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTWorldRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TWorldRouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TWorldRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-경품 당첨시작시간 꽝처리(천원)")]
        public void TryUpdateTWorldRouletteEventEntryFailedEndStartTime() {
            //arrange
            var prizes = new List<TWorldRouletteEventInstantLotteryPrizeSetting>() {
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16 }
            };
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTWorldRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TWorldRouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TWorldRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "TWorld-룰렛이벤트-경품당첨(100만원)")]
        public void TryUpdateTWorldRouletteEventEntry() {
            //arrange
            var prizes = new List<TWorldRouletteEventInstantLotteryPrizeSetting>() {
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new TWorldRouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = TWorldRouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 14 }
            };
            var entry = new TWorldRouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes[3]);
            var list = new List<TWorldRouletteEventEntry>();
            entryRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>())).Returns(list.AsQueryable());
            entryRepo.Setup(x => x.Update(It.IsAny<TWorldRouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateTWorldRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, TWorldRouletteEventInstantLotteryPrizeType.Cash1000000);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TWorldRouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<TWorldRouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<TWorldRouletteEventInstantLotteryPrizeSetting>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<TWorldRouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}
