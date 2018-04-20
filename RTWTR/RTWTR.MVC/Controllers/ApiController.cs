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

        public string Search(string name)
        {
            return this.service.SearchUserJSON(name);
        }
    }
}