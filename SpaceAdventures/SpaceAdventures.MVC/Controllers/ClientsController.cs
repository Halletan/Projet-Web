using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.MVC.Controllers
{
    //  [Route("api/mvc/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientsController(IHttpClientFactory httpClientFactory)  
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("/api/clients")]
        public async Task<IActionResult> GetClients()   
        {
            var client = _httpClientFactory.CreateClient("RetryPolicy");
            var response = await client.GetAsync("https://localhost:7195/api/v1.0/Clients");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ItineraryVm>(content);
            return Json(data);
        }

        public IActionResult ListOfClients()
        {
            return View();
        }
    }
}   
