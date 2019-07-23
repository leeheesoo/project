using Samsonite.Mall.Domain.Entities.Christmas2017;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models.Christmas2017Models {
    public class Christmas2017AdminModel {
        [Required(ErrorMessage ="가방을 선택해주세요.")]
        public ChristmasBagType ChristmasBagType { get; set; }
    }
}