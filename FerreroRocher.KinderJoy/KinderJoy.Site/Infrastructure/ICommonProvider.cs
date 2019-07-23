using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KinderJoy.Site.Infrastructure {
    public interface ICommonProvider {
        string ClientIP { get; }
        DateTime Now { get; }
        bool IsMobileDevice { get; }

        string S3FileUpload(Stream stream, string directory, string fileName);

        List<Dictionary<string, object>> ExcelUpload(HttpPostedFileBase excelData, bool isFirstRowAsColumnNames);
        void ExcelDownLoad(object data, string fileName);

        string GetDisplayName<T>(T data);
    }
}
