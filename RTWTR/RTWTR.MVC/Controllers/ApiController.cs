using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.MVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly IApiProvider apiProvider;
        
        public ApiController(IApiProvider apiProvider)
        {
            this.apiProvider = apiProvider ?? throw new ArgumentNullException(nameof(apiProvider));
        }

        public string Search(string name)
        {
            return this.apiProvider.SearchUserJSON(name);
        }
    }
}