using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.ChildrensDay {
    public class ChildrensDayHiddenPicture {

        /// <summary>
        /// 아이디
        /// </summary>
        [Key]
        public long Id { get; set; }
        
        /// <summary>
        /// 개인정보 : 이름
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 개인정보 : 나이
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// 개인정보 : 모바일
        /// </summary>
        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }

        /// <summary>
        /// 장난감 성별
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Gender { get; set; }

        /// <summary>
        /// 채널
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }

        /// <summary>
        /// 아이피주소
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 참여일
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
