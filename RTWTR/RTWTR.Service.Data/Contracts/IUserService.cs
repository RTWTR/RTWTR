using System;
using System.Collections.Generic;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface IUserService
    {
        UserDTO GetUserById(string userId);

        ICollection<UserDTO> GetAllUsers();

        ICollection<UserDTO> GetAllAndDeletedUsers();
    }
}
