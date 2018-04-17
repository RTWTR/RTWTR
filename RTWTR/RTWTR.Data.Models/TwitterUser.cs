using RTWTR.Data.Models.Abstractions;
using System.Collections.Generic;

namespace RTWTR.Data.Models
{
    public class TwitterUser : DataModel
    {
        public string ScreenName  { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

    }
}
