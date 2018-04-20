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

        public CollectionService(ISaver saver, IMappingProvider mapper, IRepository<Collection> collections, IRepository<Tweet> tweets, IRepository<CollectionTweet> collectionTweets)
        {
            this.mapper = mapper;
            this.saver = saver;
            this.collections = collections;
            this.tweets = tweets;
            this.collectionTweets = collectionTweets;
        }
        public IEnumerable<CollectionDTO> GetUserCollections(string userId)
        {
            var collections = this.collections.All.Where(x => x.UserId == userId)
                .OrderBy(x => x);

            return mapper.ProjectTo<CollectionDTO>(collections);
        }

        public int AddTweetToCollection(string collectionId, string tweetId)
        {
            var tweetToAdd = GetTweetById(tweetId);

            var collection = GetCollectionById(collectionId);

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


        public int RemoveCollection(string collectionId)
        {
            var collectionToDelete = GetCollectionById(collectionId);

            collections.Delete(collectionToDelete);

            return saver.SaveChanges();

        }

        private Tweet GetTweetById(string tweetId)
        {
            Tweet tweetToReturn = tweets.All.SingleOrDefault(x => x.Id == tweetId);

            return tweetToReturn;
        }

        private Collection GetCollectionById(string collectionId)
        {
            Collection collectionToReturn = collections.All.SingleOrDefault(x => x.Id == collectionId);

            return collectionToReturn;
        }
    }
}
