using KinderJoy.Domain.Abstract.Christmas2015;
using KinderJoy.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Admin.Christmas2015 {
    public class WinEntry {
        public string Type { get; set; }
        [ValidFile(Required = true, MaxLength = 5242880, AllowedExt = new string[] { ".xls", ".xlsx" }, ErrorMessage = "엑셀파일만 업로드가 가능합니다!")]
        public HttpPostedFileBase Data { get; set; }
    }

    public class WinSettings {
        public DateTime Date { get; set; }
        public Christmas2015EnumConst.Christmas2015WinPrize PrizeType { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }

    public class WinListEntry {
        public Christmas2015EnumConst.Christmas2015WinPrize PrizeType { get; set; }
        public string Channel { get; set; }
        public string IpAddress { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PrizeName {
            get {
                var field = PrizeType.GetType().GetField(PrizeType.ToString());
                var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrib != null ? attrib.GetName() : PrizeType.ToString();
            }
        }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Zipcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address {
            get {
                if (!string.IsNullOrEmpty(Zipcode)) {
                    return string.Format("[{0}] {1} {2}", Zipcode, Address1, Address2);
                } else {
                    return "";
                }
            }
        }
    }
    public class WinListByIpAddress {
        public string IpAddress { get; set; }
        public int TotalCount { get; set; }
        public int LoserCount { get; set; }
        public int KinderCount { get; set; }
        public int CloakCount { get; set; }
    }
}