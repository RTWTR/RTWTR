using RTWTR.Data.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class Tweet : DataModel
    {
        public string TwitterId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string CreatedAt { get; set; }

        public string InReplyToScreenName { get; set; }

        public ICollection<Tweet> Retweets { get; set; }

        public ICollection<UserTweet> UserTweets { get; set; }
        
         public ICollection<TweetCollection> TweetCollections { get; set; }
    }
}