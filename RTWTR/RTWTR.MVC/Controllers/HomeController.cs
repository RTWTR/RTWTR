using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWTR.MVC.ViewModels;

namespace RTWTR.MVC.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.Validate();
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
