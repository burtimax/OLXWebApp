using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiOlx.Classes.Models;
using ApiOlx.Classes.Models.Categories;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLXWebApp.Database.Context;
using OLXWebApp.Database.DbModels;
using OLXWebApp.Models;

namespace OLXWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
       
        public static string DEV_URL = "https://developer.olx.ua";
        public static string BASE_URL = "https://www.olx.ua";
        private string GET_CATEGORIES = "/api/partner/categories?access_token=9adc731d600281b635d6072d8fda9af87f0ee5f7&expires_in=15685&token_type=bearer&scope=v2 read write&refresh_token=7f5f7cd09109bbae8c496c0144d64f4faecdc9d1";
        private string GET_ATTRIBUTES = "/attributes";
        private string GET_CITIES = "/api/partner/cities";


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

        public IActionResult Settings()
        {
            User user = db.User.Where(u => u.Login == User.Identity.Name).FirstOrDefault();
            var accounts = db.OLXAccount.Where(a => a.UserOwnerId == user.Id).ToList();
            ViewBag.Accounts = accounts;
            ViewBag.User = user;
            return View();
        }

        public async Task<Categories> GetCategories(AOuthRequest paramsApi)
        {
            HttpClient client = new HttpClient(); //GetHttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            HttpRequestMessage requestCategorius = new HttpRequestMessage(HttpMethod.Get, GET_CATEGORIES);
            requestCategorius.Headers.Add("Version", $"Version: 2.0");
            requestCategorius.Headers.Authorization = AuthenticationHeaderValue.Parse($"Bearer 9adc731d600281b635d6072d8fda9af87f0ee5f7");
            var responseCategorius = await client.SendAsync(requestCategorius);
            if (responseCategorius.IsSuccessStatusCode)
            {
                var jsonContent = await responseCategorius.Content.ReadAsStringAsync();
                var objectCategory = JsonConvert.DeserializeObject<Categories>(jsonContent);
                return objectCategory;
            }
            return null;
        }

        public IActionResult AddOLXAccount(OLXAccountModel account)
        {
            User user = db.Methods.GetUserByLogin(User.Identity.Name);

            db.Methods.AddOLXAccountIfNeed(account, user);

            return RedirectToAction("Accounts", "User");
        }

        public IActionResult AddOLXAccountsFromExcelFile(IFormFile file)
        {
            if (file.FileName.EndsWith(".xls") == false && file.FileName.EndsWith(".xlsx") == false) 
            {
                return RedirectToAction("Accounts", "User");
            }

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);

            using (var reader = ExcelReaderFactory.CreateReader(ms))
            {
                User user = db.Methods.GetUserByLogin(User.Identity.Name);
                reader.Read(); // Заголовок прочитываем
                while (reader.Read()) //Each row of the file
                {
                    OLXAccountModel model = new OLXAccountModel()
                    {
                        Login = reader.GetValue(0).ToString(),
                        Password = reader.GetValue(1).ToString(),
                        Name = reader.GetValue(2).ToString(),
                        Phone = reader.GetValue(3).ToString(),

                    };

                    db.Methods.AddOLXAccountIfNeed(model, user);
                }
            }

            return RedirectToAction("Accounts", "User");
        }


        [HttpGet]
        public IActionResult DeleteOLXAccount(int id)
        {
            OLXAccount delAcc = db.Methods.GetOLXAccountById(id);
            if (delAcc != null)
            {
               db.Methods.DeleteOLXAccount(delAcc);
            }

            return RedirectToAction("Accounts", "User");
        }

    }
}