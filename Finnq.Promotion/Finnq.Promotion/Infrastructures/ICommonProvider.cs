using System;

namespace Finnq.Promotion.Infrastructures {
    public interface ICommonProvider {
        string IpAddress { get; }
        DateTime Now { get; }
        void ExcelDownLoad(object data, string fileName);
    }
}
