using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    public class UserTwitterUser
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string TwitterUserId { get; set; }
        public TwitterUser TwitterUser { get; set; }
    }
}
