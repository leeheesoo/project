using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Repository.DisneyStarWars2018;
using KinderJoy.Domain.Service.DisneyStarWars2018;
using KinderJoy.Domain.Entities.DisneyStarWars2018;
using KinderJoy.Domain.RepositoryDisneyStarWars2018;

namespace KinderJoy.Test.Service.DisneyStarWars2018 {
    public class DisneyStarWars2018ServiceTest {
        private Mock<IDisneyStarWars2018InstantLotteryRepository> repo;
        private Mock<IDisneyStarWars2018InstantLotteryPrizeSettingRepository> settingRepo;
        private Mock<ITimeProvider> time;
        private IDisneyStarWars2018Service service;

        public DisneyStarWars2018ServiceTest() {
            repo = new Mock<IDisneyStarWars2018InstantLotteryRepository>();
            settingRepo = new Mock<IDisneyStarWars2018InstantLotteryPrizeSettingRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2018, 5, 2, 15, 0, 0));
            service = new DisneyStarWars2018Service(repo.Object,settingRepo.Object,time.Object);
        }


        [Fact(DisplayName = "경품당첨")]
        public void TryCreateInstantLotterySuccess() {
            //arrange
            var prizes = new List<DisneyStarWars2018InstantLotteryPrizeSetting>() {
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.DisneyQuenMirror, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.StarWarsCupSet, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  }                
            };
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>())).Returns(prizes[3]);
            var list = new List<DisneyStarWars2018InstantLottery>();
            repo.Setup(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(list.AsQueryable());
            repo.Setup(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<DisneyStarWars2018InstantLotteryPrizeSetting>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }


        [Fact(DisplayName = "뽀로로-경품 Wincount == TotalCount 꽝처리")]
        public void TryCreateInstantLotteryFailedEndWincount() {
            //arrange
            var prizes = new List<DisneyStarWars2018InstantLotteryPrizeSetting>() {
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.DisneyQuenMirror, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.StarWarsCupSet, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  }
            };
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            repo.Setup(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry,true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "뽀로로-경품 당첨시작시간 꽝처리")]
        public void TryCreateInstantLotteryFailedEndStartTime() {
            //arrange
            var prizes = new List<DisneyStarWars2018InstantLotteryPrizeSetting>() {
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.DisneyQuenMirror, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new DisneyStarWars2018InstantLotteryPrizeSetting { Date = new DateTime(2018, 5, 2), PrizeType = DisneyStarWars2018InstantLotteryPrizeType.StarWarsCupSet, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  }
            };
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            repo.Setup(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<DisneyStarWars2018InstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "뽀로로-경품 isChecked = false 꽝 처리")]
        public void TryCreateInstantLotteryFailedByIsCheckedFalse() {
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(false);
            repo.Setup(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, false);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "뽀로로-경품 중복아이피 꽝 처리")]
        public void TryCreateInstantLotteryFailedByIsWinIpAddress() {
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 11, 4),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(true);
            repo.Setup(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 응모 성공")]
        public void TryUpdateInstantLotteryWinnerSuccess() {
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty,
                Name = "홍길동",
                Mobile="01012345678",
                UpdateDate = new DateTime(2018,1,4)
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(false);
            repo.Setup(x => x.Update(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var result = service.UpdateInstantLotteryWinner(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Update(It.IsAny<DisneyStarWars2018InstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 응모 실패")]
        public void TryUpdateInstantLotteryWinnerFailed() {
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty,
                Name = "홍길동",
                Mobile = "01012345678",
                UpdateDate = new DateTime(2018, 5, 2)
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(true);
            repo.Setup(x => x.Update(It.IsAny<DisneyStarWars2018InstantLottery>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<DisneyStarWars2018ServiceException>(() => {
                var tryTest = service.UpdateInstantLotteryWinner(entry);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("이미 당첨된 내역이 존재합니다.당첨된 분에게는 추가 경품이 지급되지 않습니다.", failedResult.Message);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 특정 당첨 정보 가져오기 성공")]
        public void TryGetInstantLotteryWinnerSuccess() {
            var entry = new DisneyStarWars2018InstantLottery {
                Id = 1,
                CreateDate = new DateTime(2018, 5, 2),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.PC,
                PrizeType = DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty
            };

            repo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>())).Returns(entry);

            //action
            var result = service.GetInstantLotteryWinner(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, DisneyStarWars2018InstantLotteryPrizeType.KinderJoyGifty);

            repo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<DisneyStarWars2018InstantLottery, bool>>>()), Times.Once);
        }
    }
}