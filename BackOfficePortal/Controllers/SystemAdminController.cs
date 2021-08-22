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
using System.Configuration;

namespace BackOfficePortal.Controllers
{
    public class SystemAdminController : Controller
    {

        HttpClient client = new HttpClient();
        public static string baseUrl = ConfigurationManager.AppSettings["SystemAdminLocalhost"].ToString();
        [HttpGet]
        public async Task<IActionResult> getAllBuildings()
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
        public async Task<IActionResult> AddBuilding(char BuildingNumber, bool IsOwned, int CityId, string Street = null , int BuildingManagerId = 0)
        {
            BuildingAdd buildingAdded = new BuildingAdd() 
            { BuildingNumber = BuildingNumber, IsOwned = IsOwned, CityId = CityId, Street = Street, BuildingManagerId = BuildingManagerId };

            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri(baseUrl+ "AddBuilding");

                //HTTP POST
                var post = await client.PostAsJsonAsync<BuildingAdd>("AddBuilding", buildingAdded);

            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            List<CountryDto> countries = new List<CountryDto>();
            using (client)
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpResponse = await client.GetAsync(baseUrl + "GetCountries");
                if (httpResponse.IsSuccessStatusCode)
                {
                    countries = await httpResponse.Content.ReadAsAsync<List<CountryDto>>();
                }
                }
            return View(countries);
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
