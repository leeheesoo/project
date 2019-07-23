using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Models.KMCModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace INGLife.Event.Models.TumblerEventModels {
    public class TumblerEventModel
    {
        public TumblerCreateModel TumblerCreateModel { get; set; }
        //public SecretBoxModel SecretBoxModel { get; set; }
        public RequestKMCModel TumblerAgreeKMCModel { get; set; }
        //public RequestKMCModel SecretBoxAgreeKMCModel { get; set; }
    }
}