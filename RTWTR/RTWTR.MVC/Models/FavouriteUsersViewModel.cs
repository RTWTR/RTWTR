using System.Collections.Generic;

namespace RTWTR.MVC.Models
{
    public class FavouriteUsersViewModel
    {
        public ICollection<TwitterUserViewModel> Tweets { get; set; }
    }
}
