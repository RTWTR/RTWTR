using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface ITwitterUserService
    {
        TwitterUserDto GetTwitterUserById(string Id);

        TwitterUserDto GetTwitterUserByScreenName(string screenName);

    }
}
