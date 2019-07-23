using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;
using KinderJoy.Domain.Repository.MavelFrozen;
using KinderJoy.Domain.Service.MavelFrozen;
using KinderJoy.Domain.Entities.MavelFrozen;

namespace KinderJoy.Test.Service.MavelFrozen {
    public class MavelFrozen2017ServiceTests {

        private Mock<IMavelFrozenUserRepository> mockUserRepository;
        private Mock<IMavelFrozenSnsRepository> mockSNSRepository;
        private IMavelFrozenService service;

        public MavelFrozen2017ServiceTests() {
            mockUserRepository = new Mock<IMavelFrozenUserRepository>();
            mockSNSRepository = new Mock<IMavelFrozenSnsRepository>();
            
            service = new MavelFrozenService(mockUserRepository.Object, mockSNSRepository.Object);
        }

        private List<MavelFrozenUser> CreateUser() {
            var user = new List<MavelFrozenUser>();
            user.Add(new MavelFrozenUser {
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
            user.Add(new MavelFrozenUser {
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
        
        [Fact(DisplayName = "2017 마블프로즌 - 참여자 정보 저장")]
        public void CreateUserTest() {
            /* arrange : 저장할 참여자 정보 */
            var user = CreateUser()[0];
            mockUserRepository.Setup(x => x.Add(It.IsAny<MavelFrozenUser>())).Returns(user);
            mockUserRepository.Setup(x => x.Save());

            /* action : 참여자 정보 저장 서비스 실행 */
            var result = service.CreateUser(user);

            /* assert : 검증 */
            Assert.NotNull(result);
            mockUserRepository.Verify(x => x.Add(It.IsAny<MavelFrozenUser>()), Times.Once);
            mockUserRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "2017 마블프로즌 - SNS공유 정보 저장")]
        public void CreateSNSTest() {
            /* arrange : 저장할 SNS공유 정보 */
            var user = CreateUser()[0];
            mockUserRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<MavelFrozenUser, bool>>>())).Returns(user);
            var sharing = new MavelFrozenSNS {
                Id = 1,
                UserId = user.Id,
                SnsType = Domain.Abstract.SnsType.Facebook,
                SnsId = "1234567",
                SnsName = "테스트",
                Post = "http://facebook.com",
                CreateDate = new DateTime(2017, 2, 10)
            };
            mockSNSRepository.Setup(x => x.Add(It.IsAny<MavelFrozenSNS>())).Returns(sharing);
            mockSNSRepository.Setup(x => x.Save());

            /* action : SNS공유 정보 저장 서비스 실행 */
            var result = service.CreateSNSShare(sharing);

            /* assert : 검증 */
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.UserId);
            mockUserRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<MavelFrozenUser, bool>>>()), Times.Once);
            mockSNSRepository.Verify(x => x.Add(It.IsAny<MavelFrozenSNS>()), Times.Once);
            mockSNSRepository.Verify(x => x.Save(), Times.Once);
        }
    }
}