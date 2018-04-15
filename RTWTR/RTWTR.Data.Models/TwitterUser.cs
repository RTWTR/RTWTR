using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.Data.Models.Abstractions;

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
