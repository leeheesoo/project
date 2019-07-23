using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Web.Helpers;
using System.Net;
using Newtonsoft.Json;
using NLog;
using System.Security.Cryptography;

namespace INGLife.Event.Message.Services {
    public class UplusSmsApiService : IUplusSmsApiService {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public bool SendMMS(string subject, string phone, string callback, string msg,string msgType) {
            try {

                string key = ConfigurationSettings.AppSettings["lguplus-sms-openapi-key"] as string;
                string secret = ConfigurationSettings.AppSettings["lguplus-sms-openapi-secret"] as string;
                string algorithm = "0";
                string timestamp = ( DateTime.Now.ToFileTimeUtc() - new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc() ).ToString();
                string cret_txt = Salt();
                string hash_hmac = Signature(key, secret, algorithm, timestamp, cret_txt);
                string sendMmsUrl = "https://openapi.sms.uplus.co.kr:4443/v1/send";
                                
                using( var httpClient = new HttpClient() ) {
                    
                    //headers
                    httpClient.DefaultRequestHeaders.Add("hash_hmac", hash_hmac);
                    httpClient.DefaultRequestHeaders.Add("api_key", key);
                    httpClient.DefaultRequestHeaders.Add("timestamp", timestamp.ToString());
                    httpClient.DefaultRequestHeaders.Add("cret_txt", cret_txt);
                    httpClient.DefaultRequestHeaders.Add("algorithm", algorithm);
                    
                    //contents
                    string contents = JsonConvert.SerializeObject(new {
                        send_type = "S",    //S: 즉시발송, R: 예약발송
                        to = phone,
                        from = callback,
                        msg_type = msgType, //S: SMS, L: LMS, M: MMS
                        msg = msg
                    });
                    StringContent sc = new StringContent(contents, Encoding.UTF8, "application/json");
                    
                    HttpResponseMessage response = httpClient.PostAsync(sendMmsUrl, sc).Result;
                    
                    return response.IsSuccessStatusCode;
                }
            } catch(Exception e) {
                logger.Error(e);
                return false;
            }
        }

        private string Signature(string key, string secret, string algorithm, string timestamp, string cret_txt) {
            string sb = key + timestamp + cret_txt + secret;

            StringBuilder stringBuilder = new StringBuilder();

            using( SHA256 hash = SHA256Managed.Create() ) {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(sb));

                foreach( Byte b in result )
                    stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
            //return Crypto.SHA256(sb);            
        }

        private string Salt() {
            Random random = new Random();
            string uniqId = "";
            for( var length = 1; length <= 10; length++ ) {
                var randomInt = Math.Floor((double)random.Next(10));
                uniqId = uniqId + randomInt;
            }
            return uniqId;
        }
    }
}
