using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class UserTwitterUser
    {
        // The User
        public string UserId { get; set; }
        public User User { get; set; }

        // and their favourite Twitter User
        public string TwitterUserId { get; set; }
        public TwitterUser TwitterUser { get; set; }
    }
}
