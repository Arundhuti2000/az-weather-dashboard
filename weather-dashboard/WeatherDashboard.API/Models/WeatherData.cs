namespace WeatherDashboard.API.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
