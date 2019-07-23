using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsonite.Mall.Infrastructure {
    public interface ICommonProvider {
        string IpAddress { get; }
        DateTime Now { get; }
        void ExcelDownLoad(object data, string fileName);
        string S3FileUpload(Stream stream, string directory, string fileName);
        bool IsMobileDevice { get; }
    }
}
