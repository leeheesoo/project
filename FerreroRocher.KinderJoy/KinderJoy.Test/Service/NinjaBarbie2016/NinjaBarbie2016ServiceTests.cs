using Xunit;
using KinderJoy.Domain.Service.NinjaBarbie2016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using KinderJoy.Domain.Repository.NinjaBarbie2016;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using System.Linq.Expressions;

namespace KinderJoy.Test.Service.FindingDory2017
{

    /// <summary>
    /// 닌자바비 2016 - 단위 테스트
    /// </summary>
    public class NinjaBarbie2016ServiceTests {
        private Mock<INinjaBarbie2016UserRepository> mockUserRepository;
        private Mock<INinjaBarbie2016SharingRepository> mockSharingRepository;
        private INinjaBarbie2016Service service;

        public NinjaBarbie2016ServiceTests() {
            mockUserRepository = new Mock<INinjaBarbie2016UserRepository>();
            mockSharingRepository = new Mock<INinjaBarbie2016SharingRepository>();
            
            service = new NinjaBarbie2016Service(mockUserRepository.Object, mockSharingRepository.Object);
        }

        private List<NinjaBarbie2016User> CreateUser() {
            var user = new List<NinjaBarbie2016User>();
            user.Add(new NinjaBarbie2016User {
                Id = 1,
                Name = "테스트",
                Mobile = "01012345678",
                Age = 28,
                Channel = "web",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀",
                CreateDate = new DateTime(2016, 11, 15),
                SurprizeType = NinjaBarbieSurprizeType.NINJA         
            });
            user.Add(new NinjaBarbie2016User {
                Id = 2,
                Name = "테스트2",
                Mobile = "01098765432",
                Age = 28,
                Channel = "mobile",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀22",
                CreateDate = new DateTime(2016, 11, 16),
                SurprizeType = NinjaBarbieSurprizeType.BARBIE
            });
            return user;
        }
        
        [Fact(DisplayName = "2016 닌자바비 - 참여자 정보 저장")]
        public void NinjaBarbie2016CreateUserTest() {
            /* arrange : 저장할 참여자 정보 */
            var user = CreateUser()[0];
            mockUserRepository.Setup(x => x.Add(It.IsAny<NinjaBarbie2016User>())).Returns(user);
            mockUserRepository.Setup(x => x.Save());

            /* action : 참여자 정보 저장 서비스 실행 */
            var result = service.CreateUser(user);

            /* assert : 검증 */
            Assert.NotNull(result);
            mockUserRepository.Verify(x => x.Add(It.IsAny<NinjaBarbie2016User>()), Times.Once);
            mockUserRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "2016 닌자바비 - 참여자 정보에 서프라이즈 정보 업데이트")]
        public void NinjaBarbie2016UpdateUserTest() {
            /* arrange : 저장할 참여자 정보 */
            var user = new NinjaBarbie2016User {
                Id = 1,
                Name = "테스트",
                Mobile = "01012345678",
                Age = 28,
                Channel = "web",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀",
                CreateDate = new DateTime(2016, 11, 15),
                SurprizeType = NinjaBarbieSurprizeType.NINJA
            };
            mockUserRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<NinjaBarbie2016User, bool>>>())).Returns(user);
            user.Top = 1;
            user.Bottom = 1;
            user.Accessory = 1;
            mockUserRepository.Setup(x => x.Update(It.IsAny<NinjaBarbie2016User>())).Returns(user);
            mockUserRepository.Setup(x => x.Save());
            
            /* action : 참여자 정보 저장 서비스 실행 */
            var result = service.UpdateUser(user);

            /* assert : 검증 */
            Assert.NotNull(result);
            Assert.Equal(user.Top, result.Top);
            mockUserRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<NinjaBarbie2016User, bool>>>()), Times.Once);
            mockUserRepository.Verify(x => x.Update(It.IsAny<NinjaBarbie2016User>()), Times.Once);
            mockUserRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact(DisplayName = "2016 닌자바비 - SNS공유 정보 저장")]
        public void NinjaBarbie2016CreateSharingTest() {
            /* arrange : 저장할 SNS공유 정보 */
            var user = new NinjaBarbie2016User {
                Id = 1,
                Name = "테스트",
                Mobile = "01012345678",
                Age = 28,
                Channel = "web",
                ZipCode = "12345",
                Address = "서울시 강남구 역삼동 735-22",
                AddressDetail = "갈라빌딩 9층 개발팀",
                CreateDate = new DateTime(2016, 11, 15),
                SurprizeType = NinjaBarbieSurprizeType.NINJA
            };
            mockUserRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<NinjaBarbie2016User, bool>>>())).Returns(user);
            var sharing = new NinjaBarbie2016Sharing {
                Id = 1,
                UserId = user.Id,
                SnsType = Domain.Abstract.SnsType.Facebook,
                SnsId = "1234567",
                SnsName = "테스트",
                Post = "http://facebook.com",
                CreateDate = new DateTime(2016, 11, 15)
            };
            mockSharingRepository.Setup(x => x.Add(It.IsAny<NinjaBarbie2016Sharing>())).Returns(sharing);
            mockSharingRepository.Setup(x => x.Save());

            /* action : SNS공유 정보 저장 서비스 실행 */
            var result = service.CreateSharing(sharing);

            /* assert : 검증 */
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.UserId);
            mockUserRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<NinjaBarbie2016User, bool>>>()), Times.Once);
            mockSharingRepository.Verify(x => x.Add(It.IsAny<NinjaBarbie2016Sharing>()), Times.Once);
            mockSharingRepository.Verify(x => x.Save(), Times.Once);
        }
    }
}