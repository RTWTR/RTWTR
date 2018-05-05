using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.Data.Models.Abstractions;

namespace RTWTR.Data.Models
{
    public class TwitterUserTweet : DataModel
    {
        public string TwitterUserId { get; set; }

        public TwitterUser TwitterUser { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
