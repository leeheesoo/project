using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.ManagementModels {
    public class AffiliatesViewModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Text {
            get {
                return this.Name;
            }
        }
        public string Value {
            get {
                return this.Id.ToString();
            }
        }
    }
}