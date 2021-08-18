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

            //List<FloorTable> floorTables = new List<FloorTable> { new FloorTable { FloorId = 1, FloorNumber = 'g' } };
            //floorTables.Add(new FloorTable { FloorId = 2, FloorNumber = '2' });
            //floorTables.Add(new FloorTable { FloorId = 3, FloorNumber = '3' });

            IEnumerable<BuildingsTable> buildings = null;

            //building.Add(new BuildingsTable{ 
            //    BuildingId =1,
            //    BuildingNumber="k", 
            //    BuildingManagerName="shady",
            //    BuildingManagerEmail="shady@gmail.com", 
            //    BuildingManagerId =1,
            //    City = "الرياض", 
            //    CityId =1, 
            //    Country = "السعودية", 
            //    CountryId=1, 
            //    IsOwned=true, 
            //    Street = "الريان", 
            //    FloorTables = floorTables });
            //var Building = getAllBuldingsAsync();

            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.BaseAddress = new Uri(baseUrl);
                var responseTask = client.GetAsync("GetBuildingsTable");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<BuildingsTable>>();
                    readTask.Wait();

                    buildings = readTask.Result;

                }
                else
                {
                    buildings = Enumerable.Empty<BuildingsTable>();
                }
            }

            return View(buildings);
        }
    }
}
