using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RTWTR.DTO;
using RTWTR.Infrastructure;
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

        public Task<TweetDto> GetSingleTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TwitterUserDto> GetSingleUserJSON(string screenName)
        {
            string url = string.Concat(
                this.baseUrl,
                "users/show.json?screen_name=",
                screenName,
                "&include_entities=false"
            );
            
            var response = await this.GetRequestJson(url);

            if (response.IsNullOrWhitespace())
            {
                return new TwitterUserDto();
            }

            return this.jsonProvider.DeserializeObject<TwitterUserDto>(response);
        }

        public async Task<ICollection<TweetDto>> GetUserTimeline(string screenName, int tweetsCount)
        {
            string url = string.Concat(this.baseUrl,
                $"statuses/user_timeline.json?screen_name={screenName}&count={tweetsCount}");

            var response = await this.GetRequestJson(url);

            if (response.IsNullOrWhitespace())
            {
                return new List<TweetDto>();
            }

            return this.jsonProvider.DeserializeObject<List<TweetDto>>(response);
        }

        public Task<ICollection<TweetDto>> SearchTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TwitterUserDto>> SearchUserJSON(string handle)
        {
            //// Does NOT work with Apllication-Only authentication
            //string url = string.Concat(
            //    this.baseUrl,
            //    "users/search.json?q=",
            //    handle,
            //    "&include_entities=false"
            //);

            //return await this.apiProvider.GetJSON(url);
            throw new NotImplementedException();
        }

        private async Task<string> GetRequestJson(string url)
        {
            return await this.apiProvider.GetJSON(url);
        }
    }
}