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
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache memoryCache;

        public TweetsController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            IFavouriteUserService favouriteUserService,
            ITweetService tweetService,
            IMappingProvider mapper,
            UserManager<User> userManager,
            IMemoryCache memoryCache)
        {
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
            this.twitterUserService = twitterUserService ?? throw new ArgumentNullException(nameof(twitterUserService));
            this.favouriteUserService = favouriteUserService ?? throw new ArgumentNullException(nameof(favouriteUserService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
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

        public async Task<IActionResult> ShowTwitterUserTimeline(string screenName)
        {
            try
            {
                // application user ID
                var userId = this.userManager.GetUserId(User);

                var twitterUser = await this.GetTwitterUserDtoAsync(screenName);
                var timeline = await this.GetTimelineCache(twitterUser.ScreenName);

                var model = new TwitterUserTimelineViewModel
                {
                    User = this.mapper.MapTo<TwitterUserViewModel>(twitterUser),
                    Timeline = this.mapper.MapTo<List<TweetViewModel>>(timeline)
                };

                model.User.IsFavourite = (this.favouriteUserService.IsFavourite(userId, twitterUser.Id));

                // TODO: Find a faster way to do this
                foreach (var tweet in model.Timeline)
                {
                    tweet.IsFavourite = this.tweetService.IsFavourite(tweet.Id, userId);
                }

                return View(model);
            }
            catch
            {
                ViewData["Error"] = screenName;
                return View("FailedSearch", "Users");
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

                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
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

        private async Task EnsureTweetCreated(string tweetId)
        {
            if (!this.tweetService.Exists(tweetId))
            {
                var tweetToSave = await this.twitterService
                    .GetSingleTweetAsync(tweetId);

                var twitterUser = this.twitterUserService
                    .GetTwitterUserByScreenName(tweetToSave.TwitterUser.ScreenName);

                this.tweetService.SaveTweet(tweetToSave, twitterUser);
            }
        }

        private async Task<ICollection<TweetDto>> GetTimelineCache(string screenName)
        {
            ICollection<TweetDto> timeline;

            // Look for screenName's timeline.
            if (!memoryCache.TryGetValue(screenName, out timeline))
            {
                // Get the cache data.
                timeline = await this.twitterService.GetUserTimelineAsync(screenName, 20);

                // Set cache for 90 seconds.
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(90));

                // Save data.
                memoryCache.Set(screenName, timeline, cacheOptions);
            }

            return timeline;
        }

        public IActionResult Retweet(string tweetId)
        {
            this.tweetService.Retweet(tweetId);

            return Ok();
        }

    }
}
