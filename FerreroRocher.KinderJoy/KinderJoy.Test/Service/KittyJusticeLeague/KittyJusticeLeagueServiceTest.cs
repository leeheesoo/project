using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;
using KinderJoy.Domain.Repository.FindingDory2017;
using KinderJoy.Domain.Service.FindingDory2017;
using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Repository.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Service.KittyJusticeLeague;
using KinderJoy.Domain.Repository.KittyJusticeLeague;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Entities.KittyJusticeLeague;

namespace KinderJoy.Test.Service.KittyJusticeLeague {
    public class KittyJusticeLeagueServiceTest {
        private Mock<IKittyJusticeLeagueInstantLotteryRepository> repo;
        private Mock<IKittyJusticeLeagueInstantLotteryPrizeSettingRepository> settingRepo;
        private Mock<ITimeProvider> time;
        private IKittyJusticeLeagueService service;

        public KittyJusticeLeagueServiceTest() {
            repo = new Mock<IKittyJusticeLeagueInstantLotteryRepository>();
            settingRepo = new Mock<IKittyJusticeLeagueInstantLotteryPrizeSettingRepository>();
            time = new Mock<ITimeProvider>();
            time.Setup(x => x.Now).Returns(new DateTime(2017, 11, 13, 15, 0, 0));
            service = new KittyJusticeLeagueService(repo.Object,settingRepo.Object,time.Object);
        }
        [Fact(DisplayName = "키티&저스티스리그-경품당첨")]
        public void TryCreateInstantLotterySuccess() {
            //arrange
            var prizes = new List<KittyJusticeLeagueInstantLotteryPrizeSetting>() {
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JusticeLeagueNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.ChirstmasTree, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 12 }
            };
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            settingRepo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>())).Returns(prizes[3]);
            var list = new List<KittyJusticeLeagueInstantLottery>();
            repo.Setup(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(list.AsQueryable());
            repo.Setup(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Update(It.IsAny<KittyJusticeLeagueInstantLotteryPrizeSetting>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }
        [Fact(DisplayName = "키티&저스티스리그-경품 Wincount == TotalCount 꽝처리")]
        public void TryCreateInstantLotteryFailedEndWincount() {
            //arrange
            var prizes = new List<KittyJusticeLeagueInstantLotteryPrizeSetting>() {
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JusticeLeagueNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.ChirstmasTree, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 1, StartTime = 14 }
            };
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            repo.Setup(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry,true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-경품 당첨시작시간 꽝처리")]
        public void TryCreateInstantLotteryFailedEndStartTime() {
            //arrange
            var prizes = new List<KittyJusticeLeagueInstantLotteryPrizeSetting>() {
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JusticeLeagueNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.ChirstmasTree, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19  },
                new KittyJusticeLeagueInstantLotteryPrizeSetting { Date = new DateTime(2017, 11, 13), PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.KittyNanoBlock, Rate = 1.0f, TotalCount = 1, WinnerCount = 0, StartTime = 19 }
            };
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(false);
            settingRepo.Setup(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>())).Returns(prizes.AsQueryable);
            repo.Setup(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            settingRepo.Verify(x => x.Filter(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLotteryPrizeSetting, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-경품 isChecked = false 꽝 처리")]
        public void TryCreateInstantLotteryFailedByIsCheckedFalse() {
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(false);
            repo.Setup(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, false);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-경품 중복아이피 꽝 처리")]
        public void TryCreateInstantLotteryFailedByIsWinIpAddress() {
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.Loser
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(true);
            repo.Setup(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.CreateInstantLottery(entry, true);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.Loser);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Add(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 응모 성공")]
        public void TryUpdateInstantLotteryWinnerSuccess() {
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon,
                Name = "홍길동",
                Mobile="01012345678",
                UpdateDate = new DateTime(2017,11,13)
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(false);
            repo.Setup(x => x.Update(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var result = service.UpdateInstantLotteryWinner(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon);

            repo.Verify(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
            repo.Verify(x => x.Update(It.IsAny<KittyJusticeLeagueInstantLottery>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 응모 실패")]
        public void TryUpdateInstantLotteryWinnerFailed() {
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.Mobile,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon,
                Name = "홍길동",
                Mobile = "01012345678",
                UpdateDate = new DateTime(2017, 11, 13)
            };

            repo.Setup(x => x.Any(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(true);
            repo.Setup(x => x.Update(It.IsAny<KittyJusticeLeagueInstantLottery>())).Returns(entry);

            //action
            var failedResult = Assert.Throws<KittyJusticeLeagueServiceException>(() => {
                var tryTest = service.UpdateInstantLotteryWinner(entry);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Matches("이미 당첨된 내역이 존재합니다.당첨된 분에게는 추가 경품이 지급되지 않습니다.", failedResult.Message);
        }

        [Fact(DisplayName = "키티&저스티스리그-즉석당첨 특정 당첨 정보 가져오기 성공")]
        public void TryGetInstantLotteryWinnerSuccess() {
            var entry = new KittyJusticeLeagueInstantLottery {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = Domain.Abstract.ChannelType.PC,
                PrizeType = KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon
            };

            repo.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>())).Returns(entry);

            //action
            var result = service.GetInstantLotteryWinner(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.PrizeType, KittyJusticeLeagueInstantLotteryPrizeType.JoyGifticon);

            repo.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<KittyJusticeLeagueInstantLottery, bool>>>()), Times.Once);
        }
    }
}