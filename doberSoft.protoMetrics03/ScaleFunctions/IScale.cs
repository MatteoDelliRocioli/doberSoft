using doberSoft.protoMetrics03.layer1;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    public interface IScale
    {
        IScaleParameters Parameters { get; }
    }
    public interface IScale < Tin, Tout>: IScale
    {
        Tout Scale(IInput<Tin>[] inputs);
    }
}