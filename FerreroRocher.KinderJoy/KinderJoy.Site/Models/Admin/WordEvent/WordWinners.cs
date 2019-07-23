using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Models.Admin.WordEvent
{
    public class WordWinners
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public int GiftMaleCount { get; set; }
        public int GiftFemaleCount { get; set; }
        public int TotalEntry { get; set; }
        public int TotalShare { get; set; }
        public int St { get; set; }
    }
}
