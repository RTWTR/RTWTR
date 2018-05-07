using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace RTWTR.Service.Data
{
    public class TweetService : ITweetService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mappingProvider;
        private readonly IRepository<Tweet> tweets;
        private readonly IRepository<UserTweet> userTweets;

        public TweetService(
            ISaver saver,
            IMappingProvider mappingProvider, 
            IRepository<Tweet> tweets,
            IRepository<UserTweet> userTweets
        )
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.tweets = tweets ?? throw new ArgumentNullException(nameof(tweets));
            this.userTweets = userTweets ?? throw new ArgumentNullException(nameof(userTweets));
        }

        public TweetDto GetTweetById (string tweetId)
        {
            if (string.IsNullOrEmpty(tweetId))
            {
                return null;
            }

            var tweet = tweets
                .All
                .SingleOrDefault(x => x.Id == tweetId);

            return mappingProvider.MapTo<TweetDto>(tweet);
        }

        public ICollection<TweetDto> GetAllTweets()
        {
            var tweets = this.tweets.All;

            return this.mappingProvider.MapTo<List<TweetDto>>(tweets);
        }

        public ICollection<TweetDto> GetAllAndDeletedTweets()
        {
            var tweets = this.tweets.AllAndDeleted;

            return this.mappingProvider.MapTo<List<TweetDto>>(tweets);
        }
        
        public int GetAllTweetsCount()
        {
            return this.tweets.All.Count();
        }

        public int GetAllAndDeletedTweetsCount()
        {
            return this.tweets.AllAndDeleted.Count();
        }

        public int SaveTweet(TweetDto tweetDto, TwitterUserDto twitterUser)
        {
            if (tweetDto == null)
            {
                return -1;
            }

            if (tweets.All.Any(x => x.TwitterId.Equals(tweetDto.TwitterId)))
            {
                return 1;
            }

            var tweet = this.mappingProvider.MapTo<Tweet>(tweetDto);
            var user = this.mappingProvider.MapTo<TwitterUser>(twitterUser);

            tweet.TwitterUser = user;
            tweet.TwitterUserId = user.Id;
            tweet.RetweetCount = 0;

            //var tweetToAdd = new Tweet
            //{
            //    TwitterId = tweet.TwitterId,
            //    Text = tweet.Text,
            //    CreatedAt = tweet.CreatedAt,
            //    TwitterUser = user,
            //    TwitterUserId = user.Id
            //};

            this.tweets.Add(tweet);

            return this.saver.SaveChanges();
        }

        public int AddTweetToFavourites(string tweetId, UserDTO userDto)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            if (userDto.IsNull())
            {
                throw new NullUserException(nameof(userDto));
            }

            var tweet = GetSavedTweetById(tweetId);
            var user = this.mappingProvider.MapTo<User>(userDto);

            UserTweet userTweet = null;

            if (this.IsActuallyFavourite(tweet.Id, user.Id))
            {
                if (!this.IsDeleted(tweet.Id, user.Id))
                {
                    throw new ArgumentException();
                }

                userTweet = this.userTweets
                    .AllAndDeleted
                    .SingleOrDefault(x =>
                        x.TweetId.Equals(tweet.Id)
                        &&
                        x.UserId.Equals(user.Id)
                    );

                userTweet.IsDeleted = false;

                this.userTweets.Update(userTweet);
            }
            else
            {
                userTweet = new UserTweet
                {
                    User = user,
                    UserId = user.Id,
                    Tweet = tweet,
                    TweetId = tweet.TwitterId
                };

                userTweets.Add(userTweet);
            }

            return this.saver.SaveChanges();
        }

        public int RemoveTweetFromFavourites(string tweetId, string userId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(userId));
            }

            var tweet = GetSavedTweetById(tweetId);

            if (!IsFavourite(tweet.Id, userId))
            {
                throw new ArgumentException();
            }

            var userTweet = this.userTweets
                .All
                .SingleOrDefault(x =>
                    x.UserId.Equals(userId)
                    &&
                    x.TweetId.Equals(tweet.Id)
                );

            if (userTweet.IsNull())
            {
                throw new NullTweetException();
            }

            userTweets.Delete(userTweet);

            return this.saver.SaveChanges();
        }

        public ICollection<TweetDto> GetUserFavourites(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidTwitterUserIdException(nameof(userId));
            }

            var tweets = userTweets
                .All
                .Where(x => x.UserId.Equals(userId))
                .Select(x => x.Tweet)
                .Include(x => x.TwitterUser)
                .ToList();

            var collectionOfFavourites = mappingProvider.MapTo<List<TweetDto>>(tweets);

            return collectionOfFavourites;
        }

        public int DeleteTweet(string tweetId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            var tweet = this.tweets
                .All
                .SingleOrDefault(x => x.Id.Equals(tweetId));

            return this.DeleteTweet(tweet);
        }

        public int DeleteTweet(Tweet tweet)
        {
            if (tweet.IsNull())
            {
                throw new NullTweetException();
            }

            this.tweets.Delete(tweet);

            return this.saver.SaveChanges();
        }

        public int Retweet(string tweetId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            var tweet = GetSavedTweetById(tweetId);

            tweet.RetweetCount++;

            return this.saver.SaveChanges();
        }

        public bool IsFavourite(string tweetId, string userId)
        {
            return this.userTweets
                .All
                .Any(x =>
                    x.TweetId.Equals(tweetId)
                    &&
                    x.UserId.Equals(userId)
                );
        }

        public bool IsDeleted(string tweetId, string userId)
        {
            var entity = this.userTweets
                .AllAndDeleted
                .SingleOrDefault(x =>
                    x.TweetId.Equals(tweetId)
                    &&
                    x.UserId.Equals(userId)
                );

            return entity.IsDeleted;
        }

        public bool Exists(string tweetId)
        {
            return this.tweets
                .All
                .Any(x => x.TwitterId.Equals(tweetId));
        }

        private bool IsActuallyFavourite(string tweetId, string userId)
        {
            return this.userTweets
                .AllAndDeleted
                .Any(x =>
                    x.TweetId.Equals(tweetId)
                    &&
                    x.UserId.Equals(userId)
                );
        }

        private Tweet GetSavedTweetById(string tweetId)
        {
            Tweet tweetToReturn = tweets
                .All
                .SingleOrDefault(x => x.TwitterId.Equals(tweetId));

            if (tweetToReturn.IsNull())
            {
                throw new NullTweetException();
            }

            return tweetToReturn;
        }
    }
}
