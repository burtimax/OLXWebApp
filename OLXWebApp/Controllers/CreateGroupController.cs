using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXWebApp.Controllers
{
    public class CreateGroupController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public CreateGroupController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult AddGroup()
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;
                var GroupName = HttpContext.Request.Form["GroupName"];
                IFormFile excelFile = null;

                foreach (var file in files)
                {
                    //Getting FileName
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    //Getting file Extension
                    var FileExtension = Path.GetExtension(fileName);

                    if (FileExtension == ".xls" || FileExtension == ".xlsx")
                    {
                        excelFile = file;
                    }
                }

                if(excelFile == null)
                {
                    return Content("Excel File Is NULL!");
                }


                foreach (var file in files)
                {
                    if(file == excelFile)
                    {
                        continue;
                    }

                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = GroupName + "_" + myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "data\\imgs") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "demoImages/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }


            }

            return RedirectToAction("Manager", "Accounts");
        }
    }
}
