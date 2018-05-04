using System;
using RTWTR.Data.Models.Abstractions;

namespace RTWTR.Data.Models
{
    public class CollectionTweet : DataModel
    {
        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public string CollectionId { get; set; }

        public Collection Collection { get; set; }
    }
}
