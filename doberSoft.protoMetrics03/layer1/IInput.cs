

namespace doberSoft.protoMetrics03.layer1
{
    public interface IInput
    {
    }
    public interface IInput<T> //: IInput
    {
        T GetValue();
    }
}
