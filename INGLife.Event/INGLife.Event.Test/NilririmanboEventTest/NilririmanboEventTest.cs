using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Entities.MamboEvent;
using INGLife.Event.Domain.Repositories.KidsNote;
using INGLife.Event.Domain.Services.Nilririmambo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace INGLife.Event.Test.EventManagementTest {
    public class NilririmanboEventTest
    {
        private Mock<INilririmamboEntryRepository> repository;
        private NilririmamboService service;

        private List<NilririmanboEntry> NilririmanboEntries
        {
            get
            {
                return new List<NilririmanboEntry>() {
                new NilririmanboEntry { FcCode ="fc-01", InstagramId="insta01", Mobile="01000001111", Name="아에이"},
                new NilririmanboEntry { FcCode ="fc-02", InstagramId="insta02", Mobile="01000002222", Name="오우"},
                new NilririmanboEntry { FcCode ="fc-03", InstagramId="insta03", Mobile="01000004444", Name="홍길동"}
                };
            }
        }
        public NilririmanboEventTest()
        {
            this.repository = new Mock<INilririmamboEntryRepository>();
            service = new NilririmamboService(repository.Object);
        }


        [Fact(DisplayName = "참여자 정보 저장")]
        public void CreateEventEntry()
        {
            //arrange
            var entry = NilririmanboEntries[0];
            repository.Setup(x => x.Add(It.IsAny<NilririmanboEntry>())).Returns(entry);

            //action
            var result = service.CreateNilririmamboEntry(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, entry.Name);
            repository.Verify(x => x.Add(It.IsAny<NilririmanboEntry>()), Times.Once);
            repository.Verify(x => x.Save(), Times.Once);
        }
    }
}
