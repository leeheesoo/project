using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.MainStream {
    public class MainStreamSnsModel {

        /// <summary>
        /// 응모번호
        /// </summary>
        public long MainStreamSurpriseId { get; set; }
        /// <summary>
        /// SNS유형
        /// </summary>
        public string SnsType { get; set; }
        /// <summary>
        /// SNS공유 아이디
        /// </summary>
        public string SnsId { get; set; }
        /// <summary>
        /// SNS공유 이름
        /// </summary>
        public string SnsName { get; set; }
        /// <summary>
        /// SNS공유 닉네임
        /// </summary>
        public string SnsNickName { get; set; }
        /// <summary>
        /// SNS공유 스크랩Url
        /// </summary>
        public string ScrapUrl { get; set; }
    }
}