using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.FinancialConsultantSharing;
using INGLife.Event.Domain.Services.FinancialConsultantSharing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace INGLife.Event.Test.FinancialConsultantSharingTests
{
    public class FinancialConsultantSharingServiceTest
    {
        #region variables
        private Mock<IFinancialConsultantRepository> mockFcRepository;
        private Mock<IFinancialConsultantOriginCustomerRepository> mockOriginRepository;
        private Mock<IFinancialConsultantNewCustomerRepository> mockNewRepository;
        private FinancialConsultantSharingService service;        
        #endregion

        #region properties
        #endregion

        #region constructor
        public FinancialConsultantSharingServiceTest () {
            mockFcRepository = new Mock<IFinancialConsultantRepository>();
            mockOriginRepository = new Mock<IFinancialConsultantOriginCustomerRepository>();
            mockNewRepository = new Mock<IFinancialConsultantNewCustomerRepository>();
            service = new FinancialConsultantSharingService(mockFcRepository.Object, mockOriginRepository.Object, mockNewRepository.Object);            
        }
        #endregion

        [Fact(DisplayName = "FC 공유이벤트 - 데이터 세팅")]
        public void TestSaveCertData () {
            var resultKMC = new PrivateKMCertModel { Name = "이희선", Mobile = "01074000328", Gender = "0", BirthDay = "19910328" };
        }

        //[Fact(DisplayName = "FC 공유이벤트 - 최대 기존 참여 인원 테스트")]        
        //public void Limit4000TrueOriginalEntryTest()
        //{
        //    var entry = new FinancialConsultantOriginalCustomerEntry { Name = "이희선", Gender = "0", Mobile = "01074000328", BirthDay = "19910328" };
        //    IList< FinancialConsultantOriginalCustomerEntry > test = new List<FinancialConsultantOriginalCustomerEntry>();
        //    for(var i = 0; i < 4000; i++)
        //    {
        //        test.Add(entry);
        //    }            
        //    mockOriginRepository.Setup(x => x.GetAll()).Returns(test.AsQueryable());

        //    var result = service.limitOriginCustomer();

        //    //4000명일때 True
        //    Assert.NotNull(result);            
        //    Assert.True(result);

        //    test.Add(entry);
        //    mockOriginRepository.Setup(x => x.GetAll()).Returns(test.AsQueryable());

        //    result = service.limitOriginCustomer();

        //    //4001명일때 False
        //    Assert.NotNull(result);
        //    Assert.False(result);
        //}

        [Fact(DisplayName = "FC 공유이벤트 - 최대 신규 참여 인원 테스트")]
        public void Limit2000TrueNewEntryTest()
        {
            var entry = new FinancialConsultantNewCustomerEntry { Name = "이희선", Gender = "0", Mobile = "01074000328", BirthDay = "19910328" };
            IList<FinancialConsultantNewCustomerEntry> test = new List<FinancialConsultantNewCustomerEntry>();
            for (var i = 0; i < 2000; i++)
            {
                test.Add(entry);
            }
            mockNewRepository.Setup(x => x.GetAll()).Returns(test.AsQueryable());

            var result = service.limitNewCustomer();

            //2000명일때 True
            Assert.NotNull(result);
            Assert.True(result);

            test.Add(entry);
            mockNewRepository.Setup(x => x.GetAll()).Returns(test.AsQueryable());

            result = service.limitNewCustomer();

            //2001명일때 False
            Assert.NotNull(result);
            Assert.False(result);
        }

        [Fact(DisplayName = "FC 공유이벤트 - 중복 참여자 테스트")]
        public void DepulicateEntryTest()
        {
            var entry = new FinancialConsultantOriginalCustomerEntry { Name = "이희선", Gender = "0", Mobile = "01074000328", BirthDay = "19910328" };
            mockOriginRepository.Setup(x => x.Add(It.IsAny<FinancialConsultantOriginalCustomerEntry>())).Returns(entry);

            var result = service.depulicateOriginCustomer(entry);

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact(DisplayName = "FC 공유이벤트 - 재무상담사 조회 테스트")]
        public void IsFcEntryTest()
        {
            var fcCode = "00000";
            var entry = new FinancialConsultantEntry { Name = "이희선",FcCode="00000"};
            FinancialConsultantEntry nullValue = null;

            // null 일때
            mockFcRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConsultantEntry, bool>>>())).Returns(nullValue);
            var result = Assert.Throws<EventServiceException>(() => {                
                var entryId = service.isExistFC(fcCode);
            });
            Assert.Equal("FC코드를 다시 한번 확인해주세요", result.Message);

            // null이 아닐때
            mockFcRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FinancialConsultantEntry, bool>>>())).Returns(entry);           
            var test2Result = service.isExistFC(fcCode);
            Assert.True(test2Result);

        }


        public class PrivateKMCertModel {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Gender { get; set; }
            public string BirthDay { get; set; }
        }
    }
}
