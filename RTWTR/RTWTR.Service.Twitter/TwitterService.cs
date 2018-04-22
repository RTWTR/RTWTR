using System;
using System.Threading.Tasks;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Service.Twitter
{
    public class TwitterService : ITwitterService
    {
        private string baseUrl;
        
        private readonly IApiProvider apiProvider;

        public TwitterService(IApiProvider apiProvider)
        {
            this.apiProvider = apiProvider
                ??
                throw new ArgumentNullException(nameof(apiProvider));            
            this.baseUrl = "https://api.twitter.com/1.1/";
        }

        public Task<string> GetSingleTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSingleUserJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserTimelineJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> SearchTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public string SearchUserJSON(string handle)
        {
            string additional = $"users/search.json?q={handle}";
            string url = string.Concat(
                this.baseUrl,
                "users/search.json?q=",
                handle,
                "&include_entities=false"
            );

            return this.apiProvider.GetJSON(url);
        }
    }
}