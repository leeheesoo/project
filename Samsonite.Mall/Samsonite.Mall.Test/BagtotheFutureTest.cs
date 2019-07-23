using Moq;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Repositories.BagtotheFuture;
using Samsonite.Mall.Domain.Services.BagtotheFuture;
using Samsonite.Mall.Test.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Samsonite.Mall.Test {
    public class BagtotheFutureTest {
        private Mock<IBagtotheFutureEntryRepository> bttfEntryRepo;
        private Mock<IBagtotheFutureSnsUserRepository> bttfSnsUserRepo;
        private Mock<IBagtotheFutureSnsRepository> bttfSnsRepo;
        private BagtotheFutureService service;

        private CustomHttpPostedFileBase common;

        public BagtotheFutureTest() {
            bttfEntryRepo = new Mock<IBagtotheFutureEntryRepository>();
            bttfSnsUserRepo = new Mock<IBagtotheFutureSnsUserRepository>();
            bttfSnsRepo = new Mock<IBagtotheFutureSnsRepository>(); 
            service = new BagtotheFutureService(bttfEntryRepo.Object, bttfSnsUserRepo.Object, bttfSnsRepo.Object);
        }

        #region property
        private IList<BagtotheFutureEntry> BagtotheFutureEntryList {
            get {

                List<BagtotheFutureEntry> list = new List<BagtotheFutureEntry>();
                list.Add(new BagtotheFutureEntry {
                    Id = 1,
                    Name = "테스트1",
                    Mobile = "01012345678",
                    Email = "test1@mz.co.kr",
                    IdeaName = "카페백팩",
                    IdeaDescription = "커피나 음료를 마시다가 백팩에 안전하게 넣을 수 있음 좋겠어요",
                    IpAddress = "127.0.0.1",
                    Channel = "web",
                    CreateDate = new DateTime(2017, 6, 20)
                });
                list.Add(new BagtotheFutureEntry {
                    Id = 2,
                    Name = "테스트2",
                    Mobile = "01077772222",
                    Email = "test2@mz.co.kr",
                    IdeaName = "겉싸개가 붙어있는 캐리어",
                    IdeaDescription = "캐리어가 수화물 왔다갔다 할때마다 상처나고 스크래치가 나지 않게 하기 위해 겉싸개가 아예 붙어 있었음 좋겠다.",
                    IpAddress = "127.0.0.1",
                    Channel = "web",
                    CreateDate = new DateTime(2017, 6, 21)
                });
                list.Add(new BagtotheFutureEntry {
                    Id = 3,
                    Name = "테스트3",
                    Mobile = "01082821004",
                    Email = "test3@mz.co.kr",
                    IdeaName = "캐리어&백팩 아이디어",
                    IdeaDescription = "뭐가이쓰까..",
                    IpAddress = "127.0.0.1",
                    Channel = "mobile",
                    CreateDate = new DateTime(2017, 6, 22)
                });
                return list;
            }
        }
        private IList<BagtotheFutureSnsUser> BagtotheFutureSnsUserList {
            get {                
                List<BagtotheFutureSnsUser> list = new List<BagtotheFutureSnsUser>();
                list.Add(new BagtotheFutureSnsUser {
                    Id = 1,
                    Name = "테스트1",
                    Mobile = "01012345678",
                    IpAddress = "127.0.0.1",
                    Channel = "web",
                    CreateDate = new DateTime(2017,6,20)
                });
                list.Add(new BagtotheFutureSnsUser {
                    Id = 2,
                    Name = "테스트2",
                    Mobile = "01077772222",
                    IpAddress = "127.0.0.1",
                    Channel = "web",
                    CreateDate = new DateTime(2017, 6, 21)
                });
                list.Add(new BagtotheFutureSnsUser {
                    Id = 3,
                    Name = "테스트3",
                    Mobile = "01082821004",
                    IpAddress = "127.0.0.1",
                    Channel = "mobile",
                    CreateDate = new DateTime(2017, 6, 22)
                });
                return list;
            }
        }
        private IList<BagtotheFutureSns> BagtotheFutureSnsList {
            get {
                List<BagtotheFutureSns> list = new List<BagtotheFutureSns>();
                list.Add(new BagtotheFutureSns {
                    Id = 100,
                    SnsType = Domain.Entities.Abstract.SnsType.Facebook,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 1,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x=>x.Id == 1),
                    CreateDate = new DateTime(2017, 6, 20)
                });
                list.Add(new BagtotheFutureSns {
                    Id = 101,
                    SnsType = Domain.Entities.Abstract.SnsType.KakaoStory,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 1,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 1),
                    CreateDate = new DateTime(2017, 6, 20)
                });
                list.Add(new BagtotheFutureSns {
                    Id = 102,
                    SnsType = Domain.Entities.Abstract.SnsType.Kakaotalk,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 1,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 1),
                    CreateDate = new DateTime(2017, 6, 20)
                });
                list.Add(new BagtotheFutureSns {
                    Id = 103,
                    SnsType = Domain.Entities.Abstract.SnsType.Facebook,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 2,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 2),
                    CreateDate = new DateTime(2017, 6, 21)
                });
                list.Add(new BagtotheFutureSns {
                    Id = 104,
                    SnsType = Domain.Entities.Abstract.SnsType.KakaoStory,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 2,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 2),
                    CreateDate = new DateTime(2017, 6, 21)
                });
                list.Add(new BagtotheFutureSns {
                    Id = 105,
                    SnsType = Domain.Entities.Abstract.SnsType.Kakaotalk,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 2,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 2),
                    CreateDate = new DateTime(2017, 6, 21)
                });

                list.Add(new BagtotheFutureSns {
                    Id = 106,
                    SnsType = Domain.Entities.Abstract.SnsType.Facebook,
                    SnsId = "youma",
                    SnsName = "유마",
                    Post = "http://",
                    UserId = 3,
                    User = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 3),
                    CreateDate = new DateTime(2017, 6, 22)
                });
                return list;
            }
        }
        #endregion

        #region create
        [Fact(DisplayName = "백투더퓨처 공모전 아이디어 참여정보 저장 테스트")]
        public void TryCreateBagtotheFutureEntry() {
            //arrange
            var entry = this.BagtotheFutureEntryList[0];
            common = new Infrastructure.CustomHttpPostedFileBase("application/msword", "test.doc");
            string fileurl = common.S3FileUpload(common.InputStream, "images/bag-to-the-future/ideads", common.FileName, false);
            entry.File = fileurl;
            bttfEntryRepo.Setup(x => x.Add(It.IsAny<BagtotheFutureEntry>())).Returns(entry);

            //action
            var result = service.CreateBagtotheFutureEntry(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(entry.IdeaName, result.IdeaName);
            Assert.True(entry.Id == result.Id);
            Assert.Same(entry, result);

            bttfEntryRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureEntry>()), Times.Once);
            bttfEntryRepo.Verify(x => x.Save(), Times.Once);
            bttfEntryRepo.Verify(x => x.GetAll(), Times.Never);
            bttfEntryRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureEntry>()), Times.Never);
            bttfEntryRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureEntry>()), Times.Never);
        }
        [Fact(DisplayName = "백투더퓨처 sns공유 참여정보 저장 테스트")]
        public void TryCreateBagtotheFutureSnsUser() {
            //arrange
            var entry = this.BagtotheFutureSnsUserList[0];
            bttfSnsUserRepo.Setup(x => x.Add(It.IsAny<BagtotheFutureSnsUser>())).Returns(entry);
            //action
            var result = service.CreateBagtotheFutureSnsUser(entry);
            //assert
            Assert.NotNull(result);
            Assert.Equal(entry.Name, result.Name);
            Assert.True(entry.Id == result.Id);
            Assert.Same(entry, result);

            bttfSnsUserRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSnsUser>()), Times.Once);
            bttfSnsUserRepo.Verify(x => x.Save(), Times.Once);
            bttfSnsUserRepo.Verify(x => x.GetAll(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
        }

        [Fact(DisplayName ="백투더퓨처 sns공유 정보 저장 테스트")]
        public void TryCreateBagtotheFutureSns() {
            //arrange
            var entry = BagtotheFutureSnsList[0];
            var entryUser = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == entry.UserId);
            bttfSnsUserRepo.Setup(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>())).Returns(entryUser);
            bttfSnsRepo.Setup(x => x.Add(It.IsAny<BagtotheFutureSns>())).Returns(entry);

            //action
            var result = service.CreateBagtotheFutureSns(entry);

            //assert
            Assert.NotNull(result);
            Assert.Equal(entryUser.Id, result.User.Id);
            Assert.Matches(entry.SnsTypeDisplayName, result.SnsTypeDisplayName);

            bttfSnsRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSns>()), Times.Once);
            bttfSnsRepo.Verify(x => x.Save(), Times.Once);
            bttfSnsRepo.Verify(x => x.GetAll(), Times.Never);
            bttfSnsRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSns>()), Times.Never);
            bttfSnsRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSns>()), Times.Never);
            
            bttfSnsUserRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Save(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.GetAll(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Once);
            bttfSnsUserRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);

        }
        [Fact(DisplayName = "백투더퓨처 sns공유 정보 저장 실패 테스트")]
        public void TryCreateBagtotheFutureSnsFailed() {
            //arrange
            var entry = BagtotheFutureSnsList[0];
            var entryUser = BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == 1000);
            bttfSnsUserRepo.Setup(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>())).Returns(entryUser);
            bttfSnsRepo.Setup(x => x.Add(It.IsAny<BagtotheFutureSns>())).Returns(entry);

            //assert
            var result = Assert.Throws<BagtotheFutureServiceException>(()=> {
                //action
                service.CreateBagtotheFutureSns(entry);
            });

            Assert.NotNull(result);
            Assert.Equal("400", result.Code);
            Assert.Contains("sns공유 절차가 잘못되었습니다.", result.Message);
        }
        #endregion        

        #region read
        [Fact(DisplayName = "백투더퓨처 공모전 아이디어 특정 참여자 정보 가져오기 테스트")]
        public void TryGetBagtotheFutureEntryById() {
            //arrange
            var testId = 1;
            bttfEntryRepo.Setup(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()))
                .Returns(BagtotheFutureEntryList.SingleOrDefault(x => x.Id == testId));

            //action
            var result = service.GetBagtotheFutureEntryById(testId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(testId, result.Id);

            bttfEntryRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureEntry>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Save(), Times.Never);
            bttfEntryRepo.Verify(x => x.GetAll(), Times.Never);
            bttfEntryRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureEntry>()), Times.Never);
            bttfEntryRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Once);
            bttfEntryRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureEntry>()), Times.Never);
        }
        [Fact(DisplayName = "백투더퓨처 공모전 아이디어 참여자 정보 리스트 가져오기 테스트")]
        public void TryGetBagtotheFutureEntries() {
            //arrange
            bttfEntryRepo.Setup(x => x.GetAll()).Returns(BagtotheFutureEntryList.AsQueryable());

            //action
            var result = service.GetBagtotheFutureEntries();

            //assert
            Assert.NotNull(result);
            Assert.Matches(BagtotheFutureEntryList.FirstOrDefault(x => x.Name == "테스트1").Mobile
                , result.FirstOrDefault(x => x.Name == "테스트1").Mobile);

            bttfEntryRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureEntry>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Save(), Times.Never);
            bttfEntryRepo.Verify(x => x.GetAll(), Times.Once);
            bttfEntryRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureEntry>()), Times.Never);
            bttfEntryRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureEntry, bool>>>()), Times.Never);
            bttfEntryRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureEntry>()), Times.Never);
        }
        [Fact(DisplayName = "백투더퓨처 sns공유 특정 참여자 정보 가져오기 테스트")]
        public void TryGetBagtotheFutureSnsUserById() {
            //arrange
            var testId = 3;
            bttfSnsUserRepo.Setup(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()))
                .Returns(BagtotheFutureSnsUserList.SingleOrDefault(x => x.Id == testId));

            //action
            var result = service.GetBagtotheFutureSnsUserById(testId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(testId, result.Id);

            bttfSnsUserRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Save(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.GetAll(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Once);
            bttfSnsUserRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
        }
        [Fact(DisplayName = "백투더퓨처 sns공유 참여자 정보 가져오기 테스트")]
        public void TryGetBagtotheFutureSnsUsers() {
            //arrange
            bttfSnsUserRepo.Setup(x => x.GetAll()).Returns(BagtotheFutureSnsUserList.AsQueryable());

            //action
            var result = service.GetBagtotheFutureSnsUsers();

            //assert
            Assert.NotNull(result);
            Assert.Matches(BagtotheFutureSnsUserList.FirstOrDefault(x => x.Name == "테스트1").Mobile
                , result.FirstOrDefault(x => x.Name == "테스트1").Mobile);

            bttfSnsUserRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Save(), Times.Never);
            bttfSnsUserRepo.Verify(x => x.GetAll(), Times.Once);
            bttfSnsUserRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSnsUser, bool>>>()), Times.Never);
            bttfSnsUserRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSnsUser>()), Times.Never);
        }

        [Fact(DisplayName = "백투더퓨처 특정 참여자의 sns공유 정보 가져오기 테스트")]
        public void TryGetBagtotheFutureSnsByUserId() {
            //arrange
            long testUserId = 1;
            bttfSnsRepo.Setup(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()))
                .Returns(BagtotheFutureSnsList.Where(x => x.UserId == testUserId).AsQueryable());

            //action
            var result = service.GetBagtotheFutureSnsByUserId(testUserId);

            //assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.Equal(testUserId, result[0].UserId);
            Assert.True(result.Any(x => x.UserId == testUserId));

            bttfSnsRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSns>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Save(), Times.Never);
            bttfSnsRepo.Verify(x => x.GetAll(), Times.Never);
            bttfSnsRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Once);
            bttfSnsRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSns>()), Times.Never);
            bttfSnsRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSns>()), Times.Never);

        }
        [Fact(DisplayName = "백투더퓨처 sns공유 정보 가져오기 테스트")]
        public void TryGetBagtotheFutureSns() {
            //arrange
            bttfSnsRepo.Setup(x => x.GetAll()).Returns(BagtotheFutureSnsList.AsQueryable());

            //action
            var result = service.GetBagtotheFutureSns();

            //assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.Equal(BagtotheFutureSnsList.OrderBy(x=>x.Id).FirstOrDefault().User.Name,
                result.OrderBy(x => x.Id).FirstOrDefault().User.Name);

            bttfSnsRepo.Verify(x => x.Add(It.IsAny<BagtotheFutureSns>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Save(), Times.Never);
            bttfSnsRepo.Verify(x => x.GetAll(), Times.Once);
            bttfSnsRepo.Verify(x => x.Any(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.FirstOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Filter(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Remove(It.IsAny<BagtotheFutureSns>()), Times.Never);
            bttfSnsRepo.Verify(x => x.SingleOrDefault(It.IsAny<System.Linq.Expressions.Expression<Func<BagtotheFutureSns, bool>>>()), Times.Never);
            bttfSnsRepo.Verify(x => x.Update(It.IsAny<BagtotheFutureSns>()), Times.Never);
        }
        #endregion

        #region update
        #endregion

        #region delete
        #endregion
    }
}
