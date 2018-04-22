using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
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
        public IEnumerable<CollectionDTO> GetUserCollections(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            var collections = this.collections
                .All
                .Where(x => x.UserId == userId)
                .OrderBy(x => x);

            return mapper.ProjectTo<CollectionDTO>(collections);
        }

        public int AddTweetToCollection(string collectionId, string tweetId)
        {
            if (tweetId == null || collectionId == null)
            {
                return -1;
            }

            var tweetToAdd = GetTweetById(tweetId);
            if (tweetToAdd == null)
            {
                return -1;
            }

            var collection = GetCollectionById(collectionId);
            if (collection == null)
            {
                return -1;
            }

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
            if (tweetId == null || collectionId == null)
            {
                return -1;
            }

            var tweetToDelete = GetTweetById(tweetId);

            if (tweetToDelete == null)
            {
                return -1;
            }

            var collection = GetCollectionById(collectionId);

            if (collection == null)
            {
                return -1;
            }

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

        public int RemoveCollection(string collectionId)
        {
            if (collectionId == null)
            {
                return -1;
            }

            var collectionToDelete = GetCollectionById(collectionId);

            if (collectionToDelete == null)
            {
                return -1;
            }

            collections.Delete(collectionToDelete);

            return saver.SaveChanges();
        }

        private Tweet GetTweetById(string tweetId)
        {
            return tweets.All.SingleOrDefault(x => x.Id == tweetId);
        }

        private Collection GetCollectionById(string collectionId)
        {
            return collections.All.SingleOrDefault(x => x.Id == collectionId);
        }
    }
}
