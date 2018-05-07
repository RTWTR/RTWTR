using RTWTR.Data.Models.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RTWTR.Data.Models
{
    public class TwitterUser : DataModel
    {
        public TwitterUser()
        {
            this.TwitterUserTweets = new List<TwitterUserTweet>();
            this.UserTwitterUsers = new List<UserTwitterUser>();
        }

        [Required]
        public string TwitterId { get; set; }

        [Required]
        public string ScreenName  { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ProfileImageUrl { get; set; }

        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

        public ICollection<UserTwitterUser> UserTwitterUsers { get; set; }
    }
}
