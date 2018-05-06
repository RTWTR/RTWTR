using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ITwitterService twitterService;
        private readonly ITwitterUserService twitterUserService;
        private readonly IFavouriteUserService favouriteUserService;
        private readonly UserManager<User> userManager;
        private readonly IJsonProvider jsonProvider;
        private readonly IMappingProvider mapper;

        public UsersController(
            ITwitterService twitterService,
            ITwitterUserService twitterUserService,
            IFavouriteUserService favouriteUserService,
            UserManager<User> userManager,
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
            this.favouriteUserService = favouriteUserService
                ??
                throw new ArgumentNullException(nameof(favouriteUserService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.jsonProvider = jsonProvider
                ??
                throw new ArgumentNullException(nameof(jsonProvider));
            this.mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Search(string screenName)
        {
            try
            {
                var twitterUser = await this.GetTwitterUserDtoAsync(screenName);
                var userId = this.userManager.GetUserId(User);

                var model = this.mapper.MapTo<TwitterUserViewModel>(twitterUser);
                model.IsFavourite = this.IsFavourite(userId, twitterUser.Id);

                return View(model);
            }
            catch
            {
                ViewData["Error"] = screenName;
                return View("FailedSearch");
            }
        }

        public async Task<IActionResult> ShowUser(string screenName)
        {
            try
            {
                var twitterUser = await this.GetTwitterUserDtoAsync(screenName);
                var userId = this.userManager.GetUserId(User);

                var model = this.mapper.MapTo<TwitterUserViewModel>(twitterUser);
                model.IsFavourite = this.IsFavourite(userId, twitterUser.Id);

                ViewData ["Title"] = model.Name;

                return View(model);
            }
            catch
            {
                ViewData ["Error"] = screenName;
                return View("FailedSearch");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToFavourites(string screenName)
        {
            try
            {
                var twitterUser = await this.GetTwitterUserDtoAsync(screenName);
                var user = this.mapper.MapTo<UserDTO>(await this.userManager.GetUserAsync(User));

                this.favouriteUserService.AddTwitterUserToFavourites(
                    user,
                    twitterUser
                );

                var model = this.mapper.MapTo<TwitterUserViewModel>(twitterUser);
                model.IsFavourite = true;

                // return RedirectToAction("ShowUser", model);

                // Will return this when it uses AJAX
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromFavourites(string screenName)
        {
            try
            {
                var twitterUser = await this.GetTwitterUserDtoAsync(screenName);
                var user = this.mapper.MapTo<UserDTO>(await this.userManager.GetUserAsync(User));

                this.favouriteUserService.RemoveTwitterUserFromFavourites(
                    user,
                    twitterUser
                );

                var model = this.mapper.MapTo<TwitterUserViewModel>(twitterUser);
                model.IsFavourite = false;

                // return RedirectToAction("ShowUser", model);

                // Will return this when it uses AJAX
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        public IActionResult ShowUserFavourites(string userId)
        {
            if (userId.IsNullOrWhitespace())
            {
                userId = this.userManager.GetUserId(User);
            }

            var favourites = this.favouriteUserService.GetUserFavourites(userId);

            var model = new FavouriteUsersViewModel
            {
                TwitterUsers = this.mapper.MapTo<List<TwitterUserViewModel>>(favourites)
            };

            return View(model);
        }

        public async Task<string> GetHTMLASync(string id)
        {
            return await this.twitterService.GetHTMLAsync(id);
        }

        private async Task<TwitterUserDto> GetTwitterUserDtoAsync(string screenName)
        {
            var model = this.twitterUserService.GetTwitterUserByScreenName(screenName);

            if (model.IsNull())
            {
                model = await this.twitterService.SearchUserAsync(screenName);
                this.twitterUserService.SaveTwitterUser(model);
            }

            return model;
        }

        private bool IsFavourite(string userId, string twitterUserId)
        {
            return (this.favouriteUserService.IsFavourite(userId, twitterUserId)) 
            &&
            !(this.favouriteUserService.IsDeleted(userId, twitterUserId));
        }
    }
}