using Newtonsoft.Json;
using WeatherDashboard.API.Models;

namespace WeatherDashboard.API.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city);
    }
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        public WeatherService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _apiKey = configuration["WeatherApiKey"];
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                var response = await _client.GetAsync($"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(content);

                return new WeatherData
                {
                    City = city,
                    Temperature = data.current.temp_c,
                    Condition = data.current.condition.text,
                    Timestamp = DateTime.Parse(data.current.last_updated)
                };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching weather data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error parsing weather data: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }
    }
}
