using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Infrastructures.Interfaces {
    public interface ICommonProvider {
        string IpAddress { get; }
        DateTime Now { get; }
        bool IsMobileDevice { get; }
        void ExcelDownLoad(object data, string fileName);
    }
}
