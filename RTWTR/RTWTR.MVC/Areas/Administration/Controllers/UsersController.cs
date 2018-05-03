using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Areas.Administration.Controllers.Abstractions;
using RTWTR.MVC.Areas.Administration.Models;
using RTWTR.MVC.Models;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IMappingProvider mapper;
        private readonly IUserService userService;
        private readonly ITweetService tweetService;

        public UsersController(IMappingProvider mapper, IUserService userService, ITweetService tweetService)
        {
            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
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

                var model = new UserViewModel
                {
                    Id = user.Id,
                    TwitterId = user.TwitterId,
                    Email = user.Email,
                    IsDeleted = user.IsDeleted,
                    CreatedOn = user.CreatedOn,
                    Tweets = this.mapper.MapTo<ICollection<TweetViewModel>>(tweets)
                };

                return View(model);
            }
            catch
            {
                ViewData ["Error"] = email;

                return View();
            }
        }
    }
}