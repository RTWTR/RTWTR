using System.Collections.Generic;

namespace RTWTR.MVC.Models
{
    public class FavouriteTweetsViewModel
    {
        public ICollection<TweetViewModel> Tweets { get; set; }

    }
}
