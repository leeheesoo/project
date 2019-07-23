using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INGLife.Event.Domain.Entities {
    public class EventManagement {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 제휴사
        /// </summary>
        [Required]
        [ForeignKey("Affiliates")]
        public long AffiliatesId { get; set; }
        public virtual Affiliates Affiliates { get; set; }
        /// <summary>
        /// 이벤트명
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string EventName { get; set; }
        /// <summary>
        /// 페이지 Path
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string PagePath { get; set; }
        /// <summary>
        /// 등록일
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
