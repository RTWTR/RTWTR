using RTWTR.Data.Models.Abstractions;
using System.Collections.Generic;

namespace RTWTR.Data.Models
{
    public class Tweet : DataModel
    {
        public string TwitterId { get; set; }

        public string Text { get; set; }

        public string TwitterUserId { get; set; }

        public TwitterUser User { get; set; }

        // Comes from the Twitter JSON
        // NOT the same as CreatedOn from DataModel
        public string CreatedAt { get; set; }

        public string InReplyToScreenName { get; set; } 

        public ICollection<Tweet> Retweets { get; set; }

        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

        public ICollection<CollectionTweet> CollectionTweets { get; set; }
    }
}