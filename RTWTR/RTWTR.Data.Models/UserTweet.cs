using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class UserTweet
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
