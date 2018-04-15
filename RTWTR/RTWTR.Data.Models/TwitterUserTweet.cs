using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class TwitterUserTweet
    {
        public string TwitterUserId { get; set; }

        public TwitterUser TwitterUser { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
