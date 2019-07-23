using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Abstract {
    /// <summary>
    /// 네이버 즉석당첨 템플릿
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class EventNaverSearching<TType> {
        /// <summary>
        /// 아이디
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 경품유형
        /// </summary>
        [Required]
        public TType PrizeType { get; set; }

        /// <summary>
        /// 채널
        /// </summary>
        [Required]
        public string Channel { get; set; }

        /// <summary>
        /// 아이피주소
        /// </summary>
        [Required]
        public string IpAddress { get; set; }

        /// <summary>
        /// 참여일
        /// </summary>
        [Required]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 개인정보 : 이름
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 개인정보 : 모바일
        /// </summary>
        [MaxLength(25)]
        public string Mobile { get; set; }

        /// <summary>
        /// 개인정보 : 우편번호
        /// </summary>
        [MaxLength(7)]
        public string Zipcode { get; set; }

        /// <summary>
        /// 개인정보 : 기본주소
        /// </summary>
        [MaxLength(250)]
        public string Address1 { get; set; }

        /// <summary>
        /// 개인정보 : 상세주소
        /// </summary>
        [MaxLength(250)]
        public string Address2 { get; set; }
    }
}
