using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable
    {
        public ulong? TwitterId { get; set; }

        public ICollection<UserTweet> UserTweets { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
