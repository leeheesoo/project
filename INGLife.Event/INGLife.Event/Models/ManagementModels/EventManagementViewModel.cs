using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.ManagementModels {
    public class EventManagementViewModel {
        public long AffiliatesId { get; set; }
        public string AffiliatesName { get; set; }
        public string EventName { get; set; }
        public string PagePath { get; set; }

        public string Text {
            get {
                return string.Format("[{0}] {1}", AffiliatesName, EventName);
            }
        }
        public string Value {
            get {
                return this.PagePath;
            }
        }
    }
}