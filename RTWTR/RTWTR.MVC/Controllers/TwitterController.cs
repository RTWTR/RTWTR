using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.Infrastructure;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class TwitterController : Controller
    {
        private readonly ITwitterService service;
        private readonly IMappingProvider mapper;

        public TwitterController(ITwitterService service, IMappingProvider mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionResult> Search(string screenName)
        {
            var model = await this.service.SearchUserJSON(screenName);

            return Json(model);

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
            var user = await this.service.GetSingleUserJSON(screenName);

            if (user.IsNull())
            {
                ViewData["Error"] = screenName;
                return View("FailedSearch");
            }

            var model = this.mapper.MapTo<TwitterUserViewModel>(user);

            ViewData["Title"] = model.Name;

            return View(model);
        }

        public async Task<string> GetHTML(string id)
        {
            return await this.service.GetHTML(id);
        }
    }
}