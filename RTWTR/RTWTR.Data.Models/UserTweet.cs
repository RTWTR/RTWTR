using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Abstractions;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTweet : DataModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }
    }
}
