using KinderJoy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent
{
    public class MagicKinderAppLaunching : EventSnsSharingUser
    {
        [Display(Name = "스크릿샷")]
        [Required]
        public string ScreenShot { get; set; }
        [Display(Name = "카테고리")]
        [Required]
        public MagicKinderScreenShotTypes ScreenShotType { get; set; }
        public virtual string ScreenShotTypeDisplayName {
            get {
                var field = ScreenShotType.GetType().GetField(ScreenShotType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : ScreenShotType.ToString();
            }
        }
        [Display(Name = "한줄평")]
        [Required]
        public string Comment { get; set; }
        [Display(Name = "나이")]
        [Required]
        public int Age { get; set; }
        [Display(Name = "우편번호")]
        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }
        [Display(Name = "기본주소")]
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }
        [Display(Name = "상세주소")]
        [Required]
        [MaxLength(300)]
        public string AddressDetail { get; set; }
        [Display(Name = "채널")]
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }
        [Display(Name = "IP")]
        [Required]
        public string IpAddress { get; set; }
        [Display(Name = "노출여부")]
        [Required]
        public bool IsShow { get; set; }
    }

    public enum MagicKinderScreenShotTypes
    {
        [Display(Name = "밑그림 그리고 칠하기")]
        Coloring = 0,
        [Display(Name = "비디오 시청")]
        Video = 1,
        [Display(Name = "놀면서 배워요")]
        Playing = 2,
        [Display(Name = "동화")]
        Story = 3,
        [Display(Name = "우리 별 체험")]
        EarthTrip = 4,
        [Display(Name = "기타")]
        Etc = 5
    }
}
