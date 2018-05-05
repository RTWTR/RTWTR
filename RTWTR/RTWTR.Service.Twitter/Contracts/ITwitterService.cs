using RTWTR.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTWTR.Service.Twitter.Contracts
{
    public interface ITwitterService
    {
        Task<string> GetSingleUserJSONAsync(string screenName);

        Task<TwitterUserDto> GetSingleUserAsync(string screenName);

        Task<string> GetSingleTweetJSONAsync(string id);

        Task<TweetDto> GetSingleTweetAsync(string id);

        Task<string> GetUserTimelineJSONAsync(string id, int tweetsCount);

        Task<ICollection<TweetDto>> GetUserTimelineAsync(string id, int tweetsCount);

        Task<string> SearchUserJSONAsync(string handle);

        Task<TwitterUserDto> SearchUserAsync(string handle);

        Task<string> GetHTMLAsync(string id);
    }
}
