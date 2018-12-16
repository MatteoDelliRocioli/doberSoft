

namespace doberSoft.Sensors.Core
{
    public interface IInput
    {
    }
    public interface IInput<T> //: IInput
    {
        T GetValue();
    }
}
