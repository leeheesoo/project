using KinderJoy.Domain.Entities.MavelFrozen;
using KinderJoy.Domain.Models.MarvelFrozen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.MavelFrozen {
    public interface IMavelFrozenService {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MavelFrozenUser CreateUser(MavelFrozenUser entry);
        /// <summary>
        /// SNS공유 정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        MavelFrozenSNS CreateSNSShare(MavelFrozenSNS entry);

        IQueryable<MavelFrozenUser> GetUser(AdminMarvelFrozenQueryOptions options);
        IQueryable<MavelFrozenSNS> GetSns(AdminMarvelFrozenSNSQueryOptions options);
        IQueryable<AdminMarvelFrozenStatsViewModel> GetStatistics();
    }
}
