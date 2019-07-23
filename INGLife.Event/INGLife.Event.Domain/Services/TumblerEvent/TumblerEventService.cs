using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Exceptions;
using INGLife.Event.Domain.Repositories.KidsNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Repositories.MarketingAgree;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Entities.TumblerEntry;

namespace INGLife.Event.Domain.Services.TumblerEvent
{
    public class TumblerEventService : ITumblerEventService
    {
        private ITumblerEventRepository repo;

        public TumblerEventService(ITumblerEventRepository repo) {
            this.repo = repo;
        }

        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>

        public TumblerEventEntry Create(TumblerEventEntry entry)
        {
            //if (IsOverFortyFiveEntryByMobile(entry.Mobile))
            //{
            //    throw new OverFortyFiveServiceException("400", "이미 참여하였습니다.", entry);
            //}
            var result = repo.Add(entry);
            repo.Save();
            return result;
        }

        public TumblerEventEntry CheckEntry(string name, string mobile, string gender, string birthDay)
        {
            return repo.SingleOrDefault(x => x.Name.Equals(name) && x.Mobile.Equals(mobile) && x.Gender.Equals(gender) && x.BirthDay.Equals(birthDay));
        }

        public int getEventTypeCount(string eventType) {
            return repo.GetAll().Where(x => x.EventType == eventType).Count();
        }

        public IQueryable<TumblerEventEntry> GetAdminTumblerEventEntryList(string eventType)
        {
            return repo.GetAll().Where(x => x.EventType == eventType).OrderByDescending(x => x.CreateDate);
        }
        ///// <summary>
        ///// 특정 응모정보 가져오기
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public OverFortyFiveDbEntry GetOverFortyFiveEntryById(long id)
        //{
        //    var result = repo.SingleOrDefault(x => x.Id == id && !x.UpdateDate.HasValue);
        //    if (result == null)
        //    {
        //        throw new OverFortyFiveServiceException("400", "다시 시도해주세요.", null);
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 본인인증 정보를 제외한 정보 update
        ///// </summary>
        ///// <param name="entry"></param>
        ///// <returns></returns>
        //public OverFortyFiveDbEntry UpdateOverFortyFiveEntry(OverFortyFiveDbEntry entry)
        //{
        //    if (IsOverFortyFiveEntryByMobile(entry.Mobile))
        //    {
        //        throw new OverFortyFiveServiceException("400", "이미 참여하였습니다.", entry);
        //    }
        //    var result = repo.Update(entry);
        //    repo.Save();
        //    return result;
        //}

        ///// <summary>
        ///// 참여이력 가져오기
        ///// </summary>
        ///// <param name="mobile"></param>
        ///// <returns></returns>
        //private bool IsOverFortyFiveEntryByMobile(string mobile) {

        //    return repo.Any(x => x.Mobile == mobile && x.UpdateDate.HasValue);
        //}

        //public int GetEmoticonTypeCount(EmoticonType emoticonType)
        //{
        //    return repo.GetAll().Where(x => x.UpdateDate.HasValue && x.EmoticonType.Value == emoticonType).Count();
        //}
    }
    public class OverFortyFiveServiceException : EventServiceException {
        public OverFortyFiveServiceException(string code, string message, object data)
            : base(code, message, data) {
        }
    }
}
