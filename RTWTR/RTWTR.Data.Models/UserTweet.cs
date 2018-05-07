using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Abstractions;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTweet : DataModel
    {
        public string UserId { get; set; }
        
        public User User { get; set; }
        
        public string TweetId { get; set; }
        
        public Tweet Tweet { get; set; }
    }
}
