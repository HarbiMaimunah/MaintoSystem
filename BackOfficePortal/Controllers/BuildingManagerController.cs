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
using BackOfficePortal.Lookup;

namespace BackOfficePortal.Controllers
{

    [ServiceFilter(typeof(AuthorizeFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class BuildingManagerController : Controller
    {
        public static string baseUrl = "http://10.6.8.91:44307/api/BuildingManagerAPI/";


        public ActionResult AddComments()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostComment(Comment comment)
        {
            string response;
            using (HttpClient client = new HttpClient())
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
            using (HttpClient client = new HttpClient())
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
        public async Task<ActionResult> ViewTickets()
        {
            List<TicketsDto> TicketList = new List<TicketsDto>();
            using (HttpClient client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var url = baseUrl + "ViewTickets";
                var httpResponse = await client.GetAsync(url);
                var response = httpResponse;
                if (response.IsSuccessStatusCode)
                {
                    TicketList = await response.Content.ReadAsAsync<List<TicketsDto>>();
                }
            }
            return View(TicketList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<ActionResult> ViewBuilding()
        {
            Building building = new Building();
            using (HttpClient client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync(baseUrl + "ViewBuilding");
                if (httpResponse.IsSuccessStatusCode)
                {
                    building = await httpResponse.Content.ReadAsAsync<Building>();
                }
            }
            return View(building);
        }
        [HttpGet]
      /*  public async Task<ActionResult> GetBuilding()
        {
            return Json(building, System.Web.Mvc.JsonRequestBehavior.AllowGet);

        }*/

        //------------------------------------------------------------------------------------------------------------------------------------------------
       /* public async Task<ActionResult> ViewTicketStatus()
        {
            List<Status> StatusList = new List<Status>();
            using (HttpClient client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync(baseUrl + "ViewTicketsStatus");
                if (httpResponse.IsSuccessStatusCode)
                {
                    StatusList = await httpResponse.Content.ReadAsAsync<List<Status>>();
                }
            }
            return View(StatusList);
        }*/
       /* [HttpGet]
        public async Task<ActionResult> GetTicketStatus()
        {
            List<string> StatusList = new List<string>();
            using (HttpClient client = new HttpClient())
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
       */
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
