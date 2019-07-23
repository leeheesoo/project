using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Samsonite.Mall.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace Samsonite.Mall.Infrastructure {
    public class CommonProvider : ICommonProvider {
        private AppDbContext db;
        public CommonProvider(AppDbContext db) {
            this.db = db;
        }

        public DateTime Now {
            get {
                return db.Database.SqlQuery<DateTime>("SELECT NOW()").Single();
            }
        }

        public string IpAddress {
            get {
                return System.Web.HttpContext.Current.Request.Headers["X-Forwarded-For"] ?? System.Web.HttpContext.Current.Request.UserHostAddress;
            }
        }
        public bool IsMobileDevice
        {
            get { return HttpContext.Current.Request.Browser.IsMobileDevice; }
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
        public string S3FileUpload(Stream stream, string directory, string fileName) {
            string url = null;
            using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(WebConfigurationManager.AppSettings["aws.s3.accessKey"], WebConfigurationManager.AppSettings["aws.s3.secretAccessKey"], RegionEndpoint.APNortheast2)) {
                try {
                    PutObjectRequest request = new PutObjectRequest() {
                        BucketName = WebConfigurationManager.AppSettings["aws.s3.bucketName"],
                        //Key = string.Format("workplace/profile/{0}{1}", Guid.NewGuid().ToString(), extension),
                        Key = string.Format(directory + "/{0}", fileName),
                        InputStream = stream
                    };
                    //titledRequest.Metadata.Add("title", "123");
                    PutObjectResponse response = client.PutObject(request);

                    url = string.Format("https://{0}.s3.amazonaws.com/{1}", request.BucketName, request.Key);

                } catch (AmazonS3Exception amazonS3Exception) {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))) {
                    } else {

                    }
                    throw;
                }
            }
            return url;
        }
    }
}