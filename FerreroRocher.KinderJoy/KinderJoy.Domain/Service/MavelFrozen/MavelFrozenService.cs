using KinderJoy.Domain.Entities.MavelFrozen;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Repository.MavelFrozen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Models.MarvelFrozen;
using LinqKit;

namespace KinderJoy.Domain.Service.MavelFrozen {
    public class MavelFrozenService : IMavelFrozenService {
        private IMavelFrozenUserRepository userRepository;
        private IMavelFrozenSnsRepository snsRepository;

        public MavelFrozenService(IMavelFrozenUserRepository userRepository, IMavelFrozenSnsRepository snsRepository) {
            this.userRepository = userRepository;
            this.snsRepository = snsRepository;
        }

        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MavelFrozenSNS CreateSNSShare(MavelFrozenSNS entry) {
            var user = userRepository.SingleOrDefault(x => x.Id == entry.UserId);
            if (user == null) {
                throw new EventServiceException("500", "참여자 정보가 잘못되었습니다.", entry);
            }

            var result = snsRepository.Add(entry);
            snsRepository.Save();
            return result;
        }

        /// <summary>
        /// SNS공유 정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MavelFrozenUser CreateUser(MavelFrozenUser entry) {
            var result = userRepository.Add(entry);
            userRepository.Save();
            return result;
        }

        public IQueryable<MavelFrozenUser> GetUser(AdminMarvelFrozenQueryOptions options) {
            return userRepository.GetAll().AsExpandable().Where(options.BuildPredicate());
        }

        public IQueryable<MavelFrozenSNS> GetSns(AdminMarvelFrozenSNSQueryOptions options) {
            return snsRepository.GetAll().AsExpandable().Where(options.BuildPredicate());
        }

        public IQueryable<AdminMarvelFrozenStatsViewModel> GetStatistics() {
            var sns = userRepository.GetAll()
                .Join(snsRepository.GetAll(), e => e.Id, p => p.UserId, (e, p) => new { SnsType = p.SnsType, Mobile = e.Mobile, Name = e.Name, ChildGender = e.ChildGender, Age = e.Age });

            var query = from s in sns
                        group s by s.Mobile into MarvelFrozenSns
                        select new AdminMarvelFrozenStatsViewModel {
                            Mobile = MarvelFrozenSns.Key,
                            ChildGender = MarvelFrozenSns.Count(e => e.ChildGender == GenderType.Boy) >= MarvelFrozenSns.Count(e=> e.ChildGender == GenderType.Girl) ? GenderType.Boy : GenderType.Girl,
                            Name = MarvelFrozenSns.Max(e => e.Name),
                            Age = MarvelFrozenSns.Max(e => e.Age),
                            FacebookCount = MarvelFrozenSns.Count(e => e.SnsType == Abstract.SnsType.Facebook),
                            KakaostoryCount = MarvelFrozenSns.Count(e => e.SnsType == Abstract.SnsType.Kakaostory),
                            KakaotalkCount = MarvelFrozenSns.Count(e => e.SnsType == Abstract.SnsType.Kakaotalk),
                            TotalCount = MarvelFrozenSns.Count()
                        };

            return query;
        }
    }
}
