using Finnq.Promotion.Domain.Entities.TmapEvent;
using Finnq.Promotion.Domain.Exceptions;
using Finnq.Promotion.Domain.Repositories.TmapEvent;
using LinqKit;
using System.Linq;

namespace Finnq.Promotion.Domain.Services.TmapEvent {
    public class TmapEventEntryService : ITmapEventEntryService {
        private ITmapEventEntryRepository repository;
        public TmapEventEntryService(ITmapEventEntryRepository repository) {
            this.repository = repository;
        }

        public TmapEventEntry CreateTmapEventEntry(TmapEventEntry entry) {
            if(entry == null) {
                throw new TmapEventEntryServiceException("400", "이벤트 참여 중 에러가 발생했습니다. 관리자에게 문의해주시길 바랍니다.", entry);
            }
            repository.Add(entry);
            repository.Save();
            return entry;
        }

        public IQueryable<TmapEventEntry> FilterTmapEventEntry(TmapEventEntryQueryOptions options) {
            return repository.GetAll().ToList().AsQueryable().AsExpandable().Where(options.BuildPredicate()).OrderByDescending(x=>x.CreateDate);
        }
    }
    public class TmapEventEntryServiceException: EventServiceException {
        public TmapEventEntryServiceException(string code, string message, object data)
            :base(code, message, data) {

        }

    }
}
