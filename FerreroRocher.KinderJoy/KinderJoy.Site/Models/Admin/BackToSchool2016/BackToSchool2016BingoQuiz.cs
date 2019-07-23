using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Admin.BackToSchool2016
{
    public class BackToSchool2016BingoQuiz
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public string ZipCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Channel { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreateDate { get; set; }

        public string Address
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : string.Format("({0}) {1} {2}",ZipCode,Address1,Address2);
            }
        }
    }

    public class BackToSchool2016BingoQuizSNS
    {
        public string SnsType { get; set; }
        public string SnsId { get; set; }
        public string SnsName { get; set; }
        public string SnsNickName { get; set; }
        public string ScrapUrl { get; set; }
        public DateTime RegisterDate { get; set; }
        public string IpAddress { get; set; }
        public string Channel { get; set; }

        public string Name { get; set; }
        public string Mobile { get; set; }
        public string ZipCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Age { get; set; }
        public string Address {
            get {
                return string.Format("[{0}] {1} {2}", ZipCode, Address1, Address2);
            }
        }
    }

    public class BackToSchool2016BingoQuizSNSStats
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int TotalCount { get; set; }
        public int FacebookCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaotalkCount { get; set; }
    }
}