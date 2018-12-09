using doberSoft.protoMetrics03.layer0;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    public interface IScale < Tin, Tout>
    {
        Tout Scale(IInput<Tin>[] inputs);
        IScaleParameters Parameters { get; }
    }
}