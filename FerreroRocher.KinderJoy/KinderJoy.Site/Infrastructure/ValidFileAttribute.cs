using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Infrastructure {
    public class ValidFileAttribute : ValidationAttribute {
        /// <summary>
        /// 필수여부
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 파일 용량
        /// </summary>
        public long MaxLength { get; set; }

        /// <summary>
        /// 허용 확장자(소문자로 작성)
        /// </summary>
        public string[] AllowedExt { get; set; }

        public override bool IsValid(object value) {
            var file = value as HttpPostedFileBase;

            //필수여부
            if (file == null) {
                if (!Required) {
                    return true;
                } else {
                    return false;
                }
            }

            // 확장자 체크
            string fileName = file.FileName.ToLower();
            if (!AllowedExt.Contains(fileName.Substring(fileName.LastIndexOf('.')).ToLower())) {
                return false;
            }
            // 용량 체크
            if (file.ContentLength > MaxLength) {
                return false;
            }
            return true;
        }


        public override string FormatErrorMessage(string name) {
            return base.FormatErrorMessage(name);
        }

    }
}