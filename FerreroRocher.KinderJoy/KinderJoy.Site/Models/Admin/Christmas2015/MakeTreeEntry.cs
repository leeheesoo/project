using KinderJoy.Domain.Abstract.Christmas2015;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Admin.Christmas2015 {
    public class MakeTreeEntry {
        public string Channel { get; set; }
        public string IpAddress { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Zipcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Age { get; set; }

        public string Address {
            get {
                if (!string.IsNullOrEmpty(Zipcode)) {
                    return string.Format("[{0}] {1} {2}", Zipcode, Address1, Address2);
                } else {
                    return "";
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy1 { get; set; }
        public string Toy1Name {
            get {
                if (Toy1 == null) {
                    return "";

                } else {
                    var field = Toy1.GetType().GetField(Toy1.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy1.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy2 { get; set; }
        public string Toy2Name {
            get {
                if (Toy2 == null) {
                    return "";

                } else {
                    var field = Toy2.GetType().GetField(Toy2.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy2.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy3 { get; set; }
        public string Toy3Name {
            get {
                if (Toy3 == null) {
                    return "";

                } else {
                    var field = Toy3.GetType().GetField(Toy3.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy3.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy4 { get; set; }
        public string Toy4Name {
            get {
                if (Toy4 == null) {
                    return "";

                } else {
                    var field = Toy4.GetType().GetField(Toy4.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy4.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy5 { get; set; }
        public string Toy5Name {
            get {
                if (Toy5 == null) {
                    return "";

                } else {
                    var field = Toy5.GetType().GetField(Toy5.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy5.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy6 { get; set; }
        public string Toy6Name {
            get {
                if (Toy6 == null) {
                    return "";

                } else {
                    var field = Toy6.GetType().GetField(Toy6.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy6.ToString();
                }
            }
        }
        public Christmas2015EnumConst.Christmas2015ToyType? Toy7 { get; set; }
        public string Toy7Name {
            get {
                if (Toy7 == null) {
                    return "";

                } else {
                    var field = Toy7.GetType().GetField(Toy7.ToString());
                    var attrib = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    return attrib != null ? attrib.GetName() : Toy7.ToString();
                }
            }
        }
        public string SynthesisImage { get; set; }
        public string Content { get; set; }
    }

    public class MakeTreeSNSEntry {
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
        public string Zipcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Age { get; set; }
        public string Address {
            get {
                return string.Format("[{0}] {1} {2}", Zipcode, Address1, Address2);
            }
        }
    }
    public class MakeTreeSNSStats {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int TotalCount { get; set; }
        public int FacebookCount { get; set; }
        public int KakaostoryCount { get; set; }
        public int KakaotalkCount { get; set; }
    }
}