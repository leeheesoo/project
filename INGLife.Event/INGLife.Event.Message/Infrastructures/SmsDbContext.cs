using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Message.Infrastructures {
    public class SmsDbContext : DbContext {
        public SmsDbContext() 
            : base("INGLife.Event.Message") {
        }
    }
}
