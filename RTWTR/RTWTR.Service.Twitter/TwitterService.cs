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

        public async Task<string> GetSingleUserJSON(string screenName)
        {
            string url = string.Concat(
                this.baseUrl,
                "users/show.json?screen_name=",
                screenName,
                "&include_entities=false"
            );

            return await this.apiProvider.GetJSON(url);
        }

        public Task<string> GetUserTimelineJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> SearchTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SearchUserJSON(string handle)
        {
            // Does NOT work with Apllication-Only authentication
            string url = string.Concat(
                this.baseUrl,
                "users/search.json?q=",
                handle,
                "&include_entities=false"
            );

            return await this.apiProvider.GetJSON(url);
        }
    }
}