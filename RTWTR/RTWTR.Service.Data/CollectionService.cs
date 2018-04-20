using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.DTO;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.Service.Data
{
    public class CollectionService :ICollectionService
    {
        public ICollection<CollectionDTO> GetUserCollections(string userId)
        {
            throw new NotImplementedException();
        }

        public void AddTweetToCollection(string userId, string collectionName, string tweetId)
        {
            throw new NotImplementedException();
        }

        public void RemoveTweetFromCollection(string userId, string collectionName, string tweetId)
        {
            throw new NotImplementedException();
        }

        public CollectionDTO ShowUserCollection(string userId, string collectionName)
        {
            throw new NotImplementedException();
        }

        public CollectionDTO GetUserCollectionByName(string collectionName, string userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCollection(string userId, string collectionName)
        {
            throw new NotImplementedException();
        }
    }
}
