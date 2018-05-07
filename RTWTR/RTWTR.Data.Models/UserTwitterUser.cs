using System;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class UserTwitterUser : IDeletable, IAuditable
    {
        // The User
        [Required]
        public string UserId { get; set; }
        [Required]
        public User User { get; set; }

        // and their favourite Twitter User
        [Required]
        public string TwitterUserId { get; set; }

        [Required]
        public TwitterUser TwitterUser { get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
