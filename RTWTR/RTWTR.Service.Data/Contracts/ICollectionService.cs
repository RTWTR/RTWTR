using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Service.Data.Contracts
{
    public interface ICollectionService
    {
        //Should be DTO not void
        void GetUserCollections(string userId);
             
        void AddTweetToCollection(string userId,string collectionName, string tweetId);

        void RemoveTweetFromCollection(string userId,string collectionName, string tweetId);

        void ShowUserCollection(string userId, string collectionName);

        void GetUserCollectionByName(string collectionName, string userId);

        void RemoveCollection(string userId, string collectionName);

    }
}
