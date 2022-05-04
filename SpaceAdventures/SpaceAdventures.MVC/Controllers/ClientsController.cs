using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.MVC.Controllers
{
    [ApiController]
    [Route("api/mvc/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientsController(IHttpClientFactory httpClientFactory)  
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ClientsVm> GetClients()
        {
            var client = _httpClientFactory.CreateClient("RetryPolicy");
            var response = await client.GetAsync("https://localhost:7195/api/v1.0/Clients");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ClientsVm>(content);

            return data;
        }
    }
}   
