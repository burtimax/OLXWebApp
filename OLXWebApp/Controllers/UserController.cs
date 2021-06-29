using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLXWebApp.Database.Context;
using OLXWebApp.Database.DbModels;
using OLXWebApp.Models;

namespace OLXWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationContext db;

        public UserController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Accounts()
        {
            User user = db.User.Where(u => u.Login == User.Identity.Name).FirstOrDefault();
            var accounts = db.OLXAccount.Where(a => a.UserOwnerId == user.Id).ToList();
            ViewBag.Accounts = accounts;
            return View();
        }

        public IActionResult AddOLXAccounts(OLXAccountModel account)
        {
            User user = db.User.Where(u => u.Login == User.Identity.Name).FirstOrDefault();

            if (db.OLXAccount.Where(a => a.Login == account.Login).FirstOrDefault() == null)
            {
                OLXAccount newAccount = new OLXAccount()
                {
                    Login = account.Login,
                    Password = account.Password,
                    Name = account.Name,
                    Phone = account.Phone,
                    UserOwnerId = user.Id,
                };
                db.OLXAccount.Add(newAccount);
                db.SaveChanges();
            }

            return RedirectToAction("Accounts", "User");
        }


        [HttpGet]
        public IActionResult DeleteOLXAccount(int id)
        {
            OLXAccount delAcc = db.OLXAccount.Where(u => u.Id == id).FirstOrDefault();
            if (delAcc != null)
            {
                db.OLXAccount.Remove(delAcc);
            }

            db.SaveChanges();
            return RedirectToAction("Accounts", "User");
        }

    }
}