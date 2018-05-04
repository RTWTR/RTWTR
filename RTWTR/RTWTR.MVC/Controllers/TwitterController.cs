using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class TwitterController : Controller
    {
        private readonly ITwitterService twitterService;
        private readonly ITwitterUserService twitterUserService;
        private readonly IMappingProvider mapper;

        public TwitterController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            IMappingProvider mapper
        )
        {
            this.twitterService = twitterService ??
                throw new ArgumentNullException(nameof(twitterService));
            this.twitterUserService = twitterUserService ??
                throw new ArgumentNullException(nameof(twitterUserService));
            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionResult> Search(string screenName)
        {
            try
            {
                var model = this.twitterUserService.GetTwitterUserByScreenName(screenName);

                if (model.IsNull())
                {
                    model = await this.twitterService.SearchUserAsync(screenName);
                    this.twitterUserService.SaveTwitterUser(model);
                }

                return Json(model);
            }
            catch
            {
                return Json("{{}}");
            }

            // if (model.IsNull()) 
            // {
            //     ViewData["Error"] = screenName;
            //     return View("FailedSearch");
            // }

            // var viewModel = mapper.MapTo<TwitterUserViewModel>(model);

            // ViewData["Title"] = viewModel.Name;

            // return View("Search", viewModel);
        }

        public async Task<IActionResult> ShowUser(string screenName)
        {
            var user = await this.twitterService.GetSingleUserAsync(screenName);

            if (user.IsNull())
            {
                ViewData ["Error"] = screenName;
                return View("FailedSearch");
            }

            var model = this.mapper.MapTo<TwitterUserViewModel>(user);

            ViewData ["Title"] = model.Name;

            return View(model);
        }

        public async Task<string> GetHTMLASync(string id)
        {
            return await this.twitterService.GetHTMLAsync(id);
        }
    }
}