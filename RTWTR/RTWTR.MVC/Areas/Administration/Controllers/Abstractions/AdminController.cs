using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RTWTR.MVC.Areas.Administration.Controllers.Abstractions
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public abstract class AdminController : Controller
    {
    }
}
