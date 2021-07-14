using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OLXWebApp.Models;

namespace OLXWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("moderator"))
            {
                return RedirectToAction("Manager", "Accounts");
            }
            else
            {
                return RedirectToAction("Accounts", "User");
            }
            return View();
        }


        [Authorize(Roles = "moderator")]
        public IActionResult Privacy()
        {
            return View();
        }

        //[Authorize(Roles = "moderator")]
        //public IActionResult About()
        //{
        //    return Content("Вход только для администратора");
        //}


        //[AllowAnonymous]
        //public IActionResult Me()
        //{
        //    return Content("Вход для всех");
        //}


        //[Authorize(Roles = "admin, moderator")]
        //public IActionResult Success()
        //{
        //    return Content("Вход только для администратора");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
