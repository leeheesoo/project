using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service {
    [JsonObject]
    public class SerializablePagedList<T> : PagedList<T> {
        public SerializablePagedList(IQueryable<T> superset, int pageNumber, int pageSize)
            : base(superset, pageNumber, pageSize) {
        }

        public SerializablePagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
            : base(superset, pageNumber, pageSize) {
        }

        public IEnumerable<T> Items {
            get { return base.Subset; }
        }
    }
}
