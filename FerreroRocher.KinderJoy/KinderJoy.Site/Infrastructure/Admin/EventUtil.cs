using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace KinderJoy.Site.Infrastructure.Admin
{
    public static class EventUtil
    {

        public static string FormatMobile(string mobile, string sep)
        {
            string returnMobile = "";
            string pattern = "[^0-9]";
            mobile = System.Text.RegularExpressions.Regex.Replace(mobile, pattern, "");

            pattern = "^(010|011|016|017|018|019)[0-9]{3,4}[0-9]{4}$";
            bool isMatch = System.Text.RegularExpressions.Regex.IsMatch(mobile, pattern);
            if (isMatch)
            {
                returnMobile = mobile.Substring(0, 3) + sep + mobile.Substring(3, mobile.Length - 7) + sep + mobile.Substring(mobile.Length - 4, 4);
            }

            return returnMobile;
        }

        public static void ExcelDownLoad(object data, string fileName)
        {
            if (data != null)
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = data;
                grid.DataBind();

                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
                System.Web.HttpContext.Current.Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                System.Web.HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\'text/html; charset=utf-8\'>");
                System.Web.HttpContext.Current.Response.Write(@"<style> td { mso-number-format:\@; } </style>");
                System.Web.HttpContext.Current.Response.Write(sw.ToString());
            }
        }

        public static string GetSnsUrl(string snsType, string snsId, string postId)
        {
            string url = "";
            try
            {
                if (snsType == "facebook")
                {
                    url = "http://www.facebook.com/" + postId;
                }
                else if (snsType == "twitter")
                {
                    url = "http://www.twitter.com/" + snsId + "/status/" + postId;
                }
                else if (snsType == "kakaostory")
                {
                    string[] splitPostId = postId.Split('.');
                    url = "https://story.kakao.com/" + splitPostId[0] + "/" + splitPostId[1];
                }
            }
            catch (Exception)
            {
                url = "";
            }
            return url;
        }
    }
}