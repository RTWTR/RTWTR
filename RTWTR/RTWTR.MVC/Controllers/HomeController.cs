using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITwitterService twitterService;
        private IUserService userService;
        private UserManager<User> userManager;
        private IMappingProvider mapper;

        public HomeController(
            ITwitterService twitterService,
            IUserService userService,
            UserManager<User> userManager,
            IMappingProvider mapper)
        {
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(User);

            if (this.userService.IsDeleted(userId))
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("ShowUserFavouriteTweets", "Tweets");
        }

        // public async Task<IActionResult> Timeline(string twitterUserId)
        // {
        //     int tweetsCount = 30;

        //     var tweets =  await twitterService.GetUserTimelineAsync(twitterUserId, tweetsCount);

        //     var model = new TimelineViewModel
        //     {
        //         Tweets = tweets.Select(t=> this.mapper.MapTo<TweetViewModel>(t)).ToList()
        //     };

        //     return View(model);
        // }

        public IActionResult Error()
        {
            return View();
        }

        // private bool IsLoggedIn()
        // {
        //     return HttpContext.User.Identity.IsAuthenticated;
        // }

        // private IActionResult Validate()
        // {
        //     if (!this.IsLoggedIn())
        //     {
        //        return RedirectToAction("Login", "Account");
        //     }

        //     return View();
        // }
    }
}
