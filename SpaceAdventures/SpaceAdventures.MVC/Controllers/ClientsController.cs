using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SpaceAdventures.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public ClientsController(IHttpClientFactory httpClientFactory)  
        {
            _httpClientFactory = httpClientFactory;
        }

        //[HttpGet]
        //public Task<IActionResult> GetClients()
        //{
        //    //var client = _httpClientFactory.CreateClient("RetryPolicy");
        //    //var response = await client.GetAsync("https://localhost:/api/GetClients");
        //    return View();
        //}
    }
}   
