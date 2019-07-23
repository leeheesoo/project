using AutoMapper;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Services.FinancialConcertMarketingAgree;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace INGLife.Event.Test.FinancialConcertMarketingAgreeTests {
    public class FinancialConcertMarketingAgreeServiceTest {
        #region variables
        private Mock<IFinancialConcertMarketingAgreeRepository> mockRepository;
        private FinancialConcertMarketingAgreeService service;
        private MapperConfiguration mapperConfig;
        #endregion

        #region properties
        #endregion

        #region constructor
        public FinancialConcertMarketingAgreeServiceTest () {
            mockRepository = new Mock<IFinancialConcertMarketingAgreeRepository>();
            service = new FinancialConcertMarketingAgreeService(mockRepository.Object);

            mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FinancialConcertMarketingAgreeCertModel, FinancialConcertMarketingAgreeEntry>();
            });
        }
        #endregion

        [Fact(DisplayName = "재무콘서트 마케팅동의 - 본인인증 정보 저장")]
        public void TestSaveCertData () {
            //arrange
            var resultKMC = new FinancialConcertMarketingAgreeCertModel { Name = "유미영", Mobile = "01099779790", Gender = "1", BirthDay = "19891109"};
            var entry = mapperConfig.CreateMapper().Map<FinancialConcertMarketingAgreeEntry>(resultKMC);
            FinancialConcertMarketingAgreeEntry single = null;
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>())).Returns(single);
            mockRepository.Setup(x => x.Add(It.IsAny<FinancialConcertMarketingAgreeEntry>())).Returns(entry);

            //action
            var entryId = service.Create(entry);

            //assert
            Assert.NotNull(entryId);
            Assert.True(entryId > -1);

            mockRepository.Verify(x => x.Add(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.Remove(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact(DisplayName = "재무콘서트 마케팅동의 - 본인인증 정보 저장 실패")]
        public void TestFailSaveCertData() {
            //arrange
            var resultKMC = new FinancialConcertMarketingAgreeCertModel { Name = "유미영", Mobile = "01099779790", Gender = "1", BirthDay = "19891109" };
            var entry = mapperConfig.CreateMapper().Map<FinancialConcertMarketingAgreeEntry>(resultKMC);
            FinancialConcertMarketingAgreeEntry single = new FinancialConcertMarketingAgreeEntry { Id = 1, Name = "유미영", Mobile = "01099779790", Gender = "1", BirthDay = "19891109", CreateDate = DateTime.Now, CompleteDate = DateTime.Now  };
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>())).Returns(single);
            mockRepository.Setup(x => x.Add(It.IsAny<FinancialConcertMarketingAgreeEntry>())).Returns(entry);

            //assert
            var result = Assert.Throws<EventServiceException>(() => {
                //action
                var entryId = service.Create(entry);
            });
            Assert.NotNull(result);
            Assert.Equal("400", result.Code);
            Assert.Equal("이미 참여하였습니다.", result.Message);
        }

        [Fact(DisplayName = "재무콘서트 마케팅동의 - 본인인증 정보와 입력한 개인정보 검증")]
        public void TestCheckCertData () {
            //arrange
            var entry = new FinancialConcertMarketingAgreeEntry { Id = 1, Name = "유미영", Mobile = "01099779790", Gender = "1", BirthDay = "19891109", CreateDate = DateTime.Now };
            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>())).Returns(entry);

            //action
            var result = service.CheckCertEntry(1, "유미영", "01099779790", "1", "19891109");

            //assert
            Assert.NotNull(result);
            Assert.Equal(entry.Name, result.Name);
            Assert.Equal(entry.Id, result.Id);
            
            mockRepository.Verify(x => x.Add(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.Update(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.Remove(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact(DisplayName = "재무콘서트 마케팅동의 - 응모데이터 저장")]
        public void TestCompleteCreateEntry () {
            //arrange
            var entry = new FinancialConcertMarketingAgreeEntry { Id = 1, Name = "유미영", Mobile = "01099779790", Gender = "1", BirthDay = "19891109", CreateDate = DateTime.Now };
            var updateEntry = entry;
            updateEntry.RetentionPeriodType = 3;
            updateEntry.ApplicationTurn = FinancialConcertMarketingAgreeApplicationTurnEnum.SECOND;
            updateEntry.PhoneCheck = true;
            updateEntry.MessageCheck = true;
            updateEntry.PostCheck = true;
            updateEntry.CompleteDate = DateTime.Now;

            mockRepository.Setup(x => x.Update(It.IsAny<FinancialConcertMarketingAgreeEntry>())).Returns(updateEntry);

            //action
            service.Update(updateEntry);

            //assert
            mockRepository.Verify(x => x.Add(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.Update(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Once);
            mockRepository.Verify(x => x.Remove(It.IsAny<FinancialConcertMarketingAgreeEntry>()), Times.Never);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.Filter(It.IsAny<Expression<Func<FinancialConcertMarketingAgreeEntry, bool>>>()), Times.Never);
            mockRepository.Verify(x => x.GetAll(), Times.Never);
        }

        public class FinancialConcertMarketingAgreeCertModel {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Gender { get; set; }
            public string BirthDay { get; set; }
        }
    }
}
