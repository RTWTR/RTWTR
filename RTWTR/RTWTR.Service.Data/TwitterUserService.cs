using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;
using System.Linq;

namespace RTWTR.Service.Data
{
    public class TwitterUserService : ITwitterUserService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<TwitterUser> twitterUsers;

        public TwitterUserService(ISaver saver, IMappingProvider mapper, IRepository<TwitterUser> twitterUsers)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.twitterUsers = twitterUsers;
        }

        public TwitterUserDto GetTwitterUserById(string twitterUserId)
        {
            if (string.IsNullOrEmpty(twitterUserId))
            {
                return null;
            }

            var twitterUser = twitterUsers.All.Where(x => x.Id == twitterUserId);

            return mapper.MapTo<TwitterUserDto>(twitterUser);
        }

        public TwitterUserDto GetTwitterUserByScreenName(string screenName)
        {
            if (string.IsNullOrEmpty(screenName))
            {
                return null;
            }

            var twitterUser = twitterUsers.All.Where(x => x.ScreenName == screenName);

            return mapper.MapTo<TwitterUserDto>(twitterUser);
        }

        public int SaveTwitterUser(TwitterUser twitterUser)
        {
            if (twitterUser == null)
            {
                return -1;
            }

            twitterUsers.Add(twitterUser);

            return this.saver.SaveChanges();
        }
    }
}
