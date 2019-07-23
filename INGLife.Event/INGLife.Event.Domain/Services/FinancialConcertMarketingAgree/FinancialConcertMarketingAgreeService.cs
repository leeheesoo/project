using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Repositories.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Exceptions;

namespace INGLife.Event.Domain.Services.FinancialConcertMarketingAgree {
    public class FinancialConcertMarketingAgreeService : IFinancialConcertMarketingAgreeService {

        #region variables
        private IFinancialConcertMarketingAgreeRepository repository;
        #endregion

        #region constructor
        public FinancialConcertMarketingAgreeService(IFinancialConcertMarketingAgreeRepository repository) {
            this.repository = repository;
        }
        #endregion

        #region create
        public long Create (FinancialConcertMarketingAgreeEntry entities) {
            long entryId = -1;
            var isDuplicate = repository.SingleOrDefault(x => x.Mobile.Equals(entities.Mobile));
            if (isDuplicate != null && isDuplicate.CompleteDate.HasValue) {    //응모완료일이 있다면 중복참여자
                throw new EventServiceException("400", "이미 참여하였습니다.", entities);
            } else if(isDuplicate != null) {    //(응모완료X) 본인인증만 하고 이탈한 사용자
                isDuplicate.CreateDate = entities.CreateDate;
                isDuplicate.IpAddress = entities.IpAddress;
                isDuplicate.Channel = entities.Channel;
                var result = repository.Update(isDuplicate);
                repository.Save();
                entryId = result.Id;
            } else {
                var result = repository.Add(entities);
                repository.Save();
                entryId = result.Id;
            }
            return entryId;
        }
        #endregion

        #region read
        public FinancialConcertMarketingAgreeEntry CheckCertEntry (long entryId, string name, string mobile, string gender, string birthDay) {
            return repository.SingleOrDefault(x => x.Id.Equals(entryId) && x.Name.Equals(name) && x.Mobile.Equals(mobile) && x.Gender.Equals(gender) && x.BirthDay.Equals(birthDay));
        }
        public IList<FinancialConcertMarketingAgreeEntry> GetAll () {
            return repository.Filter(x=>x.CompleteDate.HasValue).OrderByDescending(x=> new { x.CreateDate, x.CompleteDate }).ToList();
        }
        #endregion

        #region update
        public void Update (FinancialConcertMarketingAgreeEntry updateEntities) {
            repository.Update(updateEntities);
            repository.Save();
        }
        #endregion

        #region delete
        #endregion

    }
}
