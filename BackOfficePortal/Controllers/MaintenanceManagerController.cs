using BackOfficePortal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackOfficePortal.Controllers
{
    public class MaintenanceManagerController : Controller
    {
        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/MaintenanceManager/";

        public ActionResult RespondToTicket()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> PostRespond(int TicketId, TicketRespond respond)
        {
            string response;
            using (client)
            {
                var httpResponse = await client.PostAsJsonAsync("RespondToTicket" + TicketId, respond);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("Success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ViewTickets()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetTickets()
        {
            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var httpResponse = await client.GetAsync("ViewTickets");
                if (httpResponse.IsSuccessStatusCode)
                {
                    TicketList = await httpResponse.Content.ReadAsAsync<List<Ticket>>();
                }
            }
            return Json(TicketList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ViewNewTickets()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetNewTickets()
        {
            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var httpResponse = await client.GetAsync("ListNewTickets");
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
        public async Task<ActionResult> GetTicket()
        {
            Ticket ticket = new Ticket();
            using (client)
            {
                var httpResponse = await client.GetAsync("GetTicket");
                if (httpResponse.IsSuccessStatusCode)
                {
                    ticket = await httpResponse.Content.ReadAsAsync<Ticket>();
                }
            }
            return Json(ticket, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult ViewWorker()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetWorker()
        {
            User user = new User();
            using (client)
            {
                var httpResponse = await client.GetAsync("GetWorker");
                if (httpResponse.IsSuccessStatusCode)
                {
                    user = await httpResponse.Content.ReadAsAsync<User>();
                }
            }
            return Json(user, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ViewWorkers()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetWorkers()
        {
            List<User> UsersList = new List<User>();
            using (client)
            {
                var httpResponse = await client.GetAsync("ListOfWorkers");
                if (httpResponse.IsSuccessStatusCode)
                {
                    UsersList = await httpResponse.Content.ReadAsAsync<List<User>>();
                }
            }
            return Json(UsersList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

    }
}
