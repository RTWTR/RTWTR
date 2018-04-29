using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Threading.Tasks;
using RTWTR.DTO;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Service.Twitter
{
    public class TwitterService : ITwitterService
    {
        private readonly string baseUrl;
        private readonly IApiProvider apiProvider;
        private readonly IJsonProvider jsonProvider;

        public TwitterService(IApiProvider apiProvider, IJsonProvider jsonProvider)
        {
            this.apiProvider = apiProvider
                ??
                throw new ArgumentNullException(nameof(apiProvider));
            this.jsonProvider = jsonProvider 
                ??
                throw new ArgumentNullException(nameof(jsonProvider));
           
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

        public async Task<ICollection<TweetDto>> GetUserTimeline(string userId, int tweetsCount)
        {
            string url = string.Concat(this.baseUrl,
                $"statuses/user_timeline.json?user_id={userId}&count={tweetsCount}");

            var json = await this.GetRequestJSON(url);

            if (json != string.Empty)
            {
                return this.jsonProvider.DeserializeObject<List<TweetDto>>(json.ToString());
            }

            return new List<TweetDto>();
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

        private async Task<string> GetRequestJSON(string url)
        {
            return await this.apiProvider.GetJSON(url);
        }
    }
}