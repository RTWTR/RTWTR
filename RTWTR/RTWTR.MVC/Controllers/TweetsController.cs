using System;
using System.Threading.Tasks;
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
    public class TweetsController : Controller
    {
        private ITwitterService twitterService;
        private ITweetService tweetService;
        private IMappingProvider mapper;
        private UserManager<User> userManager;

       
        // TODO: Fill controller
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToFavourites(string tweetId)
        {
            try
            {
                var tweet = await this.GetTweetDtoAsync(tweetId);
                var user = await this.userManager.GetUserAsync(User);

                this.tweetService.SaveTweetToFavourites(tweet.Id, user.Id);
                    

                var model = this.mapper.MapTo<TweetViewModel>(tweet);
                model.IsFavourite = true;

                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromFavourites(string tweetId)
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
            catch (Exception e)
            {
                throw e;

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
