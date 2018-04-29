using RTWTR.Data.Models;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface ITweetService
    {
        TweetDto GetTweetById(string tweetId);

        int AddTweet(Tweet tweetToSave);

        int SaveTweetToFavourites(string tweetId,string userId);

        int DeleteTweetFromFavourites(string tweetId, string userId);

    }
}
