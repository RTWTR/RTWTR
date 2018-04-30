using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.DTO;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly ITwitterService service;

        public ApiController(ITwitterService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ICollection<TwitterUserDto>> Search(string handle)
        {
            return await this.service.SearchUserJSON(handle);
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