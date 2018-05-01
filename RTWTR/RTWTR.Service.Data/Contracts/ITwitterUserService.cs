using RTWTR.Data.Models;
using RTWTR.DTO;

namespace RTWTR.Service.Data.Contracts
{
    public interface ITwitterUserService
    {
        TwitterUserDto GetTwitterUserById(string twitterUserId);

        TwitterUserDto GetTwitterUserByScreenName(string screenName);

        int SaveTwitterUser(TwitterUser twitterUser);

    }
}
