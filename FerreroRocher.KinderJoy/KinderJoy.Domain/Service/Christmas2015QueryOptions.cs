using KinderJoy.Domain.Abstract.Christmas2015;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service {
    public class Christmas2015QueryOptions : PageQueryOptions {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Channel { get; set; }
    }
    public class Christmas2015QueryOptionsByWin : Christmas2015QueryOptions {
        public Christmas2015EnumConst.Christmas2015WinPrize? PrizeType { get; set; }
        public string IpAddress { get; set; }
    }
    public class Christmas2015QueryOptionsByMakeTreeSns : Christmas2015QueryOptions {
        public string SnsType { get; set; }
    }
}
