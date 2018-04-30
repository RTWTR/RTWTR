using RTWTR.DTO;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Service.Twitter.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json.Linq;

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
            throw new NotImplementedException();
            //return await this.apiProvider.GetJSON(url);
        }

        public async Task<ICollection<TweetDto>> GetUserTimeline(string userId, int tweetsCount)
        {
            string url = string.Concat(this.baseUrl,
                $"statuses/user_timeline.json?user_id={userId}&count={tweetsCount}");

            var response = await this.GetRequestJson(url);

            if (response == null)
            {
                return new List<TweetDto>();
            }
            return this.jsonProvider.DeserializeObject<List<TweetDto>>(response.ToString());

        }

        public Task<string> SearchTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TwitterUserDto> SearchUserJSON(string handle)
        {
            string url = string.Concat(this.baseUrl, 
                $"users/lookup.json?screen_name={handle}");

            var response = await this.GetRequestJson(url);

            if (response == null)
            {
                return new TwitterUserDto();
            }

            return this.jsonProvider.DeserializeObject<TwitterUserDto>(response.ToString());

        }

        private async Task<JArray> GetRequestJson(string url)
        {
            return await this.apiProvider.GetJSON(url);
        }
    }
}