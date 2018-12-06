namespace doberSoft.protoMetrics02.Sensors
{
    interface IScaleParameters
    {
        int MinIn { get; set; }
        int MaxIn { get; set; }
        decimal MinOut { get; set; }
        decimal MaxOut { get; set; }

    }
}
