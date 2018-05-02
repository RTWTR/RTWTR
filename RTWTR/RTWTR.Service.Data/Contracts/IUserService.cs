using System;
using System.Collections.Generic;
using RTWTR.Data.Models;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface IUserService
    {
        UserDTO GetUserById(string userId);

        ICollection<UserDTO> GetAllUsers();

        ICollection<UserDTO> GetAllAndDeletedUsers();

        int GetAllUsersCount();

        int GetAllAndDeletedUsersCount();

        int DeleteUser(string userId);

        int DeleteUser(User user);
    }
}
