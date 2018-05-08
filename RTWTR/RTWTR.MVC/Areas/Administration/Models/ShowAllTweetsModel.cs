using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTWTR.MVC.Models;

namespace RTWTR.MVC.Areas.Administration.Models
{
    public class ShowAllTweetsModel
    {
        public ICollection<TweetViewModel> Tweets;
    }
}
