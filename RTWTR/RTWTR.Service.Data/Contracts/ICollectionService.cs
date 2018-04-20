using RTWTR.DTO;
using System.Collections.Generic;

namespace RTWTR.Service.Data.Contracts
{
    public interface ICollectionService
    {
        
        IEnumerable<CollectionDTO> GetUserCollections(string userId);
             
        int AddTweetToCollection(string collectionId, string tweetId);
        
        int RemoveTweetFromCollection(string collectionId, string tweetId);

        int RemoveCollection(string collectionId);

    }
}
