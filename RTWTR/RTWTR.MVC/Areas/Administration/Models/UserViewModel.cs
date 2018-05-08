using System;
using System.Collections.Generic;
using RTWTR.MVC.Models;

namespace RTWTR.MVC.Areas.Administration.Models
{
    public class UserViewModel
    {
        public MinifiedUserViewModel User { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }

        public ICollection<TwitterUserViewModel> Favourites { get; set; }
    }
}
