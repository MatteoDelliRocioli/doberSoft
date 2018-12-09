namespace doberSoft.protoMetrics03.ScaleFunctions
{
    public interface IScaleParameters
    {
        int MinIn { get; set; }
        int MaxIn { get; set; }
        decimal MinOut { get; set; }
        decimal MaxOut { get; set; }

    }
}
