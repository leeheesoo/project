using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.Word
{
    public class WordWinner
    {
        [Key]
        public int Id { get; set; }
        public int St { get; set; }

        [MaxLength(1)]
        [Required]
        public string GiftType { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Mobile { get; set; }

    }
}
