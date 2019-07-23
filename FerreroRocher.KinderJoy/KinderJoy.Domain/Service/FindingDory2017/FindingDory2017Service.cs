using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Repository.FindingDory2017;
using KinderJoy.Domain.Exceptions;
using KinderJoy.Domain.Models.FindingDory2017;
using LinqKit;

namespace KinderJoy.Domain.Service.FindingDory2017
{
    public class FindingDory2017Service : IFindingDory2017Service
    {
        private IFindingDory2017UserRepository userRepository;
        private IFindingDory2017SNSRepository snsRepository;
        public FindingDory2017Service(IFindingDory2017UserRepository userRepository, IFindingDory2017SNSRepository snsRepository)
        {
            this.userRepository = userRepository;
            this.snsRepository = snsRepository;
        }

        /// <summary>
        /// 공유 데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public FindingDory2017SNS CreateSNSShare(FindingDory2017SNS entry)
        {
            var user = userRepository.SingleOrDefault(x => x.Id == entry.UserId);
            if(user == null)
            {
                throw new EventServiceException("500", "참여자 정보가 잘못되었습니다.", entry);
            }

            var result = snsRepository.Add(entry);
            snsRepository.Save();
            return result;
        }

        /// <summary>
        /// 개인정보 데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public FindingDory2017User CreateUser(FindingDory2017User entry)
        {
            var result = userRepository.Add(entry);
            userRepository.Save();
            return result;
        }

        /// <summary>
        /// 개인정보 데이터 리스트
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IQueryable<FindingDory2017User> GetUser(AdminFindingDory2017QueryOptions options)
        {
            return userRepository.GetAll().AsExpandable().Where(options.BuildPredicate());
        }

        public IQueryable<FindingDory2017SNS> GetSns(AdminFindingDory2017SNSQueryOptions options)
        {
            return snsRepository.GetAll().AsExpandable().Where(options.BuildPredicate());
        }

        public IQueryable<AdminFindingDory2017StatsViewModel> GetStatistics()
        {
            var sns = userRepository.GetAll()
                .Join(snsRepository.GetAll(), e => e.Id, p => p.UserId, (e,p) => new { SnsType = p.SnsType, Mobile = e.Mobile, Name = e.Name});

            var query = from s in sns
                        group s by s.Mobile into FindingDorySns
                        select new AdminFindingDory2017StatsViewModel
                        {
                            Mobile = FindingDorySns.Key,
                            Name = FindingDorySns.Max(e => e.Name),
                            FacebookCount = FindingDorySns.Count(e => e.SnsType == Abstract.SnsType.Facebook),
                            KakaostoryCount = FindingDorySns.Count(e => e.SnsType == Abstract.SnsType.Kakaostory),
                            KakaotalkCount = FindingDorySns.Count(e => e.SnsType == Abstract.SnsType.Kakaotalk),
                            TotalCount = FindingDorySns.Count()
                        };

            return query;
        }
    }
}
