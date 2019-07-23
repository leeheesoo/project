using INGLife.Event.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities.MamboInstagramEntry {
    public class NilririmanboEntry{
        [Required]
        public int ChildAge { get; set; }
        [Display(Name = "이름")]
        public string Name { get; set; }
        [Display(Name = "연락처")]
        public string Phone { get; set; }
        [Display(Name = "fc코드")]
        public string FcCode { get; set; }
        [Display(Name = "인스타그램 계정")]
        public string Instagram { get; set; }
    }
}
