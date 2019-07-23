using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;

namespace Samsonite.Mall.Infrastructure {
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