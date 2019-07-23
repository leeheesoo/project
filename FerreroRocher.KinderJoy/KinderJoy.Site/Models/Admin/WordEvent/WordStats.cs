using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Admin.WordEvent
{
    public class WordStats
    {
        public string Date { get; set; }
        public int Count { get; set; }
        public int DistinctCount { get; set; }
        public int FacebookCount { get; set; }
        public int FacebookMobileCount { get; set; }
        public int TwitterCount { get; set; }
        public int TwitterMobileCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaostoryMobileCount { get; set; }
        public int KakaotalkCount { get; set; }
        public int KakaotalkMobileCount { get; set; }
    }
}