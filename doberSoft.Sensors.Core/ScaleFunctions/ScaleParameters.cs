namespace doberSoft.Sensors.Core.ScaleFunctions
{
    public class ScaleParameters: IScaleParameters
    {
        public int MinIn { get; set; }
        public int MaxIn { get; set; }
        public decimal MinOut { get; set; }
        public decimal MaxOut { get; set; }
    }
}
