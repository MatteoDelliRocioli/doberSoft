
namespace doberSoft.Sensors.Core.Rules
{
    public interface IRules
    {
        ScanModeConstants ScanMode { get; set; }
        double ScanInterval { get; }
    }

    public interface IRules<T>: IRules
       // where T:number
    {
        T HysteresisHi { get; }
        T HysteresisLo { get; }
    }
    public enum ScanModeConstants
    {
        PushMode,
        PollMode
    }
}
