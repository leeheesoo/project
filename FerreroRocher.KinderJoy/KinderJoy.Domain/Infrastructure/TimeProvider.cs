using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Infrastructure {
    public class TimeProvider : ITimeProvider {
        private AppDbContext db;

        public TimeProvider(AppDbContext db) {
            this.db = db;
        }

        public DateTime Now {
            get {
                return db.Database.SqlQuery<DateTime>("SELECT NOW()").Single();
            }
        }
    }
}
