using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.BackToSchool2016
{
    public class BackToSchool2016BingoQuizSns
    {
        public BackToSchool2016BingoQuizSns()
        {
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
        [ForeignKey("BackToSchool2016BingoQuiz")]
        public long BackToSchool2016BingoQuizId { get; set; }
        public virtual BackToSchool2016BingoQuiz BackToSchool2016BingoQuiz { get; set; }

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
