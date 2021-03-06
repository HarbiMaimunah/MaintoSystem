using BeneficiaryPortal.Filters;
using BeneficiaryPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Controllers
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class BeneficiaryEntryController : Controller
    {
        public static string baseUrl = ConfigurationManager.AppSettings["BeneficiaryEntryLocalhost"].ToString();
        public static string sysUserUrl = ConfigurationManager.AppSettings["SystemUserLocalhost"].ToString();

        public async Task<IActionResult> Signup()
        {
            var buildings = await ListBuildings();
            ViewBag.BuildingsList = buildings;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(BeneficiaryRegistration RegisterInfo, string ConfirmPass)
        {

                using (var httpClient = new HttpClient())
                {
                    if(RegisterInfo.Name == null || 
                    RegisterInfo.Phone == null || 
                    RegisterInfo.Email == null ||
                    RegisterInfo.BuildingNumber == 0 ||
                    RegisterInfo.FloorNumber == 0 ||
                    RegisterInfo.Password == null||
                    ConfirmPass == null)
                    {
                        TempData["SignupError"] = "Please complete the form";
                        return RedirectToAction("Signup");
                    }
                    else if (ConfirmPass != RegisterInfo.Password)
                    {
                        TempData["SignupError"] = "Password confirmation is incorrect";
                        return RedirectToAction("Signup");
                    }
                    

                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(RegisterInfo), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(baseUrl + "Register", stringContent))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            string error = await response.Content.ReadAsStringAsync();
                            TempData["SignupError"] = error;
                            return RedirectToAction("Signup");
                        }

                    }

                    return RedirectToAction("Signin");

                }
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

                    return RedirectToAction("TicketsList", "Beneficiary");

                }
            }
        }

        [HttpGet]
        public async Task<List<Building>> ListBuildings()
        {
            var url = baseUrl + "ListBuildings";
            HttpClient client = new HttpClient();
            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<List<Building>>(jsonStr).ToList();
            return res;
        }

        [HttpGet]
        public async Task<JsonResult> ListFloors(int BuildingNumber)
        {
            var url = baseUrl + "ListFloors/" + BuildingNumber.ToString();
            HttpClient client = new HttpClient();
            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<List<Floor>>(jsonStr).ToList();
            return Json(new SelectList(res, "Id", "Number"));
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email)
        {
            string response;
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.PostAsJsonAsync(sysUserUrl + "SendEmail", email);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("ResetPassword");
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostPassword(Guid tempPassword, string newPassword)
        {
            string response;
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.PostAsJsonAsync(sysUserUrl + "ResetPassword", tempPassword + newPassword);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Signin");
        }
    }
}
