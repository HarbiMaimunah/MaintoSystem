using BeneficiaryPortal.Models;
using BeneficiaryPortal.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class BeneficiaryController : Controller
    {
        private readonly string baseUrl = "https://localhost:44307/api/Beneficiary/";

        public async Task<IActionResult> MyAccount()
        {
            var userInfo = await GetAccount();
            return View(userInfo);
        }

        [HttpGet]
        public async Task<UserInfo> GetAccount()
        {
            using (HttpClient client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                var url = baseUrl + "GetUserInfo";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string jsonStr = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<UserInfo>(jsonStr);
                return res;
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Signin", "BeneficiaryEntry");
        }

        public async Task<IActionResult> NewTicket()
        {
            var maintenanceTypes = await ListMaintenanceTypes();
            ViewBag.MaintenanceTypesList = maintenanceTypes;
            return View();
        }

        [HttpGet]
        public async Task<List<MaintenanceType>> ListMaintenanceTypes()
        {
            using (HttpClient client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                var url = baseUrl + "ListMaintenanceTypes";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string jsonStr = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<MaintenanceType>>(jsonStr).ToList();
                return res;
            }    
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

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });


            return Redirect(Request.Headers["Referer"].ToString());
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActionResult> TicketsList()
        {
            //IEnumerable<Ticket> tickets = null;
            List<TicketsDto> tickets = new List<TicketsDto>();

            using (HttpClient httpClient = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var url = baseUrl + "ListTickets";
                var responseTask = await httpClient.GetAsync(url);

                if (responseTask.IsSuccessStatusCode)
                {
                    tickets = await responseTask.Content.ReadAsAsync<List<TicketsDto>>();

                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(tickets);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> RequestNewTicket(TicketRequest ticket, int MaintenanceTypeId)
            {
            ticket.MaintenanceTypeID = MaintenanceTypeId;
                using (var httpClient = new HttpClient())
                {
                    var accessToken = HttpContext.Session.GetString("Token");
                    var url = baseUrl + "SubmitRequest";
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                if (ticket.Date == null || ticket.Description == null)
                {
                    TempData["NewTicketError"] = "Please complete the form";
                    return RedirectToAction("NewTicket");
                }
               

                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
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
            }

        //-------------------------------------------------------------------------------------------------------------------------------------
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostEmail(string email)
        {
            string response;
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.PostAsJsonAsync("http://localhost:16982/api/SystemUser/" + "SendEmail", email);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------
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
                var httpResponse = await client.PostAsJsonAsync("http://localhost:16982/api/SystemUser/" + "ResetPassword", tempPassword+ newPassword);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

    }
}
