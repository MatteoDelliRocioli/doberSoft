namespace doberSoft.protoMetrics02.Sensors
{
    class Position
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

        public override string ToString()
        {
            return ToString("0.00");
        }

        public string ToString(string format)
        {
            return $"{{\"Lat\":\"{Latitude.ToString(format)}\",\"Lon\":\"{Longitude.ToString(format)}\"}}";
        }
    }
}
