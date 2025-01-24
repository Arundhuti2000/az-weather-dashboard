namespace WeatherDashboard.Web.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
