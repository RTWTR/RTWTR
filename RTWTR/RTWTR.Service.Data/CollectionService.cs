using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.Service.Data
{
    public class CollectionService : ICollectionService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Collection> collections;
        private readonly IRepository<Tweet> tweets;
        private readonly IRepository<CollectionTweet> collectionTweets;

        public CollectionService(
            ISaver saver, 
            IMappingProvider mapper, 
            IRepository<Collection> collections, 
            IRepository<Tweet> tweets, 
            IRepository<CollectionTweet> collectionTweets
        )
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.collections = collections ?? throw new ArgumentNullException(nameof(collections));
            this.tweets = tweets ?? throw new ArgumentNullException(nameof(tweets));
            this.collectionTweets = collectionTweets ?? throw new ArgumentNullException(nameof(collectionTweets));
        }

        public int AddTweetToCollection(string collectionId, string tweetId)
        {
            if (collectionId.IsNullOrWhitespace())
            {
                throw new InvalidCollectionIdException(nameof(collectionId));
            }

            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            var collection = GetCollectionById(collectionId);
            var tweetToAdd = GetTweetById(tweetId);

            CollectionTweet collectionTweetToAdd = new CollectionTweet()
            {
                CollectionId = collection.Id,
                Collection = collection,
                Tweet = tweetToAdd,
                TweetId = tweetToAdd.Id
            };

            collectionTweets.Add(collectionTweetToAdd);

            return this.saver.SaveChanges();
        }

        public int RemoveTweetFromCollection(string collectionId, string tweetId)
        {
            if (collectionId.IsNullOrWhitespace())
            {
                throw new InvalidCollectionIdException(nameof(collectionId));
            }

            if (tweetId.IsNullOrWhitespace())
            {
                throw new InvalidTweetIdException(nameof(tweetId));
            }

            var tweetToDelete = GetTweetById(tweetId);
            var collection = GetCollectionById(collectionId);

            CollectionTweet collectionTweetToRemove = new CollectionTweet()
            {
                CollectionId = collection.Id,
                Collection = collection,
                Tweet = tweetToDelete,
                TweetId = tweetToDelete.Id
            };

            collectionTweets.Delete(collectionTweetToRemove);

            return this.saver.SaveChanges();
        }

        public IEnumerable<CollectionDTO> GetUserCollections(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException();
            }

            var collections = this.collections
                .All
                .Where(x => x.UserId == userId)
                .OrderBy(x => x);

            return mapper.ProjectTo<CollectionDTO>(collections);
        }
        public int RemoveCollection(string collectionId)
        {
            if (collectionId.IsNullOrWhitespace())
            {
                throw new InvalidCollectionIdException();
            }

            var collectionToDelete = GetCollectionById(collectionId);

            collections.Delete(collectionToDelete);

            return saver.SaveChanges();
        }

        private Tweet GetTweetById(string tweetId)
        {
            var tweetToReturn = tweets
                .All
                .SingleOrDefault(x => x.Id == tweetId);

            if (tweetToReturn.IsNull())
            {
                throw new NullTweetException();
            }

            return tweetToReturn;
        }

        private Collection GetCollectionById(string collectionId)
        {
            var collectionToReturn = collections
                .All
                .SingleOrDefault(x => x.Id == collectionId);

            if (collectionToReturn.IsNull())
            {
                throw new NullCollectionException();
            }

            return collectionToReturn;
        }
    }
}
