using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.MVC.Models;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly ITwitterService service;
        private readonly IMappingProvider mapper;

        public ApiController(ITwitterService service, IMappingProvider mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<ActionResult> Search(string screenName)
        {
            var model = await this.service.SearchUserJSON(screenName);

            if (model == null) 
            {
                ViewData["Error"] = screenName;
                return View("FailedSearch");
            }

            var viewModel = mapper.MapTo<TwitterUserViewModel>(model);

            return View("Search", viewModel);
        }

        public async Task<TwitterUserDto> ShowUser(string screenName)
        {
            return await this.service.GetSingleUserJSON(screenName);
        }

        public async Task<string> GetHTML(string id)
        {
            return await this.service.GetHTML(id);
        }
    }
}