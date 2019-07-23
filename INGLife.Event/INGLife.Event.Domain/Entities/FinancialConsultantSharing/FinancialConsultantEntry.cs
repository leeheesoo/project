using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.FinancialConsultantSharing
{
    public class FinancialConsultantEntry
    {
        [Key]
        [Required]
        [Display(Name = "FC 고유번호")]
        public string FcCode { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "이름")]
        public string Name { get; set; }
    }
}
