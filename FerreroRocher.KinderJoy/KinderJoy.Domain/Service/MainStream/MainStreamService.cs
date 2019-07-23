using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.MainStream;
using KinderJoy.Domain.Models.MainStream;
using KinderJoy.Domain.Repository.MainStream;

namespace KinderJoy.Domain.Service.MainStream {
    public class MainStreamService: IMainStreamService {

        private IMainStreamRepository repository;

        public MainStreamService(IMainStreamRepository repository) {
            this.repository = repository;
        }

        public IQueryable<MainStreamSurprise> MainStreamSurprises {
            get {
                return repository.MainStreamSurprises;
            }
        }

        public IQueryable<MainStreamSurpriseSNS> MainStreamSurpriseSNSs {
            get {
                return repository.MainStreamSurpriseSNSs;
            }
        }

        public long CreateEntry(MainStreamSurprise entry) {
            long entryId = repository.CreateEntry(entry);
            return entryId;
        }

        public long CreateEntrySns(MainStreamSurpriseSNS sns) {
            return repository.CreateEntrySns(sns);
        }

        public IQueryable<AdminMainStreamSnsStatsViewModel> GetMainStreamSurpriseSnsStats() {
            var sns = repository.MainStreamSurprises.AsQueryable()
               .Join(repository.MainStreamSurpriseSNSs, e => e.Id, p => p.MainStreamSurpriseId, (e, p) => new { SnsType = p.SnsType.ToLower(), Mobile = e.Mobile, Name = e.Name, Age = e.Age, Gender = e.Gender });

            return sns.GroupBy(x => x.Mobile).Select(x => new AdminMainStreamSnsStatsViewModel {
                Mobile = x.Key,
                Name = x.Max(e => e.Name),
                Age = x.Min(e=>e.Age),
                Gender = x.Count(e => e.Gender == "남아") >= x.Count(e => e.Gender == "여아") ? "남아" : "여아",
                FacebookCount = x.Count(e => e.SnsType == "facebook"),
                KakaostoryCount = x.Count(e => e.SnsType == "kakaostory"),
                KakaotalkCount = x.Count(e => e.SnsType == "kakaotalk"),
                TotalCount = x.Count()
            });
        }
    }
}
