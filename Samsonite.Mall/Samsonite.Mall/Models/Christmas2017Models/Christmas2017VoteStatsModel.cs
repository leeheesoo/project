using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.Christmas2017Models {
    public class Christmas2017VoteStatsModel {
        public int TielonnCount { get; set; }
        public int MarlonCount { get; set; }
        public int SupremeLiteCount { get; set; }
        public int PlumeBasicCount { get; set; }
        public int TotalCount {
            get { return TielonnCount + MarlonCount + SupremeLiteCount + PlumeBasicCount; }
        }
    }
}