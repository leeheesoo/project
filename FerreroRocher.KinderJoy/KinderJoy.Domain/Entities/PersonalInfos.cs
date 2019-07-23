using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities
{
    public class PersonalInfos
    {
        public PersonalInfos()
        {
            RegisterDate = DateTime.Now;
        }

        [Key]
        public int PersonalInfoId { get; set; }

        [Required]
        [MaxLength(128)]
        public string EventId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string Mobile { get; set; }

        [MaxLength(10)]
        public string Zipcode { get; set; }

        [MaxLength(250)]
        public string Address1 { get; set; }

        [MaxLength(250)]
        public string Address2 { get; set; }

        public int Age { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string IpAddress { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
