using RTWTR.DTO;
using System.Collections.Generic;

namespace RTWTR.Service.Data.Contracts
{
    public interface IFavouriteUserService
    {
        int AddTwitterUserToFavourites(UserDTO userDTO, TwitterUserDto twitterUserDto);
        
        int RemoveTwitterUserFromFavourites(UserDTO userDTO, TwitterUserDto twitterUserDto);
       
        ICollection<TwitterUserDto> GetUserFavourites(string userId);

        bool IsDeleted(string userId, string twitterUserId);

        bool IsFavourite(string userId, string twitterUserId);
    }
}
