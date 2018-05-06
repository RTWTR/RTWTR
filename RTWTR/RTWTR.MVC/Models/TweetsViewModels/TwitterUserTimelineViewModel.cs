using System;
using System.Collections.Generic;

namespace RTWTR.MVC.Models.TweetsViewModels
{
    public class TwitterUserTimelineViewModel
    {
        public TwitterUserViewModel User { get; set; }

        public ICollection<TweetViewModel> Timeline { get; set; }
    }
}
