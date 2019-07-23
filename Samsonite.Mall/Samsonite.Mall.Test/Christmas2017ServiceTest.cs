using System;
using Samsonite.Mall.Domain.Repositories.Christmas2017;
using Moq;
using Samsonite.Mall.Domain.Services.Christmas2017;
using Samsonite.Mall.Domain.Entities.Christmas2017;
using System.Linq.Expressions;
using Xunit;

namespace Samsonite.Mall.Test {

    public class Christmas2017ServiceTest {
        private Mock<IChristmas2017EntryRepository> repo;
        private IChristmas2017Service service;

        public Christmas2017ServiceTest() {
            repo = new Mock<IChristmas2017EntryRepository>();
            service = new Christmas2017Service(repo.Object);
        }

        [Fact(DisplayName = "크리스마스 이벤트 개인정보 저장")]
        public void CreateChristmas2017EntryTestSuccess() {
            var entry = new Christmas2017Entry {
                Id = 1,
                CreateDate = new DateTime(2017, 11, 13),
                IpAddress = "127.0.0.1",
                Channel = "pc",
                Name = "홍길동",
                Mobile = "01012345678",
                ChristmasBagType = ChristmasBagType.Tielonn,
                SamsoniteMallId = "test"
               
            };

            repo.Setup(x => x.Add(It.IsAny<Christmas2017Entry>())).Returns(entry);

            //action
            var result = service.CreateChristmas2017Entry(entry);

            //assert
            Assert.NotNull(result);
            repo.Verify(x => x.Add(It.IsAny<Christmas2017Entry>()), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }
    }
}
