using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Services.Rebranding {
    public class RebrandingQueryOptions :PageQueryOptions{
        public string month { get; set; }
    }
}
