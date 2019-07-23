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
    public class PhoneModel
    {       
        [Display(Name = "재무상담사 코드")]
        [Required(ErrorMessage = "FC CODE를 입력해주세요")]
        public string phone { get; set; }             
    }
}