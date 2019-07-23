using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.BackToSchool2016
{
    /// <summary>
    /// 새학기 빙고퀴즈
    /// </summary>
    public class BackToSchool2016BingoQuiz
    {
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
        /// 개인정보 : 우편번호
        /// </summary>
        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }

        /// <summary>
        /// 개인정보 : 기본주소
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Address1 { get; set; }

        /// <summary>
        /// 개인정보 : 상세주소
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Address2 { get; set; }

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
        /// 
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
