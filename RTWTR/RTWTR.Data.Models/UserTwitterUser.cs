using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.Data.Models.Abstractions;

namespace RTWTR.Data.Models
{
    public class UserTwitterUser : DataModel
    {
        // The User
        public string UserId { get; set; }
        public User User { get; set; }

        // and their favourite Twitter User
        public string TwitterUserId { get; set; }
        public TwitterUser TwitterUser { get; set; }
    }
}
