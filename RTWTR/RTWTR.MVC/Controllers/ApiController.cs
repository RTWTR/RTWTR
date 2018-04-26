using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<string> Search(string handle)
        {
            return await this.service.SearchUserJSON(handle);
        }

        public async Task<string> ShowUser(string screen_name)
        {
            return await this.service.GetSingleUserJSON(screen_name);
        }
    }
}