namespace forecaster_backend.Models
{
    public class ForecastItem
    {
        // We just need the city and forecast for the result. This is the data model.
        public string City { get; set; }
        public List Forecast { get; set; }
    }
}