using BackOfficePortal.Filters;
using BackOfficePortal.Lookup;
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("ListOfWorkers");
                if (httpResponse.IsSuccessStatusCode)
                {
                    UsersList = await httpResponse.Content.ReadAsAsync<List<User>>();
                }
            }
            return Json(UsersList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult ViewUnderReviewTickets()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetUnderReviewTickets()
        {
            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("ViewUnderReviewTickets");
                if (httpResponse.IsSuccessStatusCode)
                {
                    TicketList = await httpResponse.Content.ReadAsAsync<List<Ticket>>();
                }
            }
            return Json(TicketList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult ViewMainteneceType()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetMainteneceType()
        {
            List<MaintenanceType> TypeList = new List<MaintenanceType>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("ViewMainteneceType");
                if (httpResponse.IsSuccessStatusCode)
                {
                    TypeList = await httpResponse.Content.ReadAsAsync<List<MaintenanceType>>();
                }
            }
            return Json(TypeList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult AddMainteneceType()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PostMainteneceType(MaintenanceType NewType)
        {
            string response;
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.PostAsJsonAsync("AddMainteneceType", NewType);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult UpdateMainteneceType()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PutMainteneceType(MaintenanceType UpdatedType)
        {
            string response;
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("UpdateMainteneceType");
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult DeleteMainteneceType()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> DeleteType(MaintenanceType DeletedType)
        {
            string response;
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync("DeleteMainteneceType");
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("Success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
