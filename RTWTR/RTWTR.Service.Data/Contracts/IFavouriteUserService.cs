using RTWTR.DTO;
using System.Collections.Generic;

namespace RTWTR.Service.Data.Contracts
{
    public interface IFavouriteUserService
    {
        int AddTwitterUserToFavourites(string userId, string twitterUserId);
        
        int RemoveTwitterUserFromFavourites(string userId, string twitterUserId);
       
        IEnumerable<TwitterUserDto> GetUserFavourites(string userId);

    }
}
