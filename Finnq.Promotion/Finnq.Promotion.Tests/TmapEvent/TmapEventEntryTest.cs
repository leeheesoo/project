using Finnq.Promotion.Domain.Entities.TmapEvent;
using Finnq.Promotion.Domain.Repositories.TmapEvent;
using Finnq.Promotion.Domain.Services.TmapEvent;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Finnq.Promotion.Tests.TmapEvent {
    public class TmapEventEntryTest {
        private Mock<ITmapEventEntryRepository> repository;
        private TmapEventEntryService service;

        public TmapEventEntryTest() {
            repository = new Mock<ITmapEventEntryRepository>();
            service = new TmapEventEntryService(repository.Object);
        }

        [Fact(DisplayName = "T map 이벤트 응모 테스트")]
        public void TryCreateTmapEventEntry() {
            //arrange
            var entry = new TmapEventEntry {
                Id = 1,
                Name = "홍길동",
                Phone = "01012345678",
                CreateDate = new DateTime(2017, 10, 1),
                IpAddress = "127.0.0.1",
                IsMobileDevice = true
            };
            repository.Setup(x => x.Add(It.IsAny<TmapEventEntry>())).Returns(entry);

            //action            
            var tryTest = service.CreateTmapEventEntry(entry);

            //assert
            Assert.NotNull(tryTest);
            Assert.Matches("홍길동", tryTest.Name);

            repository.Verify(x => x.Add(It.IsAny<TmapEventEntry>()), Times.Once);
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.Filter(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.GetAll(), Times.Never);
            repository.Verify(x => x.Remove(It.IsAny<TmapEventEntry>()), Times.Never);
            repository.Verify(x => x.Save(), Times.Once);
            repository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.Update(It.IsAny<TmapEventEntry>()), Times.Never);
        }

        [Fact(DisplayName = "T map 이벤트 응모 실패 테스트")]
        public void TryCreatTemapEventEntryFailed() {
            //arrange
            var entry = new TmapEventEntry {
                Id = 1,
                Name = "홍길동",
                CreateDate = new DateTime(2017, 10, 1),
                IpAddress = "127.0.0.1",
                IsMobileDevice = true
            };
            repository.Setup(x => x.Add(It.IsAny<TmapEventEntry>())).Returns(new TmapEventEntry { });

            //action
            var failedResult = Assert.Throws<TmapEventEntryServiceException>(() => {
                var tryTest = service.CreateTmapEventEntry(null);
            });

            //assert
            Assert.Matches("400", failedResult.Code);
            Assert.Contains("관리자에게", failedResult.Message);

            repository.Verify(x => x.Add(It.IsAny<TmapEventEntry>()), Times.Never);
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.Filter(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.GetAll(), Times.Never);
            repository.Verify(x => x.Remove(It.IsAny<TmapEventEntry>()), Times.Never);
            repository.Verify(x => x.Save(), Times.Never);
            repository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
            repository.Verify(x => x.Update(It.IsAny<TmapEventEntry>()), Times.Never);
        }

        //[Fact(DisplayName = "T map 이벤트 응모 리스트 가져오기 테스트")]
        //public void TryFilterTmapEventEntry() {
        //    //arrange
        //    var list = new List<TmapEventEntry>() {
        //        new TmapEventEntry { Id = 1, Name = "홍길동", Phone = "01012345678", IsMobileDevice = true, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = false, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = false, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = false, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = true, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = true, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //        new TmapEventEntry { Id = 2, Name = "홍길동", Phone = "01012345678", IsMobileDevice = false, CreateDate = new DateTime(2017, 10, 1), IpAddress = "127.0.0.1" },
        //    };
        //    repository.Setup(x => x.Filter(It.IsAny<Expression<Func<TmapEventEntry, bool>>>())).Returns(list.AsQueryable().Where(x=>x.IsMobileDevice));

        //    //action
        //    var options = new TmapEventEntryQueryOptions { Page = 1, PageSize = 20, IsMobileDevice = true };
        //    var tryTest = service.FilterTmapEventEntry(options);

        //    //assert
        //    Assert.NotNull(tryTest);
        //    Assert.Equal(3, tryTest.Count());
        //    Assert.NotNull(tryTest.FirstOrDefault());
        //    Assert.Matches("홍길동", tryTest.FirstOrDefault().Name);

        //    repository.Verify(x => x.Add(It.IsAny<TmapEventEntry>()), Times.Never);
        //    repository.Verify(x => x.Any(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
        //    repository.Verify(x => x.Filter(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Once);
        //    repository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
        //    repository.Verify(x => x.GetAll(), Times.Never);
        //    repository.Verify(x => x.Remove(It.IsAny<TmapEventEntry>()), Times.Never);
        //    repository.Verify(x => x.Save(), Times.Never);
        //    repository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<TmapEventEntry, bool>>>()), Times.Never);
        //    repository.Verify(x => x.Update(It.IsAny<TmapEventEntry>()), Times.Never);
        //}
    }
}
