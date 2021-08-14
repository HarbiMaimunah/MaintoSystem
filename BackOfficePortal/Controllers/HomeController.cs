using BackOfficePortal.Models;
using BackOfficePortal.ModelsLanguage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using FluentAssertions.Common;

namespace BackOfficePortal.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public IActionResult Index()
        {
            

            return View();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult ChangeLanguage(string culture)
        {
             Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                 CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                 new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
           

            return Redirect(Request.Headers["Referer"].ToString());
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        /*[HttpPost]
        public ActionResult SaveFile()
        {
            string path = Path.Combine(ConfigurationManager.AppSettings["StoragePath"], "Files");
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path);
                }
            }
            return View();
        }*/
        public IActionResult SaveFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostFile(IList<IFormFile> files)
        {
            
            // string path = Path.Combine(ConfigurationManager.AppSettings["StoragePath"], "Files");
            foreach (var file in files)
            {
                var filePath = Path.Combine("C:/Users/abc/Desktop/Summer training/MaintenanceManagementSystem/UploudedFiles", file.FileName);
                if (file.Length > 0)
                {
                    
                    var stream = new FileStream(filePath, FileMode.Append);
                    await file.CopyToAsync(stream);
                }
            }
            return View();
        }
    }
}
