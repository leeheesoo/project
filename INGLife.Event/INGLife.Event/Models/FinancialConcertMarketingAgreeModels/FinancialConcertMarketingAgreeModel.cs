using INGLife.Event.Models.KMCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.FinancialConcertMarketingAgreeModels {
    public class FinancialConcertMarketingAgreeModel {
        public FinancialConcertMarketingAgreeCreateModel FinancialConcertMarketingAgreeCreateModel { get; set; }
        public FinancialConcertMarketingAgreeCertModel FinancialConcertMarketingAgreeCertModel { get; set; }
        public RequestKMCModel FinancialConcertMarketingAgreeKMCModel { get; set; }
    }
}