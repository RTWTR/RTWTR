using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RTWTR.MVC.Areas.Administration.Controllers.Abstractions
{
    // TODO: Fix this
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public abstract class AdminController : Controller
    {
    }
}
