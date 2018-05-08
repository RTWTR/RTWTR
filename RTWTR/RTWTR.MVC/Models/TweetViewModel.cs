using System;
using RTWTR.Data.Models;

namespace RTWTR.MVC.Models
{
    public class TweetViewModel
    {
        public string Id { get; set; }

        public string TwitterId { get; set; }

        public string CreatedAt { get; set; }

        public string Text { get; set; }

        public string TwitterUserId { get; set; }

        public string TwitterUserName { get; set; }

        public string TwitterUserScreenName { get; set; }

        public string TwitterUserProfileImageUrl { get; set; }

        public bool IsFavourite { get; set; }
    }
}
