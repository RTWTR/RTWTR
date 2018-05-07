using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Abstractions;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTweet : IDeletable, IAuditable
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
        
        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}