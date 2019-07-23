using ExpressiveAnnotations.Attributes;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Models.FinancialConsultantSharingModels.Origin
{
    public class FcKakaoShareModel
    {
        [Display(Name = "기존고객 난수번호")]
        public string RandomNum { get; set; }
        [Display(Name = "재무상담사 코드")]        
        public string FcCode { get; set; }             
    }
}