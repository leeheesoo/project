using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinderJoy.Domain.Entities.Christmas2015 {
    /// <summary>
    /// 2015년 크리스마스 이벤트
    /// 트리만들기이벤트 SNS공유
    /// </summary>
    public class Christmas2015MakeTreeSNSShare {
        public Christmas2015MakeTreeSNSShare() {
            RegisterDate = DateTime.Now;
        }
        /// <summary>
        /// 아이디
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 트리만들기테이블 아이디
        /// </summary>
        [Required]
        [ForeignKey("Christmas2015MakeTree")]
        public long Christmas2015MakeTreeId { get; set; }
        public virtual Christmas2015MakeTree Christmas2015MakeTree { get; set; }
        
        /// <summary>
        /// SNS유형
        /// </summary>
        [MaxLength(20)]
        public string SnsType { get; set; }

        /// <summary>
        /// SNS공유 아이디
        /// </summary>
        [MaxLength(100)]
        public string SnsId { get; set; }

        /// <summary>
        /// SNS공유 이름
        /// </summary>
        [MaxLength(50)]
        public string SnsName { get; set; }

        /// <summary>
        /// SNS공유 닉네임
        /// </summary>
        [MaxLength(50)]
        public string SnsNickName { get; set; }

        /// <summary>
        /// SNS공유 스크랩Url
        /// </summary>
        [MaxLength(200)]
        public string ScrapUrl { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }
    }
}
