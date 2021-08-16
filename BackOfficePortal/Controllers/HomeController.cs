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
using BackOfficePortal.Filters;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace BackOfficePortal.Controllers
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        public static string baseUrl = "http://localhost:16982/api/BackOfficeEntry/";

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;

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

        public IActionResult SaveFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostFile(IList<IFormFile> files)
        {
            string path = _config.GetValue<string>("StoragePath");
            foreach (var file in files)
            {
                var filePath = Path.Combine(path, file.FileName);
                if (file.Length > 0)
                {
                    var stream = new FileStream(filePath, FileMode.Append);
                    await file.CopyToAsync(stream);
                }
            }
            return View();
        }

        public IActionResult Signin()
        {
            return View();
        }

        public async Task<IActionResult> Login(Login Login)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(Login), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(baseUrl + "Login", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        TempData["LoginError"] = error;
                        return RedirectToAction("Signin");
                    }

                    HttpContext.Session.SetString("Token", token);

                     var url = baseUrl + "GetRole";
                     httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                     string roleTypeString = await httpClient.GetStringAsync(url);

                     HttpContext.Session.SetString("Role", roleTypeString);
                     var Role = HttpContext.Session.GetString("Role");

                    return RedirectToAction("NewTicket", "Beneficiary");


                }
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Beneficiary");
        }
    }
}
