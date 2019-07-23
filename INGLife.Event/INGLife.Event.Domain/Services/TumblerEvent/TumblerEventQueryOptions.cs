using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Rebranding {
    public class TumblerEventQueryOptions : PageQueryOptions{
        public string eventType { get; set; }
    }
}
