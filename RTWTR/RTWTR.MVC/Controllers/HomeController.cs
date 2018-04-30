using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.MVC.ViewModels;
using RTWTR.Service.Twitter;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITwitterService twitterService;
        private readonly IMappingProvider mapper;

        public HomeController(ITwitterService twitterService, IMappingProvider mapper)
        {
            this.twitterService = twitterService;
            this.mapper = mapper;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.Validate();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Timeline(string twitterUserId)
        {
            int tweetsCount = 30;

            var tweets =  await twitterService.GetUserTimeline(twitterUserId, tweetsCount);

            var model = new TimelineViewModel
            {
                Tweets = tweets.Select(t=> this.mapper.MapTo<TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        private bool IsLoggedIn()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        private IActionResult Validate()
        {
            //TODO: Remove Comments when finished developing _layout
            //if (!this.IsLoggedIn())
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            return View();
        }
    }
}
