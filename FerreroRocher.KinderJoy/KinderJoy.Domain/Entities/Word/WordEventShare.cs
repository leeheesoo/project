using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.Word
{
    public class WordEventShare
    {
        public WordEventShare()
        {
            RegisteredAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("WordEvent")]
        public int WordEventId { get; set; }
        public virtual WordEvent WordEvent { get; set; }

        [MaxLength(20)]
        public string SnsType { get; set; }

        [MaxLength(100)]
        public string SnsId { get; set; }

        [MaxLength(200)]
        public string PostId { get; set; }

        [MaxLength(16)]
        public string Ip { get; set; }

        [MaxLength(7)]
        public string Channel { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
