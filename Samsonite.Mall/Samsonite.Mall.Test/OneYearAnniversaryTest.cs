using Moq;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Repositories.OneYearAnniversary;
using Samsonite.Mall.Domain.Services.OneYearAnniversary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Samsonite.Mall.Test {
    public class OneYearAnniversaryTest {

        private Mock<IOneYearAnniversaryRepository> mockRepository;
        private OneYearAnniversaryService service;
        public OneYearAnniversaryTest() {
            mockRepository = new Mock<IOneYearAnniversaryRepository>();
            service = new OneYearAnniversaryService(mockRepository.Object);
        }

        [Fact(DisplayName = "오행시짓기 댓글이벤트 생성 테스트")]
        public void CreateOneYearAnniversary() {
            //arrange
            var entry = new OneYearAnniversaryEntry {
                AcrosticPoemFirst = "쌤~~",
                AcrosticPoemSecond = "소식들었나용??",
                AcrosticPoemThird = "나인뮤지스가",
                AcrosticPoemFourth = "이틀뒤에",
                AcrosticPoemFifth = "트와이스랑 학교에 온데요~~",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017,3,30)
            };
            mockRepository.Setup(x => x.Add(It.IsAny<OneYearAnniversaryEntry>())).Returns(entry);

            //action
            var result = service.CreateOneYearAnniversaryEntry(entry);

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<OneYearAnniversaryEntry>()), Times.Once);
            mockRepository.Verify(x => x.Any(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x=>x.Filter(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x=>x.FirstOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
            mockRepository.Verify(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x=>x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Update(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Save(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(result.Name, entry.Name);
            Assert.Same(result.CongratulationMessage, entry.CongratulationMessage);
            Assert.True(result.SamsoniteId == entry.SamsoniteId);
        }

        [Fact(DisplayName = "오행시짓기 댓글이벤트 특정 데이터 가져오기 테스트")]
        public void GetOneYearAnniversary() {
            //arrange
            var entry = new OneYearAnniversaryEntry {
                Id = 1,
                AcrosticPoemFirst = "쌤~~",
                AcrosticPoemSecond = "소식들었나용??",
                AcrosticPoemThird = "나인뮤지스가",
                AcrosticPoemFourth = "이틀뒤에",
                AcrosticPoemFifth = "트와이스랑 학교에 온데요~~",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 3, 30)
            };
            var anotherEntry = new OneYearAnniversaryEntry {
                Id = 2,
                AcrosticPoemFirst = "쌤스미스",
                AcrosticPoemSecond = "소녀시대",
                AcrosticPoemThird = "나인뮤지스",
                AcrosticPoemFourth = "이동욱",
                AcrosticPoemFifth = "트와이스",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트2",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 3, 30)
            };
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>())).Returns(entry);

            //action
            var result = service.GetOneYearAnniversaryEntry(entry.Id);

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Any(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
            mockRepository.Verify(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Save(), Times.Never);

            Assert.NotNull(result);
            Assert.Equal(entry.Id, result.Id);
            Assert.NotEqual(anotherEntry.Id, result.Id);
            Assert.Same(entry, result);
        }
        [Fact(DisplayName = "오행시짓기 댓글이벤트 전체 데이터 가져오기 테스트")]
        public void GetOneYearAnniversaryAll() {
            //arrange
            var entry = new OneYearAnniversaryEntry {
                Id = 1,
                AcrosticPoemFirst = "쌤~~",
                AcrosticPoemSecond = "소식들었나용??",
                AcrosticPoemThird = "나인뮤지스가",
                AcrosticPoemFourth = "이틀뒤에",
                AcrosticPoemFifth = "트와이스랑 학교에 온데요~~",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 3, 30)
            };
            var list = new List<OneYearAnniversaryEntry>();
            list.Add(entry);
            mockRepository.Setup(x => x.GetAll()).Returns(list.AsQueryable());

            //action
            var result = service.GetOneYearAnniversaryEntryAll();

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Any(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Once);
            mockRepository.Verify(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Update(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Save(), Times.Never);

            Assert.NotNull(result);            
            Assert.True(result.Count == 1);
        }
        [Fact(DisplayName = "오행시짓기 댓글이벤트 특정 데이터 변경하기 테스트")]
        public void UpdateOneYearAnniversary() {
            //arrange
            var entry = new OneYearAnniversaryEntry {
                Id = 1,
                AcrosticPoemFirst = "쌤~~",
                AcrosticPoemSecond = "소식들었나용??",
                AcrosticPoemThird = "나인뮤지스가",
                AcrosticPoemFourth = "이틀뒤에",
                AcrosticPoemFifth = "트와이스랑 학교에 온데요~~",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 3, 30)
            };
            var beforeCongratulationMessage = entry.CongratulationMessage;
            var updateEntry = entry;
            updateEntry.CongratulationMessage = "수정!";
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>())).Returns(entry);
            mockRepository.Setup(x => x.Update(It.IsAny<OneYearAnniversaryEntry>())).Returns(updateEntry);

            //action
            var result = service.UpdateOneYearAnniversaryEntry(entry);

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Any(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
            mockRepository.Verify(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<OneYearAnniversaryEntry>()), Times.Once);
            mockRepository.Verify(x => x.Save(), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(updateEntry.CongratulationMessage, result.CongratulationMessage);
            Assert.NotEqual(beforeCongratulationMessage, result.CongratulationMessage);
            
        }
        [Fact(DisplayName = "오행시짓기 댓글이벤트 특정 데이터 삭제하기 테스트")]
        public void DeleteOneYearAnniversary() {
            //arrange
            var entry = new OneYearAnniversaryEntry {
                Id = 1,
                AcrosticPoemFirst = "쌤~~",
                AcrosticPoemSecond = "소식들었나용??",
                AcrosticPoemThird = "나인뮤지스가",
                AcrosticPoemFourth = "이틀뒤에",
                AcrosticPoemFifth = "트와이스랑 학교에 온데요~~",
                CongratulationMessage = "1주년 축하드려용^^",
                Name = "테스트",
                Mobile = "01012345678",
                SamsoniteId = "test",
                Channel = "web",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 3, 30)
            };
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>())).Returns(entry);
            mockRepository.Setup(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()));

            //action
            service.DeleteOneYearAnniversaryEntry(entry.Id);

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<OneYearAnniversaryEntry>()), Times.Never);
            mockRepository.Verify(x => x.Any(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
            //mockRepository.Verify(x => x.Remove(It.IsAny<OneYearAnniversaryEntry>()), Times.Once);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<OneYearAnniversaryEntry, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<OneYearAnniversaryEntry>()), Times.Once);
            mockRepository.Verify(x => x.Save(), Times.Once);
            
        }
    }
}
