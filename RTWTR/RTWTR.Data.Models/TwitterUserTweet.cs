using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class TwitterUserTweet : IDeletable, IAuditable
    {
        public string TwitterUserId { get; set; }
        
        public TwitterUser TwitterUser { get; set; }
        
        public string TweetId { get; set; }
        
        public Tweet Tweet { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
