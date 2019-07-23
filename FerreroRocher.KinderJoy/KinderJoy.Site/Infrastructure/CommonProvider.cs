using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Excel;
using KinderJoy.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace KinderJoy.Site.Infrastructure {
    public class CommonProvider : ICommonProvider {
        private AppDbContext db;
        public CommonProvider() {
            this.db = AppDbContext.Create();
        }
        public string ClientIP {
            get { return System.Web.HttpContext.Current.Request.Headers["X-Forwarded-For"] ?? System.Web.HttpContext.Current.Request.UserHostAddress; }
        }
        public bool IsMobileDevice {
            get { return HttpContext.Current.Request.Browser.IsMobileDevice; }
        }

        public DateTime Now {
            get { return db.Database.SqlQuery<DateTime>("SELECT NOW()").Single(); }
        }


        /// <summary>
        /// aws s3 파일업로드
        /// </summary>
        /// <param name="stream">업로드할 파일 stream</param>
        /// <param name="directory">디렉터리명</param>
        /// <param name="fileName">파일명</param>
        /// <returns></returns>
        public string S3FileUpload(System.IO.Stream stream, string directory, string fileName) {
            string saveUrl = "";
            using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(WebConfigurationManager.AppSettings["aws.s3.accessKey"], WebConfigurationManager.AppSettings["aws.s3.secretAccessKey"], RegionEndpoint.APNortheast1)) {
                try {
                    var extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string dbFileName = Guid.NewGuid().ToString();
                    // put a more complex object with some metadata and http headers.
                    PutObjectRequest request = new PutObjectRequest() {
                        BucketName = WebConfigurationManager.AppSettings["aws.s3.bucketName"],
                        //Key = string.Format("workplace/profile/{0}{1}", Guid.NewGuid().ToString(), extension),
                        Key = string.Format("{0}/{1}{2}", directory, dbFileName, extension),
                        InputStream = stream
                    };
                    //titledRequest.Metadata.Add("title", "123");
                    PutObjectResponse response = client.PutObject(request);

                    saveUrl = string.Format("https://{0}.s3.amazonaws.com/{1}", request.BucketName, request.Key);

                } catch (AmazonS3Exception amazonS3Exception) {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))) {
                        //.WriteLine("Please check the provided AWS Credentials.");
                        //Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    } else {
                        //Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                    }
                    //throw new AmazonS3Exception(amazonS3Exception);
                    throw;
                }
            }
            return saveUrl;

        }


        /// <summary>
        /// 엑셀업로드
        /// 키-밸류형태의 리스트에 담아서 출력
        /// </summary>
        /// <param name="excelData">업로드한 엑셀데이터</param>
        /// <param name="isFirstRowAsColumnNames">첫행을 컬럼으로 사용할지 여부</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> ExcelUpload(HttpPostedFileBase excelData, bool isFirstRowAsColumnNames) {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            IExcelDataReader excelReader = null;
            var excelExtension = new FileInfo(excelData.FileName).Extension;
            //엑셀reader
            if (excelExtension.Equals(".xls")) { //확장자가 xls
                excelReader = ExcelReaderFactory.CreateBinaryReader(excelData.InputStream);
            } else { //확장자가 xlsx
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(excelData.InputStream);
            }
            //DataSet - Create Column names from frist row
            excelReader.IsFirstRowAsColumnNames = isFirstRowAsColumnNames;
            DataSet readData = excelReader.AsDataSet();
            foreach (DataTable dt in readData.Tables) {
                foreach (DataRow data in dt.Rows) {
                    Dictionary<string, object> saveData = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns) {
                        saveData.Add(dc.ColumnName, data[dc.ColumnName]);
                    }
                    result.Add(saveData);
                }
            }
            excelReader.Close();
            return result;
        }
        
        public void ExcelDownLoad(object data, string fileName) {
            if (data != null) {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = data;
                grid.DataBind();
                string encodeFileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).Replace("+", "%20").Replace("%5b", "[").Replace("%5d", "]");
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + encodeFileName + ".xls");
                System.Web.HttpContext.Current.Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                System.Web.HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\'text/html; charset=utf-8\'>");
                System.Web.HttpContext.Current.Response.Write(@"<style> td { mso-number-format:\@; } </style>");
                System.Web.HttpContext.Current.Response.Write(sw.ToString());
                htw.Close();
                sw.Close();
            }
        }

        public string GetDisplayName<T>(T data) {
            var field = data.GetType().GetField(data.ToString());
            var attrib = field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;
            return attrib == null ? "" : attrib.GetName();
        }
    }
}