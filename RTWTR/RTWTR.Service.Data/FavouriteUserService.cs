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
    // TODO: Fix tests!!!!!!!!!!!
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

        public int AddTwitterUserToFavourites(UserDTO userDto, TwitterUserDto twitterUserDto)
        {
            if (userDto.IsNull())
            {
                throw new NullUserException(nameof(userDto));
            }

            if (twitterUserDto.IsNull())
            {
                throw new NullTwitterUserException(nameof(twitterUserDto));
            }

            var user = this.mapper.MapTo<User>(userDto);
            var twitterUser = this.mapper.MapTo<TwitterUser>(twitterUserDto);

            UserTwitterUser userTwitterUser = null;

            if (this.IsActuallyFavourite(user.Id, twitterUser.Id))
            {
                userTwitterUser = this.userTwitterUsers
                    .AllAndDeleted
                    .SingleOrDefault(x => 
                        x.UserId.Equals(user.Id)
                        &&
                        x.TwitterUserId.Equals(twitterUser.Id
                    ));
                
                if (!this.IsDeleted(user.Id, twitterUser.Id))
                {
                    // TODO: throw adequate exception
                    return -1;
                }

                userTwitterUser.IsDeleted = false;

                userTwitterUsers.Update(userTwitterUser);
            }
            else
            {
                userTwitterUser = new UserTwitterUser() 
                { 
                    User = user, 
                    UserId = user.Id, 
                    TwitterUser = twitterUser, 
                    TwitterUserId = twitterUser.Id
                };

                userTwitterUsers.Add(userTwitterUser);
            }

            return this.saver.SaveChanges();
        }

        public int RemoveTwitterUserFromFavourites(UserDTO userDto, TwitterUserDto twitterUserDto)
        {
            if (userDto.IsNull())
            {
                throw new NullUserException(nameof(userDto));
            }

            if (twitterUserDto.IsNull())
            {
                throw new NullTwitterUserException(nameof(twitterUserDto));
            }

            if (!IsFavourite(userDto.Id, twitterUserDto.Id))
            {
                // TODO: throw adequate exception
                return -1;
            }

            var user = this.mapper.MapTo<User>(userDto);
            var twitterUser = this.mapper.MapTo<TwitterUser>(twitterUserDto);

            var userTwitterUserToRemove = this.userTwitterUsers
                .All
                .SingleOrDefault(x =>
                    x.UserId.Equals(user.Id) 
                    &&
                    x.TwitterUserId.Equals(twitterUser.Id)
                );
            
            if (userTwitterUserToRemove.IsNull())
            {
                throw new NullReferenceException();
            }

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

        public bool IsFavourite(string userId, string twitterUserId)
        {
            return this.userTwitterUsers.All.Any(x => x.TwitterUserId.Equals(twitterUserId) && x.UserId.Equals(userId));
        }

        public bool IsDeleted(string userId, string twitterUserId)
        {
            var entity = this.userTwitterUsers.AllAndDeleted.SingleOrDefault(x => x.TwitterUserId.Equals(twitterUserId) && x.UserId.Equals(userId));

            return entity.IsDeleted;
        }

        private bool IsActuallyFavourite(string userId, string twitterUserId)
        {
            return this.userTwitterUsers.AllAndDeleted.Any(x => x.TwitterUserId.Equals(twitterUserId) && x.UserId.Equals(userId));
        }
    }
}
