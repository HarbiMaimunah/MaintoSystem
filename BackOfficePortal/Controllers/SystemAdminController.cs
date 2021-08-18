using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using BackOfficePortal.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Collections;

namespace BackOfficePortal.Controllers
{
    public class SystemAdminController : Controller
    {

        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/SystemAdmin/";
        [HttpGet]
        public async Task<IActionResult> getAllBuldings()
        {
            List<BuildingsTable> building = new List<BuildingsTable>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync(baseUrl + "GetBuildingsTable");
                if (httpResponse.IsSuccessStatusCode)
                {
                    building = await httpResponse.Content.ReadAsAsync<List<BuildingsTable>>();
                }
            }
            return View(building);
        }
        [HttpPost]
        public IActionResult AddBuilding(char BuildingNumber, bool IsOwned, int CityId, string Street = null )
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCountries()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCountry()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteCountry()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteCity()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCity()
        {
            return View();
        }
    }
}
