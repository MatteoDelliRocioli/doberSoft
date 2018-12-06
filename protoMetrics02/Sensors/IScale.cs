namespace doberSoft.protoMetrics02.Sensors
{
    internal interface IScale < Tin, Tout>
    {
        Tout Scale(Tin Value);
        IScaleParameters Parameters { get; }
    }
}