using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INGLife.Event.Models.KMCModels {
    /// <summary>
    /// KMC 서비스 호출 Request Model
    /// </summary>
    public class RequestKMCModel {
        public RequestKMCModel() {
            this.CertMet = "M";
            this.Tr_Add = "N";
            this.Name = "";
            this.BirthDay = "";
            this.PhoneNo = "";
            this.PhoneCorp = "";
            this.Gender = "";
            this.Nation = "";
            this.PlusInfo = "";
        }
        [Display(Name = "회원사ID")]
        public string CpId { get; set; }
        [Display(Name = "URL 코드")]
        public string Urlcode { get; set; }
        [Display(Name = "요청일시")]
        public string Date { get; set; }
        [Display(Name = "요청번호")]
        public string CertNum { get; set; }
        [Display(Name = "본인인증방법")]
        public string CertMet { get; set; }
        [Display(Name = "성명")]
        public string Name { get; set; }
        [Display(Name = "휴대폰번호")]
        public string PhoneNo { get; set; }
        [Display(Name = "이통사")]
        public string PhoneCorp { get; set; }
        [Display(Name = "생년월일")]
        public string BirthDay { get; set; }
        [Display(Name = "성별")]
        public string Gender { get; set; }
        [Display(Name = "내외국인 구분")]
        public string Nation { get; set; }
        [Display(Name = "추가DATA정보")]
        public string PlusInfo { get; set; }
        [Display(Name = "본인인증 결과수신 POPUP URL")]
        public string Tr_Url { get; set; }
        [Display(Name = "Iframe 사용여부")]
        public string Tr_Add { get; set; }
        public string ExtendVar {
            get {
                return "0000000000000000";
            }
        }

        public string Tr_Cert {
            get {
                return string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}/{10}/{11}/{12}",
                    this.CpId,
                    this.Urlcode,
                    this.CertNum,
                    this.Date,
                    this.CertMet,
                    this.BirthDay,
                    this.Gender,
                    this.Name,
                    this.PhoneNo,
                    this.PhoneCorp,
                    this.Nation,
                    this.PlusInfo,
                    this.ExtendVar);
            }
        }

        public string Enc_Tr_Cert {
            get {

                //02. tr_cert 1차 암호화
                ICERTSECURITYLib.SEED enc = new ICERTSECURITYLib.SEED();
                string enc_tr_cert = enc.IcertSeedEncript(this.Tr_Cert, "");

                //03. tr_cert 1차 암호화한 데이터의 위변조검증값 생성
                ICERTSECURITYLib.AES hash = new ICERTSECURITYLib.AES();
                string enc_tr_cert_hash = hash.IcertHMacEncript(enc_tr_cert);

                //04. tr_cert 2차 암호화
                string plainStr = enc_tr_cert + "/" + enc_tr_cert_hash + "/" + this.ExtendVar; // "0000000000000000";
                enc_tr_cert = enc.IcertSeedEncript(plainStr, "");

                return enc_tr_cert;
            }
        }
    }
}