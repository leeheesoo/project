using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Finnq.Promotion.Infrastructures {
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