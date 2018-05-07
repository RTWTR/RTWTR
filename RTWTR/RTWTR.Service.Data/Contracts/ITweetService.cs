using System.Collections.Generic;
using RTWTR.Data.Models;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface ITweetService
    {
        TweetDto GetTweetById(string tweetId);

        ICollection<TweetDto> GetAllTweets();

        ICollection<TweetDto> GetAllAndDeletedTweets();

        ICollection<TweetDto> GetUserFavourites(string userId);

        int GetAllTweetsCount();

        int GetAllAndDeletedTweetsCount();

        int SaveTweet(TweetDto tweetToSave, TwitterUserDto twitterUser);

        int AddTweetToFavourites(string tweetId, UserDTO userDto);

        int RemoveTweetFromFavourites(string tweetId, string userId);

        int DeleteTweet(string tweetId);

        int DeleteTweet(Tweet tweet);

        int Retweet(string tweetId);

        int RetweetCount();

        bool IsFavourite(string tweetId, string userId);

        bool IsDeleted(string tweetId, string userId);

        bool Exists(string tweetId);
    }
}
