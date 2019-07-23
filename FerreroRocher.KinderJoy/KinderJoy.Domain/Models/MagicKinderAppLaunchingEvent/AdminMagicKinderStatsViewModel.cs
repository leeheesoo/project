using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderJoy.Domain.Models.MagicKinderAppLaunchingEvent
{
    public class AdminMagicKinderStatsViewModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int TotalCount { get; set; }
        public int TotalIsShowCount { get; set; }
        public int ColoringCount { get; set; }
        public int ColoringIsShowCount { get; set; }
        public int VideoCount { get; set; }
        public int VideoIsShowCount { get; set; }
        public int PlayingCount { get; set; }
        public int PlayingIsShowCount { get; set; }
        public int StoryCount { get; set; }
        public int StoryIsShowCount { get; set; }
        public int EarthTripCount { get; set; }
        public int EarthTripIsShowCount { get; set; }
        public int EtcCount { get; set; }
        public int EtcIsShowCount { get; set; }
    }
}
