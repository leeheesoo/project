using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.GoogleApisModels {
    public class GoogleApisAnalyticsReportingResponseModel {
        public string PagePath { get; set; }
        public string Date { get; set; }
        public string PageViews { get; set; }
        public string UniquePageviews { get; set; }
    }
}