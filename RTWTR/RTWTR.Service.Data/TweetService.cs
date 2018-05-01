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
        private readonly IRepository<User> users;
        private readonly IRepository<UserTweets> userTweets;

        public TweetService(ISaver saver, IMappingProvider mapper, IRepository<Tweet> tweets, IRepository<User> users, IRepository<UserTweets> userTweets)
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.tweets = tweets ?? throw new ArgumentNullException(nameof(tweets));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
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

        public int AddTweet(Tweet tweetToSave)
        {
            if (tweetToSave == null)
            {
                return -1;
            }

            this.tweets.Add(tweetToSave);

            return this.saver.SaveChanges();
        }

        public int SaveTweetToFavourites(string tweetId, string userId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(tweetId));
            }

            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidTwitterUserIdException(nameof(userId));
            }

            var user = GetUserById(userId);
            var tweet = GetSavedTweetById(tweetId);

            var userTweet = new UserTweets()
            {
                User = user,
                UserId = user.Id,
                Tweet = tweet,
                TweetId = tweet.Id
            };

            userTweets.Add(userTweet);


            return this.saver.SaveChanges();
        }

        public int DeleteTweetFromFavourites(string tweetId, string userId)
        {
            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(tweetId));
            }

            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidTwitterUserIdException(nameof(userId));
            }

            var user = GetUserById(userId);
            var tweet = GetSavedTweetById(tweetId);

            var userTweet = new UserTweets()
            {
                User = user,
                UserId = user.Id,
                Tweet = tweet,
                TweetId = tweet.Id
            };

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


        private User GetUserById(string userId)
        {
            User userToReturn = users
                .All
                .SingleOrDefault(x => x.Id == userId);

            if (userToReturn.IsNull())
            {
                throw new NullUserException();
            }

            return userToReturn;
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
