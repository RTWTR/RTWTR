using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ITweetService tweetService;
        private readonly ITwitterService twitterService;

        public HomeController(IUserService userService, ITwitterService twitterService, ITweetService tweetService)
        {
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));

        }

        public IActionResult Index()
        {
            ViewData["UsersCount"] = this.userService.GetAllAndDeletedUsersCount();
            ViewData["TweetsCount"] = this.tweetService.GetAllAndDeletedTweetsCount();
            ViewData["RetweetsCount"] = this.tweetService.RetweetCount();
            return View(ViewData);
        }
    }
}