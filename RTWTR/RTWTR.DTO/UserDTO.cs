using System;
using Microsoft.AspNetCore.Identity;

namespace RTWTR.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
