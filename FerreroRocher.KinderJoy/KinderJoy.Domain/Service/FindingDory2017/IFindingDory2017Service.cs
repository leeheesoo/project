using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Models.FindingDory2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service.FindingDory2017
{
    public interface IFindingDory2017Service
    {
        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        FindingDory2017User CreateUser(FindingDory2017User entry);
        FindingDory2017SNS CreateSNSShare(FindingDory2017SNS entry);
        IQueryable<FindingDory2017User> GetUser(AdminFindingDory2017QueryOptions options);
        IQueryable<FindingDory2017SNS> GetSns(AdminFindingDory2017SNSQueryOptions options);
        IQueryable<AdminFindingDory2017StatsViewModel> GetStatistics();
    }
}
