using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Repositories.Managements;
using INGLife.Event.Domain.Services.Managements;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace INGLife.Event.Test.EventManagementTest {
    public class EventManagementTest {
        private Mock<IEventManagementRepository> repository;
        private EventManagementService service;

        private List<EventManagement> EventManagements {
            get {
                return new List<EventManagement>() {
                new EventManagement { AffiliatesId = 0, EventName = "테스트", PagePath = "/", CreateDate = DateTime.Now },
                new EventManagement { AffiliatesId = 0, EventName = "테스트2", PagePath = "/test", CreateDate = DateTime.Now },
                new EventManagement { AffiliatesId = 0, EventName = "테스트3", PagePath = "/test3", CreateDate = DateTime.Now }
                };
            }
        }
        public EventManagementTest() {
            this.repository = new Mock<IEventManagementRepository>();
            service = new EventManagementService(repository.Object);
        }
        [Fact(DisplayName = "이벤트 관리 - 추가 테스트")]
        public void TryCreateEventManagement() {
            //arrange
            var entry = new EventManagement { AffiliatesId = 0, EventName = "테스트생성", PagePath = "/test-c", CreateDate = DateTime.Now };
            repository.Setup(x => x.Add(It.IsAny<EventManagement>())).Returns(entry);

            //action
            service.CreateEventManagement(entry);

            //assert
            repository.Verify(x => x.GetAll(), Times.Never);
            repository.Verify(x => x.Filter(It.IsAny<Expression<Func<EventManagement, bool>>>()), Times.Once);
            repository.Verify(x => x.Add(It.IsAny<EventManagement>()), Times.Once);
        }

        [Fact(DisplayName = "이벤트 관리 - 리스트 테스트")]
        public void TryGetEventManagement() {
            //arrange
            repository.Setup(x => x.GetAll()).Returns(EventManagements.AsQueryable());

            //action
            var result = service.EventManagements;

            //assert
            repository.Verify(x => x.GetAll(), Times.Once);
            repository.Verify(x => x.Filter(It.IsAny<Expression<Func<EventManagement, bool>>>()), Times.Never);
            repository.Verify(x => x.Add(It.IsAny<EventManagement>()), Times.Never);
        }
    }
}
