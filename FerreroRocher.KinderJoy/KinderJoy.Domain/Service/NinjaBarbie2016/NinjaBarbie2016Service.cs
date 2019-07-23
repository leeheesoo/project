using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Repository.NinjaBarbie2016;
using KinderJoy.Domain.Models.NinjaBarbie2016;
using LinqKit;

namespace KinderJoy.Domain.Service.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 - Service
    /// </summary>
    public class NinjaBarbie2016Service : INinjaBarbie2016Service {

        private INinjaBarbie2016UserRepository userRepository;
        private INinjaBarbie2016SharingRepository sharingRepository;
        public NinjaBarbie2016Service(INinjaBarbie2016UserRepository userRepository, INinjaBarbie2016SharingRepository sharingRepository) {
            this.userRepository = userRepository;
            this.sharingRepository = sharingRepository;
        }

        #region 이벤트페이지
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public NinjaBarbie2016User CreateUser(NinjaBarbie2016User entry) {
            var user = userRepository.Add(entry);
            userRepository.Save();
            return user;
        }

        /// <summary>
        /// 사용자 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public IQueryable<NinjaBarbie2016User> GetUsers() {
            return userRepository.GetAll();
        }

        /// <summary>
        /// 서프라이즈 꾸미기 데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public NinjaBarbie2016User UpdateUser(NinjaBarbie2016User entry) {
            var user = userRepository.SingleOrDefault(x => x.Id == entry.Id);
            if (user == null) {
                throw new NinjaBarbie2016ServiceException("500", "참여자 정보가 잘못되었습니다.", entry);
            }
            var updateUser = userRepository.Update(entry);
            userRepository.Save();
            return updateUser;
        }

        /// <summary>
        /// 공유데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public NinjaBarbie2016Sharing CreateSharing(NinjaBarbie2016Sharing entry) {
            var user = userRepository.SingleOrDefault(x => x.Id == entry.UserId);
            if (user == null) {
                throw new NinjaBarbie2016ServiceException("500", "참여자 정보가 잘못되었습니다.", entry);
            }
            var sharing = sharingRepository.Add(entry);
            sharingRepository.Save();
            return sharing;
        }
        #endregion

        #region 관리자
        public IQueryable<NinjaBarbie2016User> GetUsers(AdminNinjaBarbie2016UserQueryOptions options) {
            var predicate = options.BuildPredicate();
            return userRepository.GetAll().AsExpandable().Where(predicate).AsQueryable();
        }
        public IQueryable<NinjaBarbie2016Sharing> GetSharings(AdminNinjaBarbie2016SharingQueryOptions options) {
            var predicate = options.BuildPredicate();
            return sharingRepository.GetAll().AsExpandable().Where(predicate).AsQueryable();
        }
        public IList<AdminNinjaBarbie2016StatisticsViewModel> GetStatistics() {
            var query = userRepository.GetAll()
                .GroupJoin(sharingRepository.GetAll(), e => e.Id, p => p.UserId, (e, p) => new {
                    Id = e.Id,
                    Mobile = e.Mobile,
                    TotalCount = p.Count(),
                    SurprizeType = e.SurprizeType,
                    FacebookCount = p.Where(g => g.SnsType == Abstract.SnsType.Facebook).Count(),
                    KakaotalkCount = p.Where(g => g.SnsType == Abstract.SnsType.Kakaotalk).Count(),
                    KakaostoryCount = p.Where(g => g.SnsType == Abstract.SnsType.Kakaostory).Count(),
                }).GroupBy(x => x.Mobile).Select(x => new {
                    Id = x.Max(e => e.Id),
                    Mobile = x.Key,
                    SurprizeType = x.Count(e => e.SurprizeType == NinjaBarbieSurprizeType.NINJA) >= x.Count(e => e.SurprizeType == NinjaBarbieSurprizeType.BARBIE)? NinjaBarbieSurprizeType.NINJA : NinjaBarbieSurprizeType.BARBIE,
                    TotalCount = x.Sum(e => e.TotalCount),
                    FacebookCount = x.Sum(e => e.FacebookCount),
                    KakaotalkCount = x.Sum(e => e.KakaotalkCount),
                    KakaostoryCount = x.Sum(e => e.KakaostoryCount),
                }).Join(userRepository.GetAll(), j => j.Id, i => i.Id, (j, i) => new AdminNinjaBarbie2016StatisticsViewModel {
                    Mobile = i.Mobile,
                    Name = i.Name,
                    SurprizeType = j.SurprizeType,
                    Age = i.Age,
                    Address = i.Address,
                    AddressDetail = i.AddressDetail,
                    ZipCode = i.ZipCode,
                    TotalCount = j.TotalCount,
                    FacebookCount = j.FacebookCount,
                    KakaotalkCount = j.KakaotalkCount,
                    KakaostoryCount = j.KakaostoryCount
                });
            return query.OrderByDescending(x=>x.TotalCount).ToList();
        }
        #endregion
    }

    public class NinjaBarbie2016ServiceException : EventServiceException {
        public NinjaBarbie2016ServiceException(string code, string message, object data) : base(code, message, data) {
        }
    }
}