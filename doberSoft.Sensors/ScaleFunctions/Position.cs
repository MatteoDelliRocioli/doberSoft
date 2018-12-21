namespace doberSoft.Sensors.ScaleFunctions
{
    public class Position
    {
        public Position()
        {
        }
        public Position(decimal lat, decimal lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
