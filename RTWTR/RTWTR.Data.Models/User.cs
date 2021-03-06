﻿using Microsoft.AspNetCore.Identity;
using RTWTR.Data.Models.Contracts;
using System;
using System.Collections.Generic;

namespace RTWTR.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable
    {
        public User()
        {
            this.TwitterUserTweets = new List<TwitterUserTweet>();
            this.UserTwitterUsers = new List<UserTwitterUser>();
            this.UserTweets = new List<UserTweet>();
        }
        public string TwitterId { get; set; }
  
        public ICollection<TwitterUserTweet> TwitterUserTweets { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ICollection<UserTwitterUser> UserTwitterUsers { get; set; }

        public ICollection<UserTweet> UserTweets { get; set; }

    }
}
