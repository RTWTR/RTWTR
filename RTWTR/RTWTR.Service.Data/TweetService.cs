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

namespace RTWTR.Service.Data
{
    public class TweetService : ITweetService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Tweet> tweets;
        private readonly IRepository<UserTweet> userTweets;

        public TweetService(
            ISaver saver,
            IMappingProvider mapper, 
            IRepository<Tweet> tweets,
            IRepository<UserTweet> userTweets
        )
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            return mapper.MapTo<TweetDto>(tweet);
        }

        public ICollection<TweetDto> GetAllTweets()
        {
            var tweets = this.tweets.All;

            return this.mapper.MapTo<List<TweetDto>>(tweets);
        }

        public ICollection<TweetDto> GetAllAndDeletedTweets()
        {
            var tweets = this.tweets.AllAndDeleted;

            return this.mapper.MapTo<List<TweetDto>>(tweets);
        }
        
        public int GetAllTweetsCount()
        {
            return this.tweets.All.Count();
        }

        public int GetAllAndDeletedTweetsCount()
        {
            return this.tweets.AllAndDeleted.Count();
        }

        public int SaveTweet(TweetDto tweetDto)
        {
            if (tweetDto == null)
            {
                return -1;
            }

            if (tweets.All.Any(x => x.Id.Equals(tweetDto.Id)))
            {
                return 1;
            }

            var tweet = this.mapper.MapTo<Tweet>(tweetDto);
            var user = this.mapper.MapTo<TwitterUser>(tweetDto.User);

            var tweetToAdd = new Tweet
            {
                TwitterId = tweet.Id,
                Text = tweet.Text,
                CreatedAt = tweet.CreatedAt,
                TwitterUser = user,
                TwitterUserId = user.Id
            };

            this.tweets.Add(tweetToAdd);

            return this.saver.SaveChanges();
        }

        public int AddTweetToFavourites(string tweetId, UserDTO userDto)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(tweetId));
            }

            if (userDto.IsNull())
            {
                throw new NullUserException(nameof(userDto));
            }

            var tweet = GetSavedTweetById(tweetId);
            var user = this.mapper.MapTo<User>(userDto);

            UserTweet userTweet = null;

            if (this.IsActuallyFavourite(tweet.Id, user.Id))
            {
                if (!this.IsDeleted(tweet.Id, user.Id))
                {
                    // TODO: THrow exception
                    return -1;
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
                    TweetId = tweet.Id
                };

                userTweets.Add(userTweet);
            }

            return this.saver.SaveChanges();
        }

        public int RemoveTweetFromFavourites(string tweetId, string userId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(tweetId));
            }

            if (userId.IsNull())
            {
                throw new NullUserException(nameof(userId));
            }

            if (!IsFavourite(userId, tweetId))
            {
                // TODO: Throw exception
                return -1;
            }

            var tweet = GetSavedTweetById(tweetId);

            var userTweet = this.userTweets
                .All
                .SingleOrDefault(x =>
                    x.UserId.Equals(userId)
                    &&
                    x.TweetId.Equals(tweetId)
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

            var tweets = userTweets.All.Where(x => x.UserId == userId);

            var collectionOfFavourites = tweets.Select(t => mapper.MapTo<TweetDto>(t)).ToList();

            return collectionOfFavourites;
        }

        public int DeleteTweet(string tweetId)
        {
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
                .SingleOrDefault(x => x.Id == tweetId);

            if (tweetToReturn.IsNull())
            {
                throw new NullUserException();
            }

            return tweetToReturn;
        }
    }
}
