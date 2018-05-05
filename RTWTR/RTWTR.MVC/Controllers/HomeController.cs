using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Twitter;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITwitterService twitterService;
        private readonly IMappingProvider mapper;

        public HomeController(ITwitterService twitterService, IMappingProvider mapper)
        {
            this.twitterService = twitterService;
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Timeline(string twitterUserId)
        {
            int tweetsCount = 30;

            var tweets =  await twitterService.GetUserTimelineAsync(twitterUserId, tweetsCount);

            var model = new TimelineViewModel
            {
                Tweets = tweets.Select(t=> this.mapper.MapTo<TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool IsLoggedIn()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        private IActionResult Validate()
        {
            if (!this.IsLoggedIn())
            {
               return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
