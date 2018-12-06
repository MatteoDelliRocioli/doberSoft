namespace doberSoft.protoMetrics02.Sensors
{
    class LinearScaleParameters: IScaleParameters
    {
        public int MinIn { get; set; }
        public int MaxIn { get; set; }
        public decimal MinOut { get; set; }
        public decimal MaxOut { get; set; }
    }
}
