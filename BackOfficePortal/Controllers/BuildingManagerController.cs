using BackOfficePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BackOfficePortal.ModelsLanguage;
using BackOfficePortal.Filters;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace BackOfficePortal.Controllers
{

    [ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class BuildingManagerController : Controller
    {
        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/BuildingManagerAPI/";


        public ActionResult AddComments()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostComment(Comment comment)
        {
            string response;
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                var url = baseUrl + "AddComments";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(url, stringContent);
                //var httpResponse = await client.PostAsJsonAsync(baseUrl + $"AddComments", comment).ConfigureAwait(false);
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                    TempData["Confirmation"] = "you added the comment";
                }
                return RedirectToAction("AddComments");
            }
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

            List<Ticket> TicketList = new List<Ticket>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync(baseUrl + "ViewTicketsStatus");
                if (httpResponse.IsSuccessStatusCode)
                {
                    StatusList = await httpResponse.Content.ReadAsAsync<List<string>>();
                }
            }
            return Json(StatusList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
