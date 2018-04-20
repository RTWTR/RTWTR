using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface ITweetService
    {
        TweetDto GetTweetById(string tweetId);
    }
}
