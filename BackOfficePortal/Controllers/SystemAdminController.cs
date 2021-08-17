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

            //List<FloorTable> floorTables = new List<FloorTable> { new FloorTable { FloorId = 1, FloorNumber = 'g' } };
            //floorTables.Add(new FloorTable { FloorId = 2, FloorNumber = '2' });
            //floorTables.Add(new FloorTable { FloorId = 3, FloorNumber = '3' });
            List<BuildingsTable> building = new List<BuildingsTable>();
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
            var Building = getAllBuldingsAsync();
            return View(Building);
        }
    }
}
