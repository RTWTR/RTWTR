using System;

namespace RTWTR.Data.Models
{
    public class CollectionTweet
    {
        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public string CollectionId { get; set; }

        public Collection Collection { get; set; }
    }
}