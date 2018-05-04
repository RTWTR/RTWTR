using RTWTR.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTWTR.Service.Twitter.Contracts
{
    public interface ITwitterService
    {
        Task<string> GetSingleUserJSON(string screenName);

        Task<TwitterUserDto> GetSingleUser(string screenName);

        Task<string> GetSingleTweetJSON(string id);

        Task<TweetDto> GetSingleTweet(string id);

        Task<string> GetUserTimelineJSON(string id, int tweetsCount);

        Task<ICollection<TweetDto>> GetUserTimeline(string id, int tweetsCount);

        Task<string> SearchUserJSON(string handle);

        Task<TwitterUserDto> SearchUser(string handle);

        Task<string> GetHTML(string id);
    }
}
