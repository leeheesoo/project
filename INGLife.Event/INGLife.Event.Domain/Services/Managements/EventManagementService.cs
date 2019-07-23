using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Repositories.Managements;
using INGLife.Event.Domain.Exceptions;

namespace INGLife.Event.Domain.Services.Managements {
    public class EventManagementService : IEventManagementService {
        #region variables
        private IEventManagementRepository repository;
        #endregion

        #region constuctors
        public EventManagementService(IEventManagementRepository repository) {
            this.repository = repository;
        }
        #endregion
        
        /// <summary>
        /// 이벤트 추가하기
        /// </summary>
        /// <param name="entry"></param>
        public void CreateEventManagement(EventManagement entry) {
            if(entry == null) {
                throw new EventManagementServiceException("400", "이벤트 정보가 잘못되었습니다. 확인 후 등록해주세요.", entry);
            }
            var checkData = repository.Filter(x => x.PagePath == entry.PagePath || x.EventName == entry.EventName);
            if(checkData.ToList().Any()) {
                throw new EventManagementServiceException("400", "이미 등록된 이벤트명이거나 PagePath 입니다. 확인 후 등록해주세요.", entry);
            }
            repository.Add(entry);
            repository.Save();
        }
        /// <summary>
        /// 이벤트목록 가져오기
        /// </summary>
        public IList<EventManagement> EventManagements {
            get {
                return repository.GetAll().OrderByDescending(x => x.EventName).ToList();
            }
        }
    }
    public class EventManagementServiceException : EventServiceException {
        public EventManagementServiceException(string code, string message, object data) : base(code, message, data) {
        }
    }
}
