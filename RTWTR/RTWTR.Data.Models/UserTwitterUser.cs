using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTwitterUser : IDeletable, IAuditable
    {
        // The User
        public string UserId { get; set; }

        public User User { get; set; }

        // and their favourite Twitter User
        public string TwitterUserId { get; set; }
        
        public TwitterUser TwitterUser { get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
