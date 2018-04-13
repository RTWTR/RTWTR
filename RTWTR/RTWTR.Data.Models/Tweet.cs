using RTWTR.Data.Models.Contracts;
using System.Collections.Generic;

namespace RTWTR.Data.Models
{
    public class Tweet : DataModel
    {
        public ulong TwitterId { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string CreatedAt { get; set; }

        public string InReplyToScreenName { get; set; }

        public ICollection<Tweet> Retweets { get; set; }

        public ICollection<UserTweet> UserTweets { get; set; }
    }
}
