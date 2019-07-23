using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INGLife.Event.Models.KMCModels;
using System.Configuration;
using System.Text.RegularExpressions;
using INGLife.Event.Domain.Exceptions;

namespace INGLife.Event.Infrastructures.KMCServices {
    public class KMCService : IKMCService {
        /// <summary>
        /// KMC 본인인증서비스 Request
        /// </summary>
        /// <param name="date">날짜</param>
        /// <param name="urlCode">url 등록하고 생성되는 urlcode</param>
        /// <param name="trUrl">callback url</param>
        /// <returns></returns>
        public RequestKMCModel RequestKMC(string date, string urlCode, string trUrl) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomStr = new String(Enumerable.Repeat(chars, 6).Select(x => x[new Random().Next(x.Length)]).ToArray());
            RequestKMCModel model = new RequestKMCModel {
                CpId = ConfigurationManager.AppSettings["kmc.cpId"] as string,
                Urlcode = urlCode,
                Date = date,
                CertNum = string.Format("{0}{1}", date, randomStr),
                Tr_Url = trUrl
            };
            return model;
        }

        public RequestKMCModel RequestPlusInfoKMC(string date, string urlCode, string trUrl, string plusInfo)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomStr = new String(Enumerable.Repeat(chars, 6).Select(x => x[new Random().Next(x.Length)]).ToArray());
            RequestKMCModel model = new RequestKMCModel
            {
                CpId = ConfigurationManager.AppSettings["kmc.cpId"] as string,
                Urlcode = urlCode,
                Date = date,
                CertNum = string.Format("{0}{1}", date, randomStr),
                Tr_Url = trUrl,
                PlusInfo = plusInfo
            };
            return model;
        }

        /// <summary>
        /// KMC 본인인증서비스 Response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultMessage ResponseKMC(ResponseKMCModel model) {

            //parameter 수신
            var iv = model.CertNum;  // certNum값을 복호화키에 세팅

            //01. 1차 복호화
            ICERTSECURITYLib.SEED dec = new ICERTSECURITYLib.SEED();
            var rec_cert = dec.IcertSeedDecript(model.Rec_Cert, iv);

            //02. 복호화 데이터 Split (rec_cert 1차암호화데이터 / 위변조 검증값 / 암복화확장변수)
            var decStr_Split = rec_cert.Split('/');
            var encPara = decStr_Split[0];  // rec_cert 1차 암호화데이터
            var encMsg = decStr_Split[1];   // 위변조검증값

            //03.위변조 검증값 생성
            ICERTSECURITYLib.AES hash = new ICERTSECURITYLib.AES();
            var hashStr = hash.IcertHMacEncript(encPara);
            var msgChk = "";
            //04. 위변조 검증
            if (hashStr == encMsg) {
                msgChk = "T";
            }
            var result = new ResultKMCModel();
            if (msgChk == "T") { // 위변조가 정상일때 2차 복호화
                rec_cert = dec.IcertSeedDecript(encPara, iv);
                decStr_Split = rec_cert.Split('/');
                result = new ResultKMCModel {
                    CertNum = decStr_Split[0],
                    Date = decStr_Split[1],
                    CI = decStr_Split[2],
                    PhoneNo = decStr_Split[3],
                    PhoneCorp = decStr_Split[4],
                    BirthDay = decStr_Split[5],
                    Gender = decStr_Split[6],
                    Nation = decStr_Split[7],
                    Name = decStr_Split[8],
                    Result = decStr_Split[9],
                    CertMet = decStr_Split[10],
                    Ip = decStr_Split[11],
                    M_Name = decStr_Split[12],
                    M_BirthDay = decStr_Split[13],
                    M_Gender = decStr_Split[14],
                    M_Nation = decStr_Split[15],
                    PlusInfo = decStr_Split[16],
                    DI = decStr_Split[17]
                };
            }

            // 파라미터 형식 검증 -------------------------------------------------------
            if (result.CertNum.Length == 0 || result.CertNum.Length > 40) {
                throw new KMCServiceException("400", "요청번호 비정상", result.CertNum);
            }
            if (result.Date.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.Date)) {
                throw new KMCServiceException("400", "요청일시 비정상", result.Date);
            }

            if (result.CertMet.Length == 0 || !new Regex("^[A-Z]*$").IsMatch(result.CertMet)) {
                throw new KMCServiceException("400", "본인인증방법 비정상", result.CertMet);
            }

            if (result.Name.Length == 60 || !new Regex("^[\\s가-힣a-zA-Z-,.]*$").IsMatch(result.Name)) {
                throw new KMCServiceException("400", "성명정보 비정상", result.Name);
            }

            if (result.PhoneNo.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.PhoneNo)) {
                throw new KMCServiceException("400", "휴대폰번호 비정상", result.PhoneNo);
            }

            if (result.PhoneCorp.Length == 0 || !new Regex("^[A-Z]*$").IsMatch(result.PhoneCorp)) {
                throw new KMCServiceException("400", "이동통신사 비정상", result.PhoneCorp);
            }

            if (result.BirthDay.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.BirthDay)) {
                throw new KMCServiceException("400", "생년월일 비정상", result.BirthDay);
            }

            if (result.Gender.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.Gender)) {
                throw new KMCServiceException("400", "성별 비정상", result.Gender);
            }

            if (result.Nation.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.Nation)) {
                throw new KMCServiceException("400", "내외국인 비정상", result.Nation);
            }

            if (result.Result.Length == 0 || !new Regex("^[A-Z]*$").IsMatch(result.Result)) {
                throw new KMCServiceException("400", "결과 비정상", result.Result);
            }

            if (!string.IsNullOrEmpty(result.M_Name) && (result.M_Name.Length == 60 || !new Regex("^[\\s가-힣a-zA-Z-,.]*$").IsMatch(result.M_Name))) {
                throw new KMCServiceException("400", "미성년자 성명정보 비정상", result.M_Name);
            }

            if (!string.IsNullOrEmpty(result.M_BirthDay) && (result.M_BirthDay.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.M_BirthDay))) {
                throw new KMCServiceException("400", "미성년자 생년월일 비정상", result.M_BirthDay);
            }

            if (!string.IsNullOrEmpty(result.M_Gender) && (result.M_Gender.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.M_Gender))) {
                throw new KMCServiceException("400", "미성년자 성별 비정상", result.M_Gender);
            }

            if (!string.IsNullOrEmpty(result.M_Nation) && (result.M_Nation.Length == 0 || !new Regex("^[0-9]*$").IsMatch(result.M_Nation))) {
                throw new KMCServiceException("400", "미성년자 내외국인 비정상", result.M_Nation);
            }
            // End - 파라미터 형식 검증 -------------------------------------------------------

            //05. CI, DI 복호화
            if (string.IsNullOrEmpty(result.CI) || string.IsNullOrEmpty(result.DI)) {
                throw new KMCServiceException("400", "복호화 실패", result);
            }
            result.DescriptCI = dec.IcertSeedDecript(result.CI, iv);
            result.DescriptDI = dec.IcertSeedDecript(result.DI, iv);
            return new ResultMessage {
                Result = true,
                Message = "본인인증 완료되었습니다.",
                Data = result
            };
        }
    }
    public class KMCServiceException : EventServiceException {
        public KMCServiceException(string code, string message, object data) : base(code, message, data) {
        }
    }
}