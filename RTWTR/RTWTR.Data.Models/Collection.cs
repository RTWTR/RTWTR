using System;
using System.Collections.Generic;
using RTWTR.Data.Models.Abstractions;

namespace RTWTR.Data.Models
{
    public class Collection : DataModel
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CollectionTweet> CollectionTweets { get; set; }
    }
}
