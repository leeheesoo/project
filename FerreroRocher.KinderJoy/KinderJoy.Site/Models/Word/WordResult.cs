using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Word
{
    public class WordResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }

    public class WordTwitterResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string PostId { get; set; }
        public string SnsId { get; set; }
    }
}