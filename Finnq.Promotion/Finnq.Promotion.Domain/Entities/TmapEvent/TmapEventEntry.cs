using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Entities.TmapEvent {
    /// <summary>
    /// T map 이벤트
    /// </summary>
    public class TmapEventEntry {
        /// <summary>
        /// 아이디
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 휴대폰
        /// </summary>
        [Required]
        [MaxLength(13)]
        public string Phone { get; set; }
        /// <summary>
        /// 응모일자
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 아이피주소
        /// </summary>
        [Required]
        public string IpAddress { get; set; }
        /// <summary>
        /// 모바일유입여부
        /// </summary>
        [Required]
        public bool IsMobileDevice { get; set; }
    }
}
