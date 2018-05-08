using System.Collections.Generic;

namespace RTWTR.MVC.Models
{
    public class CollectionViewModel
    {
        public string Name { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}
