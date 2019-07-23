using Xunit;
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
using KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Repository.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;

namespace KinderJoy.Test.Service.NinjaBarbie2016 {
    public class MagicKinderAppLaunchingServiceTest
    {
        private Mock<IMagicKinderAppLaunchingRepository> mockRepository;
        private IMagicKinderAppLaunchingService service;

        public MagicKinderAppLaunchingServiceTest() {
            mockRepository = new Mock<IMagicKinderAppLaunchingRepository>();
            
            service = new MagicKinderAppLaunchingService(mockRepository.Object);
        }

        private List<MagicKinderAppLaunching> CreateMagicKinderLaunchingList() {
            var magicKinderList = new List<MagicKinderAppLaunching>();
            magicKinderList.Add(new MagicKinderAppLaunching
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
                Comment = "너무 재밌어요",
                IsShow = true,
                ScreenShot = "https://www.kinderjoy.co.kr/image/test1",
                ScreenShotType = MagicKinderScreenShotTypes.Coloring,
                CreateDate = new DateTime(2017, 2, 9)
            });
            magicKinderList.Add(new MagicKinderAppLaunching
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
                Comment = "너무 신기하네요",
                IsShow = false,
                ScreenShot = "https://www.kinderjoy.co.kr/image/test2",
                ScreenShotType = MagicKinderScreenShotTypes.EarthTrip,
                CreateDate = new DateTime(2017, 2, 10)
            });
            return magicKinderList;
        }

        [Fact(DisplayName = "매직킨더앱런칭 이벤트 - 참여자 정보 저장")]
        public void CreateMagicKinderLaunchingTest()
        {
            /* arrange : 저장할 참여자 정보 */
            var user = CreateMagicKinderLaunchingList()[0];
            mockRepository.Setup(x => x.Add(It.IsAny<MagicKinderAppLaunching>())).Returns(user);
            mockRepository.Setup(x => x.Save());

            /* action : 참여자 정보 저장 서비스 실행 */
            var result = service.CreateMagicKinderAppLaunching(user);

            /* assert : 검증 */
            Assert.NotNull(result);
            mockRepository.Verify(x => x.Add(It.IsAny<MagicKinderAppLaunching>()), Times.Once);
            mockRepository.Verify(x => x.Save(), Times.Once);
        }
        
        [Fact(DisplayName = "매직킨더앱런칭 이벤트 - 노출여부 수정")]
        public void UpdateIsSHowMagicKinderLaunchingTest()
        {
            var magicKinder = CreateMagicKinderLaunchingList()[0];
            /* arrange : 수정할 참여자 정보 */
            var oldIsShow = magicKinder.IsShow;

            mockRepository.Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<MagicKinderAppLaunching, bool>>>())).Returns(magicKinder);
            mockRepository.Setup(x => x.Update(It.IsAny<MagicKinderAppLaunching>())).Returns(magicKinder);
            mockRepository.Setup(x => x.Save());

            /* action : 노출여부 수정 서비스 실행 */
            var result = service.UpdateIsShowMagicKinderAppLaunching(magicKinder.Id);

            /* assert : 검증 */
            Assert.NotNull(result);
            Assert.NotEqual(oldIsShow, result.IsShow);
            mockRepository.Verify(x => x.SingleOrDefault(It.IsAny<Expression<Func<MagicKinderAppLaunching, bool>>>()), Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<MagicKinderAppLaunching>()), Times.Once);
            mockRepository.Verify(x => x.Save(), Times.Once);
        }
    }
}