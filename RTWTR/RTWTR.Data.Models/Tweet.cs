using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class Tweet
    {
        public int Id { get; set; }

        public ulong TwitterId { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string CreatedAt { get; set; }

        public ICollection<Tweet> Retweets { get; set; }

        public string InReplyToScreenName { get; set; }

    }
}
