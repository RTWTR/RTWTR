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

        public string TwitterUserId { get; set; }

        public TwitterUser TwitterUser { get; set; }

        public string CreatedAt { get; set; }

        public string InReplyToScreenName { get; set; }

        public ICollection<Tweet> Retweets { get; set; }

        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

        public ICollection<CollectionTweet> CollectionTweets { get; set; }

        public ICollection<UserTweets> UserTweets { get; set; }

    }
}