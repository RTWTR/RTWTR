using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
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

        public async Task<TwitterUserDto> SearchUserJSON(string handle)
        {
            string url = string.Concat(this.baseUrl, 
                $"users/lookup.json?screen_name={handle}");

            var responseAsString = await this.GetRequestJson(url);

            JArray response = jsonProvider.ParseToJArray(responseAsString);

            if (response[0].ToString() == "errors")
            {
                return new TwitterUserDto();
            }

            return this.jsonProvider.DeserializeObject<TwitterUserDto>(response[0].ToString());

        }

        public async Task<string> GetHTML(string id)
        {
            string url = string.Concat(
                "https://publish.twitter.com/oembed?",
                "url=https://twitter.com/Interior/status/",
                id
            );

            var response = await this.GetRequestJson(url);

            var parsedResponse = JObject.Parse(response);

            return parsedResponse["html"].ToString();
        }

        private async Task<string> GetRequestJson(string url)
        {
            return await this.apiProvider.GetJSON(url);
        }
    }
}