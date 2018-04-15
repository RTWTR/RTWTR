using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    class TimelineTweets
    {
        public string TimelineId { get; set; }

        public Timeline Timeline { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
