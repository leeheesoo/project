using INGLife.Event.Domain.Entities.MamboEvent;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Repositories.MarketingAgree;
using INGLife.Event.Domain.Services.Nilririmambo;
using INGLife.Event.Domain.Services.OverFortyFiveDb;
using INGLife.Event.Domain.Services.TumblerEvent;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace INGLife.Event.Test.OverFortyFiveDbEventTest
{
    public class TumblerEventTest
    {
        private Mock<ITumblerEventRepository> repository;
        private ITumblerEventService service;

        private List<TumblerEventEntry> TumblerEventTestEntries
        {
            get
            {
                return new List<TumblerEventEntry>() {
                    new TumblerEventEntry { Id=1, BirthDay="19890912", Address="서울 강남구 역삼동 735-22번지", AddressDetail ="gala빌딩 9층", Gender="1", Mobile="01012345678", Name="홍길동"},
                    new TumblerEventEntry { Id=1, BirthDay="19890913", Address="서울 강남구 역삼동 735-22번지", AddressDetail ="gala빌딩 9층", Gender="2", Mobile="01012345670", Name="심청이"},
                    new TumblerEventEntry { Id=1, Address="서울 강남구 역삼동 735-22번지", AddressDetail ="gala빌딩 9층", Gender="2", Name="멍청이"}
                };
            }
        }
        public TumblerEventTest()
        {
            this.repository = new Mock<ITumblerEventRepository>();
            service = new TumblerEventService(repository.Object);
        }


        [Fact(DisplayName = "텀블러이벤트 - 참여자 정보 저장")]
        public void CreateEventEntry()
        {
            //arrange
            var entry = TumblerEventTestEntries[0];
            repository.Setup(x => x.Add(It.IsAny<TumblerEventEntry>())).Returns(entry);

            //action
            var result = service.Create(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, entry.Name);
            repository.Verify(x => x.Add(It.IsAny<TumblerEventEntry>()), Times.Once);
            repository.Verify(x => x.Save(), Times.Once);
        }


        [Fact(DisplayName = "텀블러이벤트 - 중복 참여자 테스트")]
        public void DepulicateEntryTest()
        {
            var entry = TumblerEventTestEntries[0];
            repository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TumblerEventEntry, bool>>>())).Returns(entry);

            var result = service.CheckEntry(entry.Name, entry.Mobile, entry.Gender, entry.BirthDay);

            Assert.NotNull(result);
            Assert.Equal(entry.Name, result.Name);
            Assert.Equal(entry.Mobile, result.Mobile);

            repository.Verify(x => x.Add(It.IsAny<TumblerEventEntry>()), Times.Never);
            repository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TumblerEventEntry, bool>>>()), Times.Once);
        }
    }
}
