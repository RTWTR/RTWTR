using System;
using System.Threading.Tasks;

namespace RTWTR.Service.Twitter.Contracts
{
    public interface IApiProvider
    {
        Task<string> GetSingleUserJSON(string id);

        Task<string> GetSingleTweetJSON(string id);

        Task<string> GetUserTimelineJSON(string id);
        
        Task<string> SearchUserJSON(string handle);

        Task<string> SearchTweetJSON(string id);
    }
}
