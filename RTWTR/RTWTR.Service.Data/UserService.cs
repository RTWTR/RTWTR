using System;
using System.Collections.Generic;
using System.Linq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.Service.Data
{
    public class UserService : IUserService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<User> users;

        public UserService(
            ISaver saver,
            IMappingProvider mapper,
            IRepository<User> users
        )
        {
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public UserDTO GetUserById(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException();
            }

            var user = this.users
                .AllAndDeleted
                .SingleOrDefault(x => x.Id == userId);

            if (user.IsNull())
            {
                throw new NullUserException();
            }

            return this.mapper.MapTo<UserDTO>(user);
        }

        public UserDTO GetUserByEmail(string userEmail)
        {
            if (userEmail.IsNullOrWhitespace())
            {
                throw new InvalidUserIdException();
            }

            var user = this.users
                .AllAndDeleted
                .SingleOrDefault(x => x.Email == userEmail);

            if (user.IsNull())
            {
                throw new NullUserException();
            }

            return this.mapper.MapTo<UserDTO>(user);
        }

        public ICollection<UserDTO> GetAllUsers()
        {
            var users = this.users.All;

            return this.mapper.MapTo<List<UserDTO>>(users);
        }

        public ICollection<UserDTO> GetAllAndDeletedUsers()
        {
            var users = this.users.AllAndDeleted;

            return this.mapper.MapTo<List<UserDTO>>(users);
        }

        public int GetAllUsersCount()
        {
            return this.users.All.Count();
        }

        public int GetAllAndDeletedUsersCount()
        {
            return this.users.AllAndDeleted.Count();
        }

        public int DeleteUser(string userId)
        {
            var user = this.users
                .All
                .SingleOrDefault(x => x.Id == userId);

            return this.DeleteUser(user);
        }

        public int DeleteUser(User user)
        {
            if (user.IsNull())
            {
                throw new NullUserException();
            }

            this.users.Delete(user);

            return this.saver.SaveChanges();
        }
    }
}
