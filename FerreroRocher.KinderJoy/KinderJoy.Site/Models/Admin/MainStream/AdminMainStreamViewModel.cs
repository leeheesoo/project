using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderJoy.Domain.Entities.MainStream;

namespace KinderJoy.Site.Models.Admin.MainStream {
    public class AdminMainStreamViewModel {
        /// <summary>
        /// 아이디
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 채널
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 개인정보 : 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 개인정보 : 나이
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 개인정보 : 모바일
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 아이의 성별
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 우리아이에게 필요한 서프라이즈
        /// </summary>
        public MainStreamSurpriseType Quiz { get; set; }
        public string QuizName {
            get {
                var field = Quiz.GetType().GetField(Quiz.ToString());
                var attrib = field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;
                return attrib == null ? "" : attrib.GetName();
            }
        }

        /// <summary>
        /// 아이피주소
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 참여일
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    public class AdminMainStreamSnsViewModel {
        /// <summary>
        /// 응모정보: 채널
        /// </summary>
        public string MainStreamSurpriseChannel { get; set; }

        /// <summary>
        /// 응모정보: 참여자 이름
        /// </summary>
        public string MainStreamSurpriseName { get; set; }
        /// <summary>
        /// 응모정보: 참여자 연락처
        /// </summary>
        public string MainStreamSurpriseMobile { get; set; }
        /// <summary>
        /// 응모정보: 아이피주소
        /// </summary>
        public string MainStreamSurpriseIpAddress{ get; set; }
        /// <summary>
        /// 응모정보: 참여자 나이
        /// </summary>
        public string MainStreamSurpriseAge { get; set; }

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
        
        public DateTime RegisterDate { get; set; }
    }
}