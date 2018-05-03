using System;

namespace RTWTR.MVC.Areas.Administration.Models
{
    public class MinifiedUserViewModel
    {
         public string Id { get; set; }

        public string TwitterId { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
