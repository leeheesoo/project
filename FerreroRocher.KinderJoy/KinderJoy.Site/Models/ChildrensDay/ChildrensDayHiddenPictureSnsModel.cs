using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.ChildrensDay {
    public class ChildrensDayHiddenPictureSnsModel {
        public long ChildrensDayHiddenPictureId { get; set; }

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

    public class ChildrensDayHiddenPictureSnsShareModel {
        public string SNSType { get; set; }
        public long HiddenPictureId { get; set; }
    }
}