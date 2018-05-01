using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Tweet> tweets;
        private readonly IMemoryCache cache;

        public HomeController(IRepository<User> users, IRepository<Tweet> tweets, IMemoryCache cache)
        {
            this.users = users ??
                throw new ArgumentNullException(nameof(users));
            this.tweets = tweets ??
                throw new ArgumentNullException(nameof(tweets));
            this.cache = cache ?? 
            throw new ArgumentNullException(nameof(cache));
        }

        public IActionResult Index()
        {
            int activeUsers;
            int allTweets;
            int deletedTweets;

            // Store In-Memory for 3 minutes
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

            // Get active users
            if (!this.cache.TryGetValue("activeUsers", out activeUsers))
            {
                activeUsers = this.users.All.Count();

                cache.Set("activeUsers", activeUsers, cacheOptions);
            }
            ViewData ["activeUsers"] = activeUsers;

            // Get all tweets
            if (!this.cache.TryGetValue("allTweets", out allTweets))
            {
                allTweets = this.tweets.All.Count();

                cache.Set("allTweets", allTweets, cacheOptions);
            }
            ViewData ["allTweets"] = allTweets;

            // Get deleted tweets
            if (!this.cache.TryGetValue("deletedTweets", out deletedTweets))
            {
                deletedTweets = this.tweets.All.Count();

                cache.Set("deletedTweets", deletedTweets, cacheOptions);
            }
            ViewData ["deletedTweets"] = deletedTweets;

            return View();
        }
    }
}