using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Abstract;

namespace KinderJoy.Domain.Entities.NinjaBarbie2016 {
    /// <summary>
    /// 닌자바비 2016 이벤트 SNS공유 참여자
    /// </summary>
    public class NinjaBarbie2016User : EventSnsSharingUser {
        
        [Display(Name = "채널")]
        [Required]
        [MaxLength(10)]
        public string Channel { get; set; }

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

        [Display(Name = "나이")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "서프라이즈 유형")]
        [Required]
        public NinjaBarbieSurprizeType SurprizeType { get; set; }
        public virtual string SurprizeTypeDisplayName {
            get {
                var field = SurprizeType.GetType().GetField(SurprizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : SurprizeType.ToString();
            }
        }

        [Display(Name = "IP")]
        [Required]
        public string IpAddress { get; set; }

        #region 꾸미기 정보
        [Display(Name = "상체")]
        public int? Top { get; set; }
        [Display(Name = "하체")]
        public int? Bottom { get; set; }
        [Display(Name = "장식품(무기)")]
        public int? Accessory { get; set; }
        [Display(Name = "페이스북 합성이미지 Url")]
        public string FacebookImage { get; set; }
        [Display(Name = "카카오톡 합성이미지 Url")]
        public string KakaotalkImage { get; set; }
        [Display(Name = "카카오스토리 합성이미지 Url")]
        public string KakaostoryImage { get; set; }
        #endregion
    }

    /// <summary>
    /// 서프라이즈 유형 닌자 or 바비
    /// </summary>
    public enum NinjaBarbieSurprizeType {
        [Display(Name = "닌자")]
        NINJA = 0,
        [Display(Name = "바비")]
        BARBIE = 1
    }
}