using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Models.NinjaBarbie2016;

namespace KinderJoy.Domain.Service.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 - Service 인터페이스
    /// </summary>
    public interface INinjaBarbie2016Service {
        #region 이벤트페이지
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        NinjaBarbie2016User CreateUser(NinjaBarbie2016User entry);

        /// <summary>
        /// 사용자 정보 가져오기
        /// </summary>
        /// <returns></returns>
        IQueryable<NinjaBarbie2016User> GetUsers();
        
        /// <summary>
        /// 서프라이즈 꾸미기 데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        NinjaBarbie2016User UpdateUser(NinjaBarbie2016User entry);

        /// <summary>
        /// 공유데이터 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        NinjaBarbie2016Sharing CreateSharing(NinjaBarbie2016Sharing entry);
        #endregion

        #region 관리자
        IQueryable<NinjaBarbie2016User> GetUsers(AdminNinjaBarbie2016UserQueryOptions options);
        IQueryable<NinjaBarbie2016Sharing> GetSharings(AdminNinjaBarbie2016SharingQueryOptions options);
        IList<AdminNinjaBarbie2016StatisticsViewModel> GetStatistics();
        #endregion
    }
}