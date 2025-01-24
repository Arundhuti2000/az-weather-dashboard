using Newtonsoft.Json;
using WeatherDashboard.Web.Models;

namespace WeatherDashboard.Web.Services
{
    public interface IWeatherService
    {
        Task<WeatherViewModel> GetWeatherDataAsync(string city);
    }
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public WeatherService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _apiKey = configuration["WeatherApiKey"];
        }

        public async Task<WeatherViewModel> GetWeatherDataAsync(string city)
        {
            var response = await _client.GetAsync($"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(content);

            return new WeatherViewModel
            {
                City = city,
                Temperature = data.current.temp_c,
                Condition = data.current.condition.text,
                LastUpdated = DateTime.Parse(data.current.last_updated)
            };
        }

    }
}
