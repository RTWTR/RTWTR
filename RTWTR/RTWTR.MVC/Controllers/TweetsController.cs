using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;
using RTWTR.MVC.Models.TweetsViewModels;

namespace RTWTR.MVC.Controllers
{
    [Authorize]
    public class TweetsController : Controller
    {
        private ITwitterService twitterService;
        private readonly ITwitterUserService twitterUserService;
        private ITweetService tweetService;
        private IMappingProvider mapper;
        private UserManager<User> userManager;

        public TweetsController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            ITweetService tweetService,
            IMappingProvider mapper,
            UserManager<User> userManager)
        {
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
            this.twitterUserService = twitterUserService ?? throw new ArgumentNullException(nameof(twitterUserService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IActionResult ShowUserFavouriteTweets(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                userId = this.userManager.GetUserId(User);
            }

            var favourites = this.tweetService.GetUserFavourites(userId);

            var model = new FavouriteTweetsViewModel()
            {
                Tweets = this.mapper.MapTo<List<TweetViewModel>>(favourites)
            };

            model.Tweets.Select(x => { x.IsFavourite = true; return x; }).ToList();

            return View(model);
        }

        // TODO: cache this
        public async Task<IActionResult> ShowTwitterUserTimeline(string screenName)
        {
            try
            {
                var user = await this.GetTwitterUserDtoAsync(screenName);
                var timeline = await this.twitterService.GetUserTimelineAsync(user.ScreenName, 20);

                var model = new TwitterUserTimelineViewModel
                {
                    User = this.mapper.MapTo<TwitterUserViewModel>(user),
                    Timeline = this.mapper.MapTo<List<TweetViewModel>>(timeline)
                };

                ViewData["Title"] = model.User.ScreenName;

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToTweetFavourites(string tweetId)
        {
            try
            {
                var user = this.mapper.MapTo<UserDTO>(await this.userManager.GetUserAsync(User));

                this.tweetService.AddTweetToFavourites(
                    tweetId,
                    user
                );

                // TODO: Maybe delete?
                // var model = this.mapper.MapTo<TweetViewModel>(tweet);
                // model.IsFavourite = true;

                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromTweetFavourites(string tweetId)
        {
            try
            {
                // var tweet = await this.GetTweetDtoAsync(tweetId);
                var userId = this.userManager.GetUserId(User);

                this.tweetService.RemoveTweetFromFavourites(
                    tweetId,
                    userId
                );

                // var model = this.mapper.MapTo<TweetViewModel>(tweet);
                // model.IsFavourite = false;

                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> AddTweetFromUrl(string tweetUrl)
        // {
        //     throw new Exception();
        // }

        private async Task<TweetDto> GetTweetDtoAsync(string tweetId)
        {
            var model = this.tweetService.GetTweetById(tweetId);

            if (model.IsNull())
            {
                model = await this.twitterService.GetSingleTweetAsync(tweetId);
                this.tweetService.SaveTweet(model);
            }

            return model;
        }

        private async Task<TwitterUserDto> GetTwitterUserDtoAsync(string screenName)
        {
            var model = this.twitterUserService.GetTwitterUserByScreenName(screenName);

            if (model.IsNull())
            {
                model = await this.twitterService.GetSingleUserAsync(screenName);
                this.twitterUserService.SaveTwitterUser(model);
            }

            return model;
        }
    }
}
