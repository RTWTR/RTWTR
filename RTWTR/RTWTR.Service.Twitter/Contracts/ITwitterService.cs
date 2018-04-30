using RTWTR.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTWTR.Service.Twitter.Contracts
{
    public interface ITwitterService
    {
        Task<string> GetSingleUserJSON(string id);

        Task<string> GetSingleTweetJSON(string id);

        Task<ICollection<TweetDto>> GetUserTimeline(string id, int tweetsCount);

        Task<TwitterUserDto> SearchUserJSON(string handle);

        Task<string> SearchTweetJSON(string id);
    }
}
