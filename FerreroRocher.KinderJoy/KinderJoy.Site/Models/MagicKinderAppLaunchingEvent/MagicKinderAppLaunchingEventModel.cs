﻿using ExpressiveAnnotations.Attributes;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.MagicKinderAppLaunchingEvent
{
    public class MagicKinderAppLaunchingEventModel
    {
        [Display(Name = "스크릿샷")]
        public string ScreenShot { get; set; }

        [Required(ErrorMessage = "스크린샷을 등록해주세요.")]
        [ValidFile(Required = true, MaxLength = 10485760, AllowedExt = new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = "10MB 이하의 jpg / png / jpeg 만 등록 가능합니다.")]
        public HttpPostedFileBase ScreenShotFile { get; set; }

        [Display(Name = "카테고리")]
        [Required(ErrorMessage = "카테고리를 선택해주세요.")]
        public MagicKinderScreenShotTypes ScreenShotType { get; set; }

        [Display(Name = "한줄평")]
        [Required(ErrorMessage = "한 줄 평을 작성해주세요.")]
        [StringLength(20)]
        public string Comment { get; set; }

        [Display(Name = "이름")]
        [Required(ErrorMessage = "이름을 20자 이내로 입력해주세요.")]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "나이")]
        [Required(ErrorMessage = "나이를 입력해 주세요.")]
        public int Age { get; set; }

        [Display(Name = "휴대폰 번호")]
        [Required(ErrorMessage = "휴대폰 번호를 입력해주세요.")]
        [RegularExpression("^(010|011|016|017|019)[0-9]{7,8}$", ErrorMessage = "휴대폰 번호를 정확히 입력해주세요. ex) 01012345678")]
        public string Mobile { get; set; }

        [Display(Name = "우편번호")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string ZipCode { get; set; }

        [Display(Name = "주소")]
        [Required(ErrorMessage = "주소를 입력해 주세요.")]
        public string Address { get; set; }

        [Display(Name = "상세주소")]
        [Required(ErrorMessage = "상세주소를 입력해 주세요.")]
        [StringLength(200, ErrorMessage = "상세주소를 200자 이내로 입력해 주세요.")]
        public string AddressDetail { get; set; }

        [Display(Name = "개인정보보호를 위한 이용자 동의사항")]
        [Required(ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        [AssertThat("Agree == true", ErrorMessage = "개인정보보호를 위한 이용자 동의사항에 동의해 주세요.")]
        public bool Agree { get; set; }
    }
}