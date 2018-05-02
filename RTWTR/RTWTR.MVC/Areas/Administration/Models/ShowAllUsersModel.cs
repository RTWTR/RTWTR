using System;
using System.Collections.Generic;
using RTWTR.DTO;

namespace RTWTR.MVC.Areas.Administration.Models
{
    public class ShowAllUsersModel
    {
        public ICollection<UserDTO> Users { get; set; }
    }
}
