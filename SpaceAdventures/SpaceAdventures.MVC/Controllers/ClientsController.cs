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
        public async Task<List<ClientVm>> GetClients()
        {
            var client = _httpClientFactory.CreateClient("RetryPolicy");
            var response = await client.GetAsync("https://localhost:7195/api/clients");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ClientVm>>(content);

            return data;
        }
    }
}   
