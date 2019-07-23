using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Service
{
    public class BackToSchool2016QueryOptions : PageQueryOptions
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Channel { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }
    }

    public class BackToSchool2016QueryOptionsByBingoQuizSns : BackToSchool2016QueryOptions
    {
        public string SnsType { get; set; }
    }
}
