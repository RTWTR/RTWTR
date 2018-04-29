using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTWTR.MVC.Models
{
    public class FavouriteUsersViewModel
    {
        public ICollection<TwitterUserViewModel> Tweets { get; set; }
    }
}
