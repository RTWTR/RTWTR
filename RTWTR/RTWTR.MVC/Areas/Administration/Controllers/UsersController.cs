using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Data.Models;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Areas.Administration.Models;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IUserService userService;
        private readonly ITweetService tweetService;
        private IFavouriteUserService favouriteService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(
            IMappingProvider mapper,
            IUserService userService,
            ITweetService tweetService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IFavouriteUserService favouriteService
        )
        {
            this.mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            this.favouriteService = favouriteService
                          ??
                          throw new ArgumentNullException(nameof(favouriteService));
            this.userService = userService
                ??
                throw new ArgumentNullException(nameof(userService));
            this.tweetService = tweetService
                ??
                throw new ArgumentNullException(nameof(tweetService));
            this.userManager = userManager
                ??
                throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager
                ??
                throw new ArgumentNullException(nameof(roleManager));
        }

        [Route("Administration/Users/All/")]
        public IActionResult ShowAllUsers()
        {
            var users = this.userService.GetAllAndDeletedUsers();

            var model = new ShowAllUsersModel
            {
                Users = this.mapper.MapTo<ICollection<MinifiedUserViewModel>>(users)
            };

            return View(model);
        }

        [Route("Administration/Users/Details/")]
        public IActionResult ShowUserDetails(string email)
        {
            try
            {
                var user = this.userService.GetUserByEmail(email);
                var tweets = this.tweetService.GetUserFavourites(user.Id);
                var favourites = this.favouriteService.GetUserFavourites(user.Id);
                

                var model = new UserViewModel();

                model.User = this.mapper.MapTo<MinifiedUserViewModel>(user);
                model.Favourites = this.mapper.MapTo<ICollection<TwitterUserViewModel>>(favourites);
                model.Tweets = new List<TweetViewModel>();
                foreach (var tweet in tweets)
                {
                    var tweetToAdd = new TweetViewModel()
                    {
                        Id = tweet.Id,
                        TwitterId = tweet.TwitterId,
                        CreatedAt = tweet.CreatedAt,
                        Text = tweet.Text,
                        TwitterUserName = tweet.TwitterUser.ScreenName,
                        TwitterUserProfileImageUrl = tweet.TwitterUser.ProfileImageUrl,

                    };

                    model.Tweets.Add(tweetToAdd);
                }
                return View(model);
            }
            catch
            {
                ViewData ["Error"] = email;

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                this.userService.DeleteUser(userId);

                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}