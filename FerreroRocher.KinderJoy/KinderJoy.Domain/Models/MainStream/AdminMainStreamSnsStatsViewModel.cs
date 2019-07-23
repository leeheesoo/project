using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.MainStream {

    public class AdminMainStreamSnsStatsViewModel {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public int TotalCount { get; set; }
        public int FacebookCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaotalkCount { get; set; }
        
        public string Gender { get; set; }
    }
}
