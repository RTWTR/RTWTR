using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.Service.Data
{
    public class FavouriteUserService : IFavouriteUserService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<User> users;
        private readonly IRepository<TwitterUser> twitterUsers;
        private readonly IRepository<UserTwitterUser> userTwitterUsers;

        public FavouriteUserService(
            ISaver saver, 
            IMappingProvider mapper, 
            IRepository<User> users, 
            IRepository<TwitterUser> twitterUsers, 
            IRepository<UserTwitterUser> userTwitterUsers
        )
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.twitterUsers = twitterUsers ?? throw new ArgumentNullException(nameof(twitterUsers));
            this.userTwitterUsers = userTwitterUsers ?? throw new ArgumentNullException(nameof(userTwitterUsers));
        }

        public int AddTwitterUserToFavourites(string userId, string twitterUserId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(userId));
            }

            if (twitterUserId.IsNullOrWhitespace())
            {
                throw new InvalidTwitterUserIdException(nameof(twitterUserId));
            }

            var user = GetUserById(userId);
            var twitterUser = GetTwitterUserById(twitterUserId);

            var userTwitterUserToAdd = new UserTwitterUser() 
            { 
                User = user, 
                UserId = user.Id, 
                TwitterUser = twitterUser, 
                TwitterUserId = twitterUser.Id 
            };

            userTwitterUsers.Add(userTwitterUserToAdd);

            return this.saver.SaveChanges();
        }

        public int RemoveTwitterUserFromFavourites(string userId, string twitterUserId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(userId));
            }

            if (twitterUserId.IsNullOrWhitespace())
            {
                throw new InvalidTwitterUserIdException(nameof(twitterUserId));
            }

            var user = GetUserById(userId);
            var twitterUser = GetTwitterUserById(twitterUserId);

            var userTwitterUserToRemove = new UserTwitterUser() 
            { 
                User = user, 
                UserId = user.Id, 
                TwitterUser = twitterUser, 
                TwitterUserId = twitterUser.Id 
            };

            userTwitterUsers.Delete(userTwitterUserToRemove);

            return this.saver.SaveChanges();
        }

        public IEnumerable<TwitterUserDto> GetUserFavourites(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException(nameof(userId));
            }

            var favourites = userTwitterUsers
                .All
                .Where(x => x.UserId == userId);

            return mapper.ProjectTo<TwitterUserDto>(favourites);
        }

        private User GetUserById(string userId)
        {
            User userToReturn = users
                .All
                .SingleOrDefault(x => x.Id == userId);

            if (userToReturn.IsNull())
            {
                throw new NullUserException();
            }

            return userToReturn;
        }

        private TwitterUser GetTwitterUserById(string userId)
        {
            TwitterUser twitterUserToReturn = twitterUsers
                .All
                .SingleOrDefault(x => x.Id == userId);

            if (twitterUserToReturn.IsNull())
            {
                throw new NullTwitterUserException();
            }

            return twitterUserToReturn;
        }
    }
}
