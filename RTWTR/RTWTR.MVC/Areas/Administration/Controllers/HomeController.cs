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