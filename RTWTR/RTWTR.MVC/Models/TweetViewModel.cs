using System;

namespace RTWTR.MVC.Models
{
    public class TweetViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public string TwitterUserId { get; set; }

        public string TwitterUserScreenName { get; set; }
  
    }
}
