using BackOfficePortal.Filters;
using BackOfficePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BackOfficePortal.Controllers
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class MaintenanceWorkerController : Controller
    {
        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/MaintenanceWorker/";

        public ActionResult AcceptingTicket()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AcceptTicket(int TicketId)
        {
            string response;
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.PostAsJsonAsync("AcceptingTicket", TicketId);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("Success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ListTickets()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetTickets()
        {
            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("ListTickets");
                if (httpResponse.IsSuccessStatusCode)
                {
                    TicketList = await httpResponse.Content.ReadAsAsync<List<Ticket>>();
                }
            }
            return Json(TicketList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ViewTicket()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetTicket(int TicketId)
        {
            Ticket ticket = new Ticket();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("GetTicket" + TicketId);
                if (httpResponse.IsSuccessStatusCode)
                {
                    ticket = await httpResponse.Content.ReadAsAsync<Ticket>();
                }
            }
            return Json(ticket, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

    }
}
