using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.MainStream;
using KinderJoy.Domain.Infrastructure;

namespace KinderJoy.Domain.Repository.MainStream {
    public class MainStreamRepository: IMainStreamRepository {

        private AppDbContext db;

        public MainStreamRepository(AppDbContext db) {
            this.db = db;
        }

        public IQueryable<MainStreamSurprise> MainStreamSurprises {
            get {
                return db.MainStreamSurprise;
            }
        }

        public IQueryable<MainStreamSurpriseSNS> MainStreamSurpriseSNSs {
            get {
                return db.MainStreamSurpriseSNS;
            }
        }

        public long CreateEntry(MainStreamSurprise entry) {
            entry = db.MainStreamSurprise.Add(entry);
            db.SaveChanges();
            return entry.Id;
        }

        public long CreateEntrySns(MainStreamSurpriseSNS sns) {
            db.MainStreamSurpriseSNS.Add(sns);
            db.SaveChanges();
            return sns.Id;
        }
    }
}
