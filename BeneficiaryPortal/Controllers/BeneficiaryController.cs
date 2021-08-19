using BeneficiaryPortal.Filters;
using BeneficiaryPortal.Models;
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
        public static string baseUrl = "https://localhost:44307/api/Beneficiary/";

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

        public ActionResult TicketsList()
        {
            IEnumerable<Ticket> tickets = null;

            using (var httpClient = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                var url = baseUrl + "ListTickets";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var responseTask = httpClient.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Ticket>>();
                    readTask.Wait();

                    tickets = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    tickets = Enumerable.Empty<Ticket>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(tickets);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> RequestNewTicket(TicketRequest ticket)
            {
                using (var httpClient = new HttpClient())
                {
                    var accessToken = HttpContext.Session.GetString("Token");
                    var url = baseUrl + "SubmitRequest";
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    /*string uniqueFileName = null;

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

                    ticket.Ticket.Picture = uniqueFileName;*/

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

        //download file
        /*public FileResult DownloadAttachment(string fileDownloadName)
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);
            var path = Path.GetFullPath(Path.Combine(dirPath, "\\Users\\maimu\\OneDrive\\سطح المكتب\\C-Sharp\\HR_System\\DataAccess\\Attachments"));

            string uploadsFolder = Path.Combine(path);

            var file = uploadsFolder + "\\" + fileDownloadName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(file);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Image.Jpeg, fileDownloadName);
        }*/

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
