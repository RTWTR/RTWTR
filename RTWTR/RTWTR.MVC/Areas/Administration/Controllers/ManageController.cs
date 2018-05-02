using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.MVC.Areas.Administration.Models;
using RTWTR.Service.Data.Contracts;

namespace RTWTR.MVC.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class ManageController : Controller
    {
        private readonly IUserService userService;

        public ManageController(IUserService userService)
        {
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public IActionResult ShowAllUsers()
        {
            var model = new ShowAllUsersModel
            {
                Users = this.userService.GetAllAndDeletedUsers().Take(20).ToList()
            };

            return View(model);
        }

        public IActionResult SearchUser(string email)
        {
            try
            {
                UserDTO model = this.userService.GetUserByEmail(email);

                return View("ShowUser", model);
            }
            catch
            {
                ViewData["ErrorMessage"] = "Oops, User wasn't found";

                return View(ViewData);
            }
        }

        public IActionResult ShowUser()
        {
            return View();
        }
    }
}