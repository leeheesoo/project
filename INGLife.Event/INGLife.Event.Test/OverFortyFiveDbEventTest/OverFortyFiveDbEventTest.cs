using INGLife.Event.Domain.Entities.MamboEvent;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Repositories.MarketingAgree;
using INGLife.Event.Domain.Services.Nilririmambo;
using INGLife.Event.Domain.Services.OverFortyFiveDb;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace INGLife.Event.Test.OverFortyFiveDbEventTest
{
    public class OverFortyFiveDbEventTest
    {
        private Mock<IOverFortyFiveDbEntryRepository> repository;
        private OverFortyFiveService service;

        private List<OverFortyFiveDbEntry> OverFortyFiveDbEventEntries
        {
            get
            {
                return new List<OverFortyFiveDbEntry>() {
                    new OverFortyFiveDbEntry { Id=1, BirthDay="19890912", EmoticonType=EmoticonType.Emoticon1, Gender="1", Mobile="01012345678", Name="홍길동"},
                    new OverFortyFiveDbEntry { Id=1, BirthDay="19890913", EmoticonType=EmoticonType.Emoticon1, Gender="2", Mobile="01012345670", Name="심청이"},
                    new OverFortyFiveDbEntry { Id=1, BirthDay="19890914", EmoticonType=EmoticonType.Emoticon1, Gender="2", Mobile="01012345677", Name="멍청이"}
                };
            }
        }
        public OverFortyFiveDbEventTest()
        {
            this.repository = new Mock<IOverFortyFiveDbEntryRepository>();
            service = new OverFortyFiveService(repository.Object);
        }


        [Fact(DisplayName = "참여자 정보 저장")]
        public void CreateEventEntry()
        {
            //arrange
            var entry = OverFortyFiveDbEventEntries[0];
            repository.Setup(x => x.Add(It.IsAny<OverFortyFiveDbEntry>())).Returns(entry);

            //action
            var result = service.CreateOverFortyFiveEntry(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, entry.Name);
            repository.Verify(x => x.Add(It.IsAny<OverFortyFiveDbEntry>()), Times.Once);
            repository.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "참여자 정보 업데이트")]
        public void UpdateEventEntry()
        {
            //arrange
            var entry = OverFortyFiveDbEventEntries[0];
            repository.Setup(x => x.Update(It.IsAny<OverFortyFiveDbEntry>())).Returns(entry);

            //action
            var result = service.UpdateOverFortyFiveEntry(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, entry.Name);
            repository.Verify(x => x.Update(It.IsAny<OverFortyFiveDbEntry>()), Times.Once);
            repository.Verify(x => x.Save(), Times.Once);
        }
    }
}
