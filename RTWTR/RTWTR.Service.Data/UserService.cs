using System;
using System.Collections.Generic;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
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
            throw new NotImplementedException();
        }

        public ICollection<UserDTO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public ICollection<UserDTO> GetAllAndDeletedUsers()
        {
            throw new NotImplementedException();
        }
    }
}
