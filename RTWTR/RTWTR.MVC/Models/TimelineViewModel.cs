using System.Collections.Generic;

namespace RTWTR.MVC.Models
{
    public class TimelineViewModel
    {
        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}
