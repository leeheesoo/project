using Samsonite.Mall.Domain.Entities.Christmas2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samsonite.Mall.Domain.Repositories.Christmas2017;

namespace Samsonite.Mall.Domain.Services.Christmas2017 {
    public class Christmas2017Service : IChristmas2017Service {
        private IChristmas2017EntryRepository repo;

        public Christmas2017Service(IChristmas2017EntryRepository repo) {
            this.repo = repo;
        }

        public int GetChristmasPlumeBasicCount {
            get {
                return repo.GetAll().Count(x => x.ChristmasBagType == ChristmasBagType.PlumeBasic);
            }
        }

        public int GetChristmasSupremeLiteCount {
            get {
                return repo.GetAll().Count(x => x.ChristmasBagType == ChristmasBagType.SupremeLite);
            }
        }

        public int GetChristmasTielonnCount {
            get {
                return repo.GetAll().Count(x => x.ChristmasBagType == ChristmasBagType.Tielonn);
            }
        }
        public int GetChristmasMarlonCount {
            get {
                return repo.GetAll().Count(x => x.ChristmasBagType == ChristmasBagType.Marlon);
            }
        }
        
        public Christmas2017Entry CreateChristmas2017Entry(Christmas2017Entry entry) {
            var result = repo.Add(entry);
            repo.Save();
            return result;
        }

        public IQueryable<Christmas2017Entry> GetAdminChristmasEntryList() {
            return repo.GetAll().OrderByDescending(x => x.CreateDate);
        }
    }
}
