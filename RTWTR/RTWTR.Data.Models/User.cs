using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RTWTR.Data.Models
{
    public class User : IdentityUser
    {
        public ulong? TwitterId { get; set; }
    }
}
