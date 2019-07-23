using Finnq.Promotion.Domain.Infrastructures;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Finnq.Promotion.Infrastructures {
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
    }
}