using RTWTR.DTO;
using System.Collections.Generic;

namespace RTWTR.Service.Data.Contracts
{
    public interface IFavouriteUserService
    {
        int AddTwitterUserToFavourites(UserDTO userDTO, TwitterUserDto twitterUserDto);
        
        int RemoveTwitterUserFromFavourites(UserDTO userDTO, TwitterUserDto twitterUserDto);
       
        IEnumerable<TwitterUserDto> GetUserFavourites(string userId);

        bool IsFavourite(string userId, string twitterUserId);
    }
}
