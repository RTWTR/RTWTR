using System;
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

namespace RTWTR.MVC.Controllers
{
    [Authorize]
    public class TweetsController : Controller
    {
        private ITwitterService twitterService;
        private ITweetService tweetService;
        private IMappingProvider mapper;
        private UserManager<User> userManager;

        public TweetsController(
            ITwitterService twitterService,
            ITweetService tweetService,
            IMappingProvider mapper,
            UserManager<User> userManager)
        {
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
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

            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToTweetFavourites(string tweetId)
        {
            try
            {
                // var tweet = await this.GetTweetDtoAsync(tweetId);
                var user = await this.userManager.GetUserAsync(User);

                this.tweetService.SaveTweetToFavourites(
                    tweetId,
                    user.Id
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
        public async Task<IActionResult> RemoveFromTweetFavourites(string tweetId)
        {
            try
            {
                var tweet = await this.GetTweetDtoAsync(tweetId);
                var user = await this.userManager.GetUserAsync(User);

                this.tweetService.DeleteTweetFromFavourites(
                    tweet.Id,
                    user.Id
                );

                var model = this.mapper.MapTo<TweetViewModel>(tweet);
                model.IsFavourite = false;

                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        private async Task<TweetDto> GetTweetDtoAsync(string tweetId)
        {
            var model = this.tweetService.GetTweetById(tweetId);

            if (model.IsNull())
            {
                model = await this.twitterService.GetSingleTweetAsync(tweetId);
                this.tweetService.AddTweet(model);
            }

            return model;
        }
    }
}
