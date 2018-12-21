using doberSoft.Sensors.Core;

namespace doberSoft.Sensors.Core.ScaleFunctions
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