using KinderJoy.Domain.Abstract.Christmas2015;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinderJoy.Domain.Entities.Christmas2015 {
    /// <summary>
    /// 2015년 크리스마스 이벤트
    /// 트리만들기 이벤트
    /// </summary>
    public class Christmas2015MakeTree {

        /// <summary>
        /// 아이디
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 채널
        /// </summary>
        [Required]
        public string Channel { get; set; }

        /// <summary>
        /// 아이피주소
        /// </summary>
        [Required]
        public string IpAddress { get; set; }

        /// <summary>
        /// 참여일
        /// </summary>
        [Required]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 개인정보 : 이름
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 개인정보 : 모바일
        /// </summary>
        [MaxLength(25)]
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 개인정보 : 우편번호
        /// </summary>
        [MaxLength(7)]
        [Required]
        public string Zipcode { get; set; }

        /// <summary>
        /// 개인정보 : 기본주소
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Address1 { get; set; }

        /// <summary>
        /// 개인정보 : 상세주소
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Address2 { get; set; }
        
        /// <summary>
        /// 개인정보 : 나이(추가)
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// 장난감1
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy1 { get; set; }

        /// <summary>
        /// 장난감2
        /// </summary>
        public Christmas2015EnumConst.Christmas2015ToyType? Toy2 { get; set; }

        /// <summary>
        /// 장난감3
        /// </summary>
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

        /// <summary>
        /// 합성이미지 경로
        /// </summary>
        public string SynthesisImage { get; set; }

        /// <summary>
        /// 합성콘텐츠
        /// </summary>
        public string Content { get; set; }
    }
}
