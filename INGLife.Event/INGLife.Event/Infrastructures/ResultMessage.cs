using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Infrastructures {
    public class ResultMessage {
        public bool Result { get; set; }
        public string Message { get; set; }
        public ResultKMCModel Data { get; set; }
    }
}