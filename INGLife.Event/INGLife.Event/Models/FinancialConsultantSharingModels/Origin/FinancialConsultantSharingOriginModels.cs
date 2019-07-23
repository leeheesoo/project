using INGLife.Event.Models.FinancialConsultantSharingModels;
using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.FinancialConsultantSharingModels.Origin
{
    public class FinancialConsultantSharingOriginModels
    {
        public FinancialConsultantSharingOriginCreateModel FinancialConsultantSharingOriginCreateModel { get; set; }        
        public RequestKMCModel RequestKMCModel { get; set; }
        public FinancialConsultantSharingKMCResultModel FinancialConsultantSharingKMCResultModel { get; set; }
    }
}