using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Word
{
    public class WordShare
    {
        public int WordEventId { get; set; }
        public string SnsType { get; set; }
        public string SnsId { get; set; }
        public string PostId { get; set; }
        public string Ip { get; set; }
        public string Channel { get; set; }
    }
}