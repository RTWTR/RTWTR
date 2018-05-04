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

        public async Task<string> GetSingleUserJSONAsync(string screenName)
        {
            string url = string.Concat(
                this.baseUrl,
                "users/show.json?screen_name=",
                screenName,
                "&include_entities=false"
            );
            
            var response = await this.GetRequestJsonAsync(url);

            if (response.IsNullOrWhitespace())
            {
                return string.Empty;
            }

            return response;
        }

        public async Task<TwitterUserDto> GetSingleUserAsync(string screenName)
        {
            var response = await this.GetSingleUserJSONAsync(screenName);

            if (response.IsNullOrWhitespace())
            {
                return new TwitterUserDto();
            }

            return this.jsonProvider.DeserializeObject<TwitterUserDto>(response);
        }

        public async Task<string> GetSingleTweetJSONAsync(string tweetId)
        {
            string url = string.Concat(
                this.baseUrl,
                "statuses/show.json?id=",
                tweetId
            );

            var response = await this.GetRequestJsonAsync(url);

            if (response.IsNullOrWhitespace())
            {
                return string.Empty;
            }

            return response;
        }

        public async Task<TweetDto> GetSingleTweetAsync(string tweetId)
        {
            var response = await this.GetSingleTweetJSONAsync(tweetId);

            if (response.IsNullOrWhitespace())
            {
                return new TweetDto();
            }

            return this.jsonProvider.DeserializeObject<TweetDto>(response);
        }

        public async Task<string> GetUserTimelineJSONAsync(string screenName, int tweetsCount)
        {
            string url = string.Concat(
                this.baseUrl,
                $"statuses/user_timeline.json?screen_name=",
                screenName,
                "&count=",
                tweetsCount
            );

            var response = await this.GetRequestJsonAsync(url);

            if (response.IsNullOrWhitespace())
            {
                return string.Empty;
            }

            return response;
        }

        public async Task<ICollection<TweetDto>> GetUserTimelineAsync(string screenName, int tweetsCount)
        {
            var response = await this.GetUserTimelineJSONAsync(screenName, tweetsCount);

            if (response.IsNullOrWhitespace())
            {
                return new List<TweetDto>();
            }

            return this.jsonProvider.DeserializeObject<List<TweetDto>>(response);
        }

        // TODO: Check this later
        public async Task<string> SearchUserJSONAsync(string handle)
        {
            string url = string.Concat(this.baseUrl, 
                $"users/lookup.json?screen_name={handle}");

            var responseAsString = await this.GetRequestJsonAsync(url);

            if (responseAsString.IsNullOrWhitespace())
            {
                return string.Empty;
            }

            JArray response = jsonProvider.ParseToJArray(responseAsString);

            return response[0].ToString();
        }

        public async Task<TwitterUserDto> SearchUserAsync(string handle)
        {
            var responseAsString = await this.SearchUserJSONAsync(handle);

            if (responseAsString == string.Empty)
            {
                return null;
            }

            JArray response = jsonProvider.ParseToJArray(responseAsString);

            return this.jsonProvider.DeserializeObject<TwitterUserDto>(response[0].ToString());
        }

        public async Task<string> GetHTMLAsync(string id)
        {
            string url = string.Concat(
                "https://publish.twitter.com/oembed?",
                "url=https://twitter.com/Interior/status/",
                id
            );

            var response = await this.GetRequestJsonAsync(url);

            var parsedResponse = JObject.Parse(response);

            return parsedResponse["html"].ToString();
        }

        private async Task<string> GetRequestJsonAsync(string url)
        {
            return await this.apiProvider.GetJSON(url);
        }
    }
}