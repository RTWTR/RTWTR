using System;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTweets : IDeletable
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedOn { get; set; }
    }
}
