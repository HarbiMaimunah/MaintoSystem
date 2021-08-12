using BackOfficePortal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace BackOfficePortal.Controllers
{
    public class BuildingManagerController : Controller
    {
        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/BuildingManagerAPI/";


        public ActionResult AddComments()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> PostComment(int id, string comment)
        {
            string response;
            using (client)
            {
                var httpResponse = await client.PostAsJsonAsync(baseUrl + $"AddComments/{id}" , comment);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return Json("Success", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult EditBuilding()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PutBuilding(int buildingID, Building Updatedbuilding)
        {
            string response;
            using (client)
            {
                var httpResponse = await client.PutAsJsonAsync(baseUrl + "EditBuilding" + buildingID,  Updatedbuilding);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult ViewTickets()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetTickets()
        {
            HttpClient client = new HttpClient();

            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var httpResponse = await client.GetAsync(baseUrl + "ViewTickets");
                if (httpResponse.IsSuccessStatusCode)
                {
                    TicketList = await httpResponse.Content.ReadAsAsync<List<Ticket>>();
                }
            }
            return Json(TicketList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult ViewBuilding()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetBuilding()
        {
            Building building = new Building();
            using (client)
            {
                var httpResponse = await client.GetAsync(baseUrl + "ViewBuilding");
                if (httpResponse.IsSuccessStatusCode)
                {
                    building = await httpResponse.Content.ReadAsAsync<Building>();
                }
            }
            return Json(building, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult ViewTicketStatus()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetTicketStatus()
        {
            List<string> StatusList = new List<string>();
            using (client)
            {
                var httpResponse = await client.GetAsync(baseUrl + "ViewTicketsStatus");
                if (httpResponse.IsSuccessStatusCode)
                {
                    StatusList = await httpResponse.Content.ReadAsAsync<List<string>>();
                }
            }
            return Json(StatusList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

    }
}
