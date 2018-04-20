using RTWTR.DTO;
using System.Collections.Generic;

namespace RTWTR.Service.Data.Contracts
{
    public interface ICollectionService
    {
        //Should be DTO not void
        ICollection<CollectionDTO> GetUserCollections(string userId);
             
        void AddTweetToCollection(string userId,string collectionName, string tweetId);

        void RemoveTweetFromCollection(string userId,string collectionName, string tweetId);

        CollectionDTO ShowUserCollection(string userId, string collectionName);

        CollectionDTO GetUserCollectionByName(string collectionName, string userId);

        void RemoveCollection(string userId, string collectionName);

    }
}
