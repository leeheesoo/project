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
    public class RouletteEventServiceTest {
        private Mock<IRouletteEventEntryRepository> entryRepo;
        private Mock<IRouletteEventPrizeSettingRepository> settingRepo;
        private Mock<ITimeProvider> time;
        private RouletteEventService service;

        public RouletteEventServiceTest() {
            entryRepo = new Mock<IRouletteEventEntryRepository>();
            settingRepo = new Mock<IRouletteEventPrizeSettingRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2017, 10, 25, 15, 0, 0));
            service = new RouletteEventService(entryRepo.Object,settingRepo.Object,time.Object);
        }

        [Fact(DisplayName = "룰렛이벤트-개인정보저장")]
        public void TryCreateRouletteEventEntry() {
            //arrange
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action            
            var tryTest = service.CreateRouletteEventEntry(entry);

            //assert
            Assert.NotNull(tryTest);
            Assert.Matches("홍길동", tryTest.Name);

            entryRepo.Verify(x => x.Add(It.IsAny<RouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Any(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "룰렛이벤트-개인정보저장실패(응모이력 존재)")]
        public void TryCreateRouletteEventEntryFailedWon() {
            //arrange
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(true);
            entryRepo.Setup(x => x.Add(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<RouletteServiceException>(() => {
                var tryTest = service.CreateRouletteEventEntry(entry);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("해당번호로 중복참여가 불가능합니다.", failedResult.Message);
        }

        [Fact(DisplayName = "룰렛이벤트-개인정보저장실패(당첨정보가 존재)")]
        public void TryCreateRouletteEventEntryFailedHasPrizeType() {
            //arrange
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = RouletteEventInstantLotteryPrizeType.Cash1000000
            };
            entryRepo.Setup(x => x.Any(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(false);
            entryRepo.Setup(x => x.Add(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<RouletteServiceException>(() => {
                var tryTest = service.CreateRouletteEventEntry(entry);
            });
            
            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("정상적인 경로로 이벤트에 참여해주세요.", failedResult.Message);
        }

        [Fact(DisplayName = "룰렛이벤트-경품 Wincount == TotalCount 꽝처리(천원)")]
        public void TryUpdateRouletteEventEntryFailedEndWincount() {
            //arrange
            var prizes = new List<RouletteEventInstantLotteryPrizeSetting>() {
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14 }
            };
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, RouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<RouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "룰렛이벤트-경품 당첨시작시간 꽝처리(천원)")]
        public void TryUpdateRouletteEventEntryFailedEndStartTime() {
            //arrange
            var prizes = new List<RouletteEventInstantLotteryPrizeSetting>() {
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16 }
            };
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            entryRepo.Setup(x => x.Update(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, RouletteEventInstantLotteryPrizeType.Loser);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<RouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "룰렛이벤트-경품당첨(100만원)")]
        public void TryUpdateRouletteEventEntry() {
            //arrange
            var prizes = new List<RouletteEventInstantLotteryPrizeSetting>() {
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash10000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash50000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash500000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 16  },
                new RouletteEventInstantLotteryPrizeSetting { Date = new DateTime(2017, 10, 25), PrizeType = RouletteEventInstantLotteryPrizeType.Cash1000000, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 14 }
            };
            var entry = new RouletteEventEntry {
                Id = 1,
                Name = "홍길동",
                Mobile = "01012345678",
                CreateDate = new DateTime(2017, 10, 25),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile
            };

            entryRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(entry);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>())).Returns(prizes[3]);
            var list = new List<RouletteEventEntry>();
            entryRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>())).Returns(list.AsQueryable());
            entryRepo.Setup(x => x.Update(It.IsAny<RouletteEventEntry>())).Returns(entry);

            //action
            var result = service.UpdateRouletteEventEntry(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, RouletteEventInstantLotteryPrizeType.Cash1000000);

            entryRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>()),Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<RouletteEventInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            entryRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<RouletteEventEntry, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<RouletteEventInstantLotteryPrizeSetting>()), Times.Once);
            entryRepo.Verify(x => x.Update(It.IsAny<RouletteEventEntry>()), Times.Once);
            entryRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}
