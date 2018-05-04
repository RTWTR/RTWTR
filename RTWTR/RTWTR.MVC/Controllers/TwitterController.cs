using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Contracts;
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
        private readonly IJsonProvider jsonProvider;
        private readonly IMappingProvider mapper;

        public TwitterController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            IJsonProvider jsonProvider,
            IMappingProvider mapper
        )
        {
            this.twitterService = twitterService
                ??
                throw new ArgumentNullException(nameof(twitterService));
            this.twitterUserService = twitterUserService
                ??
                throw new ArgumentNullException(nameof(twitterUserService));
            this.jsonProvider = jsonProvider
                ??
                throw new ArgumentNullException(nameof(jsonProvider));
            this.mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Search(string screenName)
        {
            var user = await this.GetUserDtoAsync(screenName);

            var model = this.mapper.MapTo<TwitterUserViewModel>(user);

            return View(model);
        }

        public async Task<IActionResult> ShowUser(string screenName)
        {
            // var user = await this.twitterService.GetSingleUserAsync(screenName);

            // if (user.IsNull())
            // {
            //     ViewData ["Error"] = screenName;
            //     return View("FailedSearch");
            // }

            // var model = this.mapper.MapTo<TwitterUserViewModel>(user);

            // ViewData ["Title"] = model.Name;

            // return View(model);
            try
            {
                var user = await this.GetUserDtoAsync(screenName);

                var model = this.mapper.MapTo<TwitterUserViewModel>(user);

                ViewData ["Title"] = model.Name;

                return View("Search", model);
            }
            catch
            {
                ViewData ["Error"] = screenName;
                return View("FailedSearch");
            }
        }

        public async Task<string> GetHTMLASync(string id)
        {
            return await this.twitterService.GetHTMLAsync(id);
        }

        private async Task<TwitterUserDto> GetUserDtoAsync(string screenName)
        {
            var model = this.twitterUserService.GetTwitterUserByScreenName(screenName);

            if (model.IsNull())
            {
                model = await this.twitterService.SearchUserAsync(screenName);
                this.twitterUserService.SaveTwitterUser(model);
            }

            return model;
        }
    }
}