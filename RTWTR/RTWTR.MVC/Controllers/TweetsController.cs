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
        private readonly IFavouriteUserService favouriteUserService;
        private ITweetService tweetService;
        private IMappingProvider mapper;
        private UserManager<User> userManager;

        public TweetsController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            IFavouriteUserService favouriteUserService,
            ITweetService tweetService,
            IMappingProvider mapper,
            UserManager<User> userManager)
        {
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
            this.twitterUserService = twitterUserService ?? throw new ArgumentNullException(nameof(twitterUserService));
            this.favouriteUserService = favouriteUserService ?? throw new ArgumentNullException(nameof(favouriteUserService));
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
                // application user ID
                var userId = this.userManager.GetUserId(User);

                var user = await this.GetTwitterUserDtoAsync(screenName);
                var timeline = await this.twitterService.GetUserTimelineAsync(user.ScreenName, 20);

                var model = new TwitterUserTimelineViewModel
                {
                    User = this.mapper.MapTo<TwitterUserViewModel>(user),
                    Timeline = this.mapper.MapTo<List<TweetViewModel>>(timeline)
                };

                model.User.IsFavourite = 
                    (this.favouriteUserService.IsFavourite(userId, user.Id))
                    &&
                    (!this.favouriteUserService.IsDeleted(userId, user.Id));

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
        public async Task<IActionResult> AddTweetToFavourites(string tweetId)
        {
            try
            {
                await this.EnsureTweetCreated(tweetId);
                var user = await this.userManager.GetUserAsync(User);
                var mappedUser = this.mapper.MapTo<UserDTO>(user);

                this.tweetService.AddTweetToFavourites(
                    tweetId,
                    mappedUser
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
        public async Task<IActionResult> RemoveTweetFromFavourites(string tweetId)
        {
            try
            {
                await this.EnsureTweetCreated(tweetId);
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

        private async Task EnsureTweetCreated(string tweetId)
        {
            if (!this.tweetService.Exists(tweetId))
            {
                var tweetToSave = await this.twitterService
                    .GetSingleTweetAsync(tweetId);

                this.tweetService.SaveTweet(tweetToSave);
            }
        }
    }
}
