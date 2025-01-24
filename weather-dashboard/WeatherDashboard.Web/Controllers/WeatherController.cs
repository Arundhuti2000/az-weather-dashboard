using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.API.Models;
using WeatherDashboard.Web.Models;
using WeatherDashboard.Web.Services;

namespace WeatherDashboard.Web.Controllers;

public class WeatherController : Controller
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    public WeatherController(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(string city = "London")
    {
        var apiKey = _configuration["WeatherApiKey"];
        var response = await _client.GetAsync($"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={city}");
        // Handle response and return view
        try
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            return View(weatherData);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error fetching weather data.");
            return View(null);
        }
       
    }
}
