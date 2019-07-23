using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Repositories.Managements;
using INGLife.Event.Domain.Services.Managements;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace INGLife.Event.Test.AffiliatesTest {    
    public class AffiliatesTest {
        private Mock<IAffiliatesRepository> repository;
        private AffiliatesService service;

        public AffiliatesTest() {
            repository = new Mock<IAffiliatesRepository>();
            service = new AffiliatesService(repository.Object);
        }

        [Fact(DisplayName = "이벤트 관리 - 제휴사 가져오기")]
        public void TryGetAffiliates() {
            //arrange
            var list = new List<Affiliates> {
                new Affiliates { Name = "레진코믹스" },
                new Affiliates { Name = "골프존" }
            };
            repository.Setup(x => x.GetAll()).Returns(list.AsQueryable());

            //action
            var result = service.GetAffiliates;

            //assert
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.Contains(result[0].Name, "레진코믹스");

            repository.Verify(x => x.GetAll(), Times.Once);
            repository.Verify(x => x.Add(It.IsAny<Affiliates>()), Times.Never);
            repository.Verify(x => x.Filter(It.IsAny<Expression<Func<Affiliates, bool>>>()), Times.Never);
            
        }
    }
}
