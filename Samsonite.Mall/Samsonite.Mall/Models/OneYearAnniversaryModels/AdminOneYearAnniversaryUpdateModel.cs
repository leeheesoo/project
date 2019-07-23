using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.OneYearAnniversaryModels {
    public class AdminOneYearAnniversaryUpdateModel {
        public long Id { get; set; }

        public string AcrosticPoemFirst { get; set; }

        public string AcrosticPoemSecond { get; set; }

        public string AcrosticPoemThird { get; set; }

        public string AcrosticPoemFourth { get; set; }

        public string AcrosticPoemFifth { get; set; }

        public string CongratulationMessage { get; set; }

        public string Name { get; set; }

        public string SamsoniteId { get; set; }

        public string Mobile { get; set; }

        public bool IsShow { get; set; }
    }
}