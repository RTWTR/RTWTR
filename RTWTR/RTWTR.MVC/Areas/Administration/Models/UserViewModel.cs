using System;
using System.Collections.Generic;
using RTWTR.MVC.Models;

namespace RTWTR.MVC.Areas.Administration.Models
{
    public class UserViewModel : MinifiedUserViewModel
    {
        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}
