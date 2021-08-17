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

namespace BackOfficePortal.Controllers
{
    public class SystemAdminController : Controller
    {

        HttpClient client = new HttpClient();
        public static string baseUrl = "http://localhost:16982/api/SystemAdmin/";

        public async Task<List<BuildingsTable>> getAllBuldingsAsync()
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
            return building;
        }
        [HttpGet]
        public IActionResult BuildingsTable()
        {
            var Buildings = getAllBuldingsAsync();
            return View(Buildings);
        }
    }
}
