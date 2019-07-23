using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.Word
{
    public class WordEvent
    {
        public WordEvent()
        {
            RegisteredAt = DateTime.Now;
            St = 1;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public virtual PersonalInfos PersonalInfo { get; set; }

        [Required]
        [MaxLength(1)]
        public string GiftType { get; set; }

        [MaxLength(16)]
        public string Ip { get; set; }

        [MaxLength(7)]
        public string Channel { get; set; }

        [DefaultValue(1)]
        public int St { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
