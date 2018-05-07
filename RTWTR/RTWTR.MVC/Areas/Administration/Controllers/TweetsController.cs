using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Areas.Administration.Models;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class TweetsController : Controller
    {
        private ITweetService tweetService;
        private IMappingProvider mapper;


        public TweetsController(ITweetService tweetService, IMappingProvider mapper)
        {
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        [Route("Administration/Tweets/All/")]
        public IActionResult ShowAllTweets()
        {
            var tweets = this.tweetService.GetAllTweets();

            var model = new ShowAllTweetsModel()
            {
                Tweets = new List<TweetViewModel>()
            };

            foreach (var tweet in tweets)
            {
                var tweetToAdd = new TweetViewModel()
                {
                    Id = tweet.Id,
                    TwitterId = tweet.TwitterId,
                    CreatedAt = tweet.CreatedAt,
                    Text = tweet.Text,
                    TwitterUserName = tweet.TwitterUser.ScreenName,
                    TwitterUserProfileImageUrl = tweet.TwitterUser.ProfileImageUrl,
                    
                };

                model.Tweets.Add(tweetToAdd);
            }
            

            return View(model);
        }

        [Route("Administration/Tweets/")]
        public IActionResult ShowTweetDetails(string tweetId)
        {
            var tweet = this.tweetService.GetTweetById(tweetId);

            var model = new TweetViewModel()
            {
                Id = tweet.Id,
                TwitterId = tweet.TwitterId,
                CreatedAt = tweet.CreatedAt,
                Text = tweet.Text,
                TwitterUserName = tweet.TwitterUser.ScreenName,
                TwitterUserProfileImageUrl = tweet.TwitterUser.ProfileImageUrl
            };

            return View(model);
        }


    }
}