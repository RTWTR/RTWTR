﻿using RTWTR.Data.Models.Abstractions;
using System.Collections.Generic;

namespace RTWTR.Data.Models
{
    public class TwitterUser : DataModel
    {
        public TwitterUser()
        {
            this.TwitterUserTweets = new List<TwitterUserTweet>();
            this.UserTwitterUsers = new List<UserTwitterUser>();
        }

        // TODO: Match the properties' names with JSON names
        public string TwitterId { get; set; }

        public string ScreenName  { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

        public ICollection<UserTwitterUser> UserTwitterUsers { get; set; }
    }
}
