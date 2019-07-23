using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Models.FinancialConsultantSharingModels.New
{
    public class FCNewAuthModel
    {
        [Display(Name = "기존고객 난수번호")]
        public string rid { get; set; }
        [Display(Name = "재무상담사 코드")]        
        public string fc { get; set; }             
    }
}