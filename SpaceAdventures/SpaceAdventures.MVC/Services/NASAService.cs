
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models.NASA;
using SpaceAdventures.MVC.Services.Interfaces;
using Microsoft.VisualBasic;

namespace SpaceAdventures.MVC.Services
{
    public class NASAService : INASAService
    {
        private HttpClient _httpClient;


        public NASAService(HttpClient httpClient) 

        {
            _httpClient=httpClient;
        }
        public async Task<NasaCollection> GetNasaData(string planetName, string? accessToken)
        {
            
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/NASA/GetAlbum/" + planetName);

            if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NasaCollection>(content);

            return data;
        }

        public async Task<string> GetNasaVideo(NasaCollection nasaCollection, string? accessToken)
        {
            Random rnd = new Random();
            List<string> lst = new List<string>();
            foreach (var item in nasaCollection.collection.items)
            {
                string path = item.href;
                path=Strings.Replace(path, "/", "%2F");
                var response =
                    await _httpClient.GetAsync("https://localhost:7195/api/v1.0/NASA/GetNasaVideo/" + path);
                if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<string>>(content);
                lst.AddRange(data);

                
            }

            string video = lst[rnd.Next(0, lst.Count)];
            return video;
        }
    }
}
