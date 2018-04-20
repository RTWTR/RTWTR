using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Service.Data.Contracts
{
    public interface IFavouriteUserService
    {
        void AddTwitterUserToFavorites(string userId, string twitterUserId);

        void RemoveTwitterUserFromFavourites(string userId, string twitterUserId);

        void GetUserFavourites(string userId);

    }
}
