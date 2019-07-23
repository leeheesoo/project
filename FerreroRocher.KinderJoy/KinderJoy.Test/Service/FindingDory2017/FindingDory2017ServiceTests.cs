using Xunit;
using KinderJoy.Domain.Service.NinjaBarbie2016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;
using KinderJoy.Domain.Repository.FindingDory2017;
using KinderJoy.Domain.Service.FindingDory2017;
using KinderJoy.Domain.Entities.FindingDory2017;

namespace KinderJoy.Test.Service.FindingDory2017 {
    public class FindingDory2017ServiceTests
    {
        private Mock<IFindingDory2017UserRepository> mockUserRepository;
        private Mock<IFindingDory2017SNSRepository> mockSNSRepository;
        private IFindingDory2017Service service;

        public FindingDory2017ServiceTests() {
            mockUserRepository = new Mock<IFindingDory2017UserRepository>();
            mockSNSRepository = new Mock<IFindingDory2017SNSRepository>();
            
            service = new FindingDory2017Service(mockUserRepository.Object, mockSNSRepository.Object);
        }

        private List<FindingDory2017User> CreateUser() {
            var user = new List<FindingDory2017User>();
            user.Add(new FindingDory2017User
            {
                Id = 1,
                Name = "테스트",
                Mobile = "01011111111",
                Age = 28,
                Channel = "pc",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 2, 9)
            });
            user.Add(new FindingDory2017User
            {
                Id = 2,
                Name = "테스트2",
                Mobile = "01012345678",
                Age = 28,
                Channel = "mobile",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀22",
                IpAddress = "127.0.0.1",
                CreateDate = new DateTime(2017, 2, 10)
            });
            return user;
        }
        
        [Fact(DisplayName = "2017 도리를찾아서 - 참여자 정보 저장")]
        public void CreateUserTest() {
            /* arrange : 저장할 참여자 정보 */
            var user = CreateUser()[0];
            mockUserRepository.Setup(x => x.Add(It.IsAny<FindingDory2017User>())).Returns(user);
            mockUserRepository.Setup(x => x.Save());

            /* action : 참여자 정보 저장 서비스 실행 */
            var result = service.CreateUser(user);

            /* assert : 검증 */
            Assert.NotNull(result);
            mockUserRepository.Verify(x => x.Add(It.IsAny<FindingDory2017User>()), Times.Once);
            mockUserRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "2017 도리를찾아서 - SNS공유 정보 저장")]
        public void CreateSNSTest() {
            /* arrange : 저장할 SNS공유 정보 */
            var user = CreateUser()[0];
            mockUserRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<FindingDory2017User, bool>>>())).Returns(user);
            var sharing = new FindingDory2017SNS
            {
                Id = 1,
                UserId = user.Id,
                SnsType = Domain.Abstract.SnsType.Facebook,
                SnsId = "1234567",
                SnsName = "테스트",
                Post = "http://facebook.com",
                CreateDate = new DateTime(2017, 2, 10)
            };
            mockSNSRepository.Setup(x => x.Add(It.IsAny<FindingDory2017SNS>())).Returns(sharing);
            mockSNSRepository.Setup(x => x.Save());

            /* action : SNS공유 정보 저장 서비스 실행 */
            var result = service.CreateSNSShare(sharing);

            /* assert : 검증 */
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.UserId);
            mockUserRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<FindingDory2017User, bool>>>()), Times.Once);
            mockSNSRepository.Verify(x => x.Add(It.IsAny<FindingDory2017SNS>()), Times.Once);
            mockSNSRepository.Verify(x => x.Save(), Times.Once);
        }
    }
}