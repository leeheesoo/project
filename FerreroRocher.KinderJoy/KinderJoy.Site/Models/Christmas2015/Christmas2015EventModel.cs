using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Abstract.Christmas2015;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderJoy.Site.Models.Christmas2015 {
    /// <summary>
    /// 2015년도 크리스마스 이벤트
    /// </summary>
    public class Christmas2015EventModel {
        public Christmas2015WinEvent WinEvent { get; set; }
        public Christmas2015TreeEvent TreeEvent { get; set; }
    }
    public class Christmas2015WinEvent {
        /// <summary>
        /// 즉석당첨 참여 아이디
        /// </summary>
        [HiddenInput(DisplayValue=false)]
        public long WinId { get; set; }

        /// <summary>
        /// 경품유형
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public Christmas2015EnumConst.Christmas2015WinPrize PrizeType { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        [Required(ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "연락처를 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        /// <summary>
        /// 우편번호
        /// </summary>
        [Display(Name = "우편번호")]
        [RequiredIf("PrizeType == Christmas2015EnumConst.Christmas2015WinPrize.Cloak", ErrorMessage = "주소를 입력해 주세요.")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 기본주소
        /// </summary>
        [Display(Name = "주소")]
        [RequiredIf("PrizeType == Christmas2015EnumConst.Christmas2015WinPrize.Cloak", ErrorMessage = "주소를 입력해 주세요.")]
        public string Address1 { get; set; }

        /// <summary>
        /// 상세주소
        /// </summary>
        [Display(Name = "상세주소")]
        [RequiredIf("PrizeType == Christmas2015EnumConst.Christmas2015WinPrize.Cloak", ErrorMessage = "상세주소를 입력해 주세요!")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요!")]
        public string Address2 { get; set; }

        /// <summary>
        /// 개인정보수집 동의
        /// </summary>
        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
    }
    public class Christmas2015TreeEvent {
        [Required(ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "연락처를 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "연락처를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        /// <summary>
        /// 우편번호
        /// </summary>
        [Display(Name = "우편번호")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 기본주소
        /// </summary>
        [Display(Name = "주소")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string Address1 { get; set; }

        /// <summary>
        /// 상세주소
        /// </summary>
        [Display(Name = "상세주소")]
        [Required(ErrorMessage = "상세주소를 입력해 주세요!")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요!")]
        public string Address2 { get; set; }


        [Required(ErrorMessage = "나이를 입력해주세요.")]
        [RegularExpression("^[0-9]{2}", ErrorMessage = "나이를 정확히 입력해주세요.(최대2자리)")]        
        public int Age { get; set; }

        /// <summary>
        /// 개인정보수집 동의
        /// </summary>
        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }

        [HiddenInput(DisplayValue=false)]
        public int TreeId { get; set; }
        /*
        /// <summary>
        /// 합성컨텐츠
        /// </summary>
        public string SynthesisContent {get;set;}
        /// <summary>
        /// 합성이미지(카카오톡)
        /// </summary>
        public string SynthesisImageByKakaoTalk { get; set; }
        */
    }

    /// <summary>
    /// 트리이미지 생성 모델
    /// </summary>
    public class Christmas2015MakeTreeModel {
        public long MakeTreeId { get; set; }
        /// <summary>
        /// 합성이미지(기본)
        /// </summary>
        public string SynthesisImage { get; set; }

        public string Content { get; set; }
        /// <summary>
        /// 장난감1
        /// </summary>
        [Required]
        public Christmas2015EnumConst.Christmas2015ToyType Toy1 { get; set; }

        /// <summary>
        /// 장난감2
        /// </summary>
        [Required]
        public Christmas2015EnumConst.Christmas2015ToyType? Toy2 { get; set; }

        /// <summary>
        /// 장난감3
        /// </summary>
        [Required]
        public Christmas2015EnumConst.Christmas2015ToyType? Toy3 { get; set; }

        /// <summary>
        /// 장난감4
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy4 { get; set; }

        /// <summary>
        /// 장난감5
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy5 { get; set; }

        /// <summary>
        /// 장난감6
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy6 { get; set; }

        /// <summary>
        /// 장난감7
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy7 { get; set; }
    }

    public class Christmas2015MakeTreeCheckSNSModel {
        public string SnsType { get; set; }
        public string Mobile { get; set; }
    }
    public class Christmas2015MakeTreeSNSModel {

        public long Christmas2015MakeTreeId { get; set; }

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
    }
}