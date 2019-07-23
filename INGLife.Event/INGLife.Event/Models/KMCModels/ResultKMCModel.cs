using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.KMCModels {
    public class ResultKMCModel {
        [Display(Name = "서비스 인증번호(복호화 KEY)", Description = "tr_cert 정보 리턴")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "요청번호가 비정상")]
        [MaxLength(40, ErrorMessage = "요청번호가 비정상")]
        [MinLength(1, ErrorMessage = "요청번호가 비정상")]
        public string CertNum { get; set; }
        [Display(Name = "서비스 요청일시", Description = "tr_cert 정보 리턴")]
        public string Date { get; set; }
        [Display(Name = "연계정보(Unique한 식별정보)")]
        public string CI { get; set; }
        [Display(Name = "휴대폰번호")]
        public string PhoneNo { get; set; }
        [Display(Name = "이동통신사", Description = "SKT: SK텔레콤, KTF: KT, LGT: LG U+, SKM: SKT mvno, KTM: KT mvno, KGM: LG U+ mvno")]
        public string PhoneCorp { get; set; }
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "성별", Description = "0: 남자, 1: 여자")]
        public string Gender { get; set; }
        [Display(Name = "내.외국인 정보", Description = "0: 내국인, 1: 외국인")]
        public string Nation { get; set; }
        [Display(Name = "성명")]
        public string Name { get; set; }
        [Display(Name = "본인인증 결과값", Description = "Y: 성공, N: 실패, F: 오류")]
        public string Result { get; set; }
        [Display(Name = "본인확인방법", Description = "M: 휴대폰 본인확인, P: 공인인증서")]
        public string CertMet { get; set; }
        [Display(Name = "이용자IP주소정보", Description = "")]
        public string Ip { get; set; }
        [Display(Name = "14세 미만 성명", Description = "")]
        public string M_Name { get; set; }
        [Display(Name = "14세 미만 생년월일", Description = "")]
        public string M_BirthDay { get; set; }
        [Display(Name = "14세 미만 성별", Description = "")]
        public string M_Gender { get; set; }
        [Display(Name = "14세 미만 내.외국인", Description = "")]
        public string M_Nation { get; set; }
        [Display(Name = "추가 DATA정보", Description = "최대 320byte까지 추가 요청정보 지원 tr_cert정보 리턴. /,|,&를 제외한 특수기호를이용하여 구분")]
        public string PlusInfo { get; set; }
        [Display(Name = "중복가입확인정보", Description = "")]
        public string DI { get; set; }
        [Display(Name = "확장변수(데이터 복호화를 위한 확장필드)", Description = "")]
        public string ExtenVar { get; set; }

        public string DescriptCI { get; set; }
        public string DescriptDI { get; set; }
    }
}