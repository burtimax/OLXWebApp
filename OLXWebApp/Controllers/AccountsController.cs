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
            
            Role userRole = db.Role.FirstOrDefault(r => r.Name == "user");
            List<User> users = db.User.Where(u=>u.RoleId == userRole.Id).ToList();
            ViewBag.Users = users;

            ViewData["Title"] = "Авторизация";
            ViewData["PageTitle"] = "Страница авторизации";
            return View();
        }


        [HttpPost]
        public IActionResult AddAccount(LoginModel model)
        {
            Role userRole = db.Role.FirstOrDefault(r => r.Name == "user");
            User newUser = new User
            {
                Login = model.Login,
                Password = model.Password,
                RoleId = userRole.Id,
            };
            db.User.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Manager", "Accounts");
        }

        [HttpGet]
        public IActionResult DeleteAccount(int id)
        {
            User userForDelete = db.User.Where(u => u.Id == id).FirstOrDefault();
            if (userForDelete != null)
            {
                db.User.Remove(userForDelete);
            }
            
            db.SaveChanges();
            return RedirectToAction("Manager", "Accounts");
        }

    }
}