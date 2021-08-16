using BackOfficePortal.Filters;
using BeneficiaryPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Controllers
{
    /*[ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]*/
    public class BeneficiaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static string baseUrl = "http://localhost:16982/api/BeneficiaryEntry/";

        public async Task<IActionResult> Signup()
        {
            var buildings = await ListBuildings();
            ViewBag.BuildingsList = new SelectList(buildings, "Id", "Number");
            return View();
        }

        public async Task<IActionResult> Register(BeneficiaryRegistration RegisterInfo)
        {
            using (var httpClient = new HttpClient())
            {
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

                    return RedirectToAction("NewTicket", "Beneficiary");


                }
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Beneficiary");
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
        public JsonResult ListFloors(int buildingID)
        {
            var url = baseUrl + "ListFloors/" + buildingID.ToString();
            
            
        }

        public IActionResult NewTicket()
        {
            return View();
        }
        //------------------------------------------------------------------------------------------------------------------
        public IActionResult UpdateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PutUser(BackOfficePortal.Models.User user)
        {
            string response;
            
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.PutAsJsonAsync("http://localhost:16982/api/SystemUser/" + "UpdateUser", user);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------------
        /*public async Task<IActionResult> RequestNewTicket(NewTicket ticket)
        {
            using (var httpClient = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                var url = baseUrl + "SubmitRequest";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string uniqueFileName = null;

                if (ticket.Attachment != null)
                {
                    var dirPath = Assembly.GetExecutingAssembly().Location;
                    dirPath = Path.GetDirectoryName(dirPath);
                    var path = Path.GetFullPath(Path.Combine(dirPath, @"C:\Users\maimu\Source\Repos\NWcodeart\MaintenanceMagementSystems\MaintenanceMagementSystems.Database\TicketRequestsAttachments"));

                    string uploadsFolder = Path.Combine(path);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + ticket.Attachment.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ticket.Attachment.CopyTo(fileStream);
                    }
                }

                ticket.Ticket.Picture = uniqueFileName;

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(ticket.Ticket), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(url, stringContent))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        TempData["NewTicketError"] = error;
                        return RedirectToAction("NewTicket");
                    }

                }
            }

            TempData["NewTicketConfirmation"] = "Your ticket has been sent successfully";
            return RedirectToAction("NewTicket");
        }*/


    }
}
