using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class TwitterUserTweet : IDeletable, IAuditable
    {
        [Required]
        public string TwitterUserId { get; set; }

        [Required]
        public TwitterUser TwitterUser { get; set; }

        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
