using BeneficiaryPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    /*[ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]*/
    public class BeneficiaryController : Controller
    {
        public static string baseUrl = "http://10.6.8.91:44307/api/Beneficiary/";

        public IActionResult NewTicket()
        {
            return View();
        }

        public async Task<IActionResult> RequestNewTicket(NewTicket ticket)
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
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "BeneficiaryEntry");
        }
    }
}
