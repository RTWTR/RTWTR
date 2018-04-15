using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models
{
    class Timeline
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

    }
}
