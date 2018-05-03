using System;
using Microsoft.AspNetCore.Mvc;
using RTWTR.MVC.Areas.Administration.Controllers.Abstractions;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IUserService userService;
        private readonly ITwitterService twitterService;

        public HomeController(IUserService userService, ITwitterService twitterService)
        {
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            this.twitterService = twitterService ?? throw new ArgumentNullException(nameof(twitterService));
        }

        public IActionResult Index()
        {
            ViewData["UsersCount"] = this.userService.GetAllAndDeletedUsersCount();

            return View(ViewData);
        }
    }
}