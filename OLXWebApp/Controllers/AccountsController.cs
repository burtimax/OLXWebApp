using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLXWebApp.Database.Context;
using OLXWebApp.Database.DbModels;
using OLXWebApp.Models;

namespace OLXWebApp.Controllers
{
    [Authorize(Roles = "moderator")]
    public class AccountsController : Controller
    {
        private ApplicationContext db;

        public AccountsController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Manager()
        {

            Role userRole = db.Methods.GetRole_USER();
            List<User> users = db.Methods.GetAllUsersByRole(userRole);
            ViewBag.Users = users;

            ViewData["Title"] = "Авторизация";
            ViewData["PageTitle"] = "Страница авторизации";
            return View();
        }


        [HttpPost]
        public IActionResult AddAccount(LoginModel model)
        {
            Role userRole = db.Methods.GetRole_USER();
            db.Methods.AddUserByLoginModel(model, userRole);
            return RedirectToAction("Manager", "Accounts");
        }

        [HttpGet]
        public IActionResult DeleteAccount(int id)
        {
            User userForDelete = db.Methods.GetUserById(id);
            if (userForDelete != null)
            {
                db.Methods.DeleteUser(userForDelete);
            }
            
            return RedirectToAction("Manager", "Accounts");
        }

    }
}